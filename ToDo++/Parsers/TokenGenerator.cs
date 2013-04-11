//@ivan A0086401M
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToDo
{
    // Factory class for creating Tokens.
    public class TokenGenerator
    {
        public TokenGenerator()
        {
        }

        // ******************************************************************
        // Public Methods
        // ******************************************************************

        #region Public Token Generation Methods
        /// <summary>
        /// This method searches an input list of strings and generates the relevant
        /// tokens representing the meaning of each string.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of matched phrases as tokens.</returns>
        public List<Token> GenerateAllTokens(List<string> input)
        {
            List<Token> tokens = new List<Token>();
            List<TokenCommand> commandTokens = GenerateCommandTokens(input);
            tokens.AddRange(commandTokens);
            tokens.AddRange(GenerateIndexRangeTokens(input, commandTokens));
            tokens.AddRange(GenerateSortTypeTokens(input, commandTokens));
            tokens.AddRange(GenerateTimeRangeTokens(input, commandTokens));
            tokens.AddRange(GenerateDayTokens(input));
            tokens.AddRange(GenerateDateTokens(input));
            tokens.AddRange(GenerateTimeTokens(input));
            tokens.AddRange(GenerateContextTokens(input, tokens));
            // must be done last. all words not already in tokens are taken to be literals
            tokens.AddRange(GenerateLiteralTokens(input, tokens));
            DeconflictTokens(ref tokens);
            tokens.Sort(CompareByPosition);
            return tokens;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of command keywords and returns
        /// a list of tokens corresponding to the matched command keywords.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of command tokens</returns>
        public List<TokenCommand> GenerateCommandTokens(List<string> input)
        {
            CommandType commandType;
            List<TokenCommand> tokens = new List<TokenCommand>();
            int index = 0;
            foreach (string word in input)
            {
                string wordLower = word.ToLower();
                if (CustomDictionary.commandKeywords.TryGetValue(wordLower, out commandType))
                {
                    Logger.Info("Command word found: " + wordLower, "GenerateCommandTokens::TokenGenerator");
                    TokenCommand commandToken = new TokenCommand(index, commandType);
                    tokens.Add(commandToken);
                }
                index++;
            }
            return tokens;
        }

        /// <summary>
        /// This method checks an input list of strings for index range words and generates a list of tokens
        /// based on the found index range words.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <param name="commandTokens">The list of generated command tokens based on the same list of input strings</param>
        /// <returns>List of index range tokens</returns>
        public List<Token> GenerateIndexRangeTokens(List<string> input, List<TokenCommand> commandTokens)
        {
            List<Token> indexRangeTokens = new List<Token>();
            int index = 0;
            foreach (string word in input)
            {
                var validTokens = from token in commandTokens
                                  where token.RequiresIndexRange()
                                  select token;
                if (validTokens.Count() > 0)
                {
                    bool isAll = false;
                    int[] userDefinedIndex = null;
                    TokenIndexRange indexRangeToken = null;
                    if (CheckIfAllKeyword(word))
                    {
                        Logger.Info("Index range word found: all", "GenerateIndexRangeTokens::TokenGenerator");
                        isAll = true;
                        indexRangeToken = new TokenIndexRange(index, userDefinedIndex, isAll);
                    }
                    else if (TryGetNumericalRange(word, out userDefinedIndex))
                    {
                        var validTokensAdjacent = from token in commandTokens
                                                  where token.Position == index - 1
                                                  select token;
                        if (validTokensAdjacent.Count() > 0)
                        {
                            indexRangeToken = new TokenIndexRange(index, userDefinedIndex, isAll);
                        }
                    }

                    if (indexRangeToken != null)
                    {
                        indexRangeTokens.Add(indexRangeToken);
                    }
                }

                index++;
            }
            return indexRangeTokens;
        }

        /// <summary>
        /// This method checks an input list of strings for sort type keywords (name or date) and generates a list of
        /// tokens based on the found sort type keywords.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <param name="commandTokens">The list of generated command tokens based on the same list of input strings</param>
        /// <returns>List of sort type tokens</returns>
        public List<Token> GenerateSortTypeTokens(List<string> input, List<TokenCommand> commandTokens)
        {
            SortType sortType;
            int index = 0;
            List<Token> sortTokens = new List<Token>();

            foreach (string word in input)
            {
                string wordLower = word.ToLower();
                var validTokens = from token in commandTokens
                                  where (token.Position == index - 1 &&
                                         token.Value == CommandType.SORT)
                                  select token;
                if (validTokens.Count() > 0)
                {
                    if (CustomDictionary.sortTypeKeywords.TryGetValue(wordLower, out sortType) && index != 0)
                    {
                        Logger.Info("Sort type word found: " + wordLower, "GenerateSortTypeTokens::TokenGenerator");
                        TokenSortType sortTypeToken = new TokenSortType(index, sortType);
                        sortTokens.Add(sortTypeToken);
                    }
                }
                index++;
            }
            return sortTokens;
        }

        //@jenna A0083536B
        /// <summary>
        /// This method checks an input list of strings for time range words and generates a list of tokens
        /// based on the found time range words.
        /// </summary>
        /// <param name="inputWords">The list of command phrases, separated words and/or time/date phrases</param>
        /// <param name="commandTokens">The list of generated command tokens based on the same list of input strings</param>
        /// <returns>List of time range tokens</returns>
        public List<Token> GenerateTimeRangeTokens(List<string> inputWords, List<TokenCommand> commandTokens)
        {
            List<Token> timeRangeTokens = new List<Token>();
            int index = 0;
            bool requiresTimeLength = false;

            if (commandTokens.Count(e => e.RequiresTimeRange()) < 1)
            {
                return timeRangeTokens;
            }
            if (commandTokens.Count(e => e.RequiresTimeRange() && e.Value != CommandType.ADD) > 0)
            {
                requiresTimeLength = true;
            }

            foreach (string word in inputWords)
            {
                int timeRangeAmount = 0;
                TimeRangeType timeRangeType;
                TimeRangeKeywordsType timeRangeKeyword;
                TokenTimeRange timeRangeToken = null;
                string wordLower = word.ToLower();
                if (CustomDictionary.timeRangeKeywords.TryGetValue(wordLower, out timeRangeKeyword))
                {
                    Logger.Info("Time range word found: " + wordLower, "GenerateTimeRangeTokens::TokenGenerator");
                    timeRangeToken = new TokenTimeRange(index, timeRangeKeyword);
                }

                else if (CustomDictionary.IsTimeRange(wordLower) && requiresTimeLength)
                {
                    Match match = CustomDictionary.isTimeRange.Match(wordLower);
                    Int32.TryParse(match.Groups["index"].Value, out timeRangeAmount);
                    string matchString = match.Groups["type"].Value;
                    if (timeRangeAmount == 0 && matchString != null)
                    {
                        Logger.Warning("Task duration was specified without amount" + wordLower, "GenerateTimeRangeTokens::TokenGenerator");
                        throw new InvalidTimeRangeException("Task duration type was specified without a valid amount (0 " + matchString + ").");
                    }
                    if (!CustomDictionary.timeRangeType.TryGetValue(matchString, out timeRangeType))
                    {
                        Logger.Error("Recognized time range type (" + matchString + ") cannot be found in custom dictionary", "GenerateTimeRangeTokens::TokenGenerator");
                        throw new Exception("Something is wrong with IsTimeRange regex etc.");
                    }
                    Logger.Info("Time range word found: " + timeRangeAmount + " " + matchString, "GenerateTimeRangeTokens::TokenGenerator");
                    timeRangeToken = new TokenTimeRange(index, timeRangeAmount, timeRangeType);
                }

                if (timeRangeToken != null)
                {
                    timeRangeTokens.Add(timeRangeToken);
                }
                index++;
            }
            return timeRangeTokens;
        }

        //@ivan A0086401M
        /// <summary>
        /// Checks if the supplied string matchCheck represents a number or a numerical range.
        /// If positive, the index or pair of indexes (respectively) is added to the
        /// integer array.
        /// </summary>
        /// <param name="matchCheck">The string to be checked</param>
        /// <param name="userDefinedIndex">The integer array to be updated</param>
        /// <returns>Returns true if a numerical range is detected; false if otherwise</returns>
        private bool TryGetNumericalRange(string matchCheck, out int[] userDefinedIndex)
        {
            userDefinedIndex = null;
            Match match = CustomDictionary.isNumericalRange.Match(matchCheck);
            bool matchSuccess = match.Success;
            if (matchSuccess)
            {
                userDefinedIndex = new int[TokenIndexRange.RANGE];
                int startIndex, endIndex;
                Int32.TryParse(match.Groups["start"].Value, out startIndex);
                if (match.Groups["end"].Success)
                {
                    Int32.TryParse(match.Groups["end"].Value, out endIndex);
                }
                else
                {
                    endIndex = startIndex;
                }
                userDefinedIndex[TokenIndexRange.START_INDEX] = startIndex;
                userDefinedIndex[TokenIndexRange.END_INDEX] = endIndex;
                Logger.Info("Index range found: " + startIndex + " to " + endIndex, "TryGetNumericalRange::TokenGenerator");
            }
            return matchSuccess;
        }

        /// <summary>
        /// This method checks if the input word is the 'all' keyword, non case-sensitively.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool CheckIfAllKeyword(string word)
        {
            bool isAll;
            if ((CustomDictionary.rangeAllKeywords.Where(e => e == word.ToLower()).Count()) >= 1)
            {
                isAll = true;
            }
            else
            {
                isAll = false;
            }
            return isAll;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of day keywords and returns
        /// a list of tokens corresponding to the matched day keywords.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of day tokens</returns>
        public List<Token> GenerateDayTokens(List<string> input)
        {
            List<Token> dayTokens = new List<Token>();
            DayOfWeek day;
            int index = 0;
            foreach (string word in input)
            {
                string wordLower = word.ToLower();
                if (CustomDictionary.dayKeywords.ContainsKey(wordLower))
                {
                    Logger.Info("Day word found: " + wordLower, "GenerateDayTokens::TokenGenerator");
                    CustomDictionary.dayKeywords.TryGetValue(wordLower, out day);
                    TokenDay dayToken = new TokenDay(index, day);
                    dayTokens.Add(dayToken);
                }
                index++;
            }
            return dayTokens;
        }

        //@jenna A0083536B
        /// <summary>
        /// This method searches an input list of strings for all valid dates and generates a list of date tokens
        /// corresponding to all the found matched date strings using regexes.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of date tokens</returns>
        public List<TokenDate> GenerateDateTokens(List<string> input)
        {
            int day = 0;
            int month = 0;
            int year = 0;
            int index = 0;
            List<TokenDate> dateTokens = new List<TokenDate>();

            foreach (string word in input)
            {
                Specificity isSpecific = new Specificity();
                DateTime dateTime = new DateTime();
                TokenDate dateToken = null;
                string wordLower = word.ToLower();
                if (CustomDictionary.IsValidDate(wordLower)
                    || CustomDictionary.IsToday(wordLower)
                    || CustomDictionary.IsTomorrow(wordLower)
                    || CustomDictionary.IsValidMonthWord(wordLower)
                    )
                {
                    string dayString = String.Empty;
                    string monthString = String.Empty;
                    string yearString = String.Empty;
                    if (CustomDictionary.IsToday(wordLower))
                    {
                        day = DateTime.Now.Day;
                        month = DateTime.Now.Month;
                        year = DateTime.Now.Year;
                    }
                    else if (CustomDictionary.IsTomorrow(wordLower))
                    {
                        day = DateTime.Now.Day + 1;
                        month = DateTime.Now.Month;
                        year = DateTime.Now.Year;
                    }
                    else if (CustomDictionary.IsValidMonthWord(wordLower))
                    {
                        monthString = CustomDictionary.date_alphabeticMonth.Match(wordLower).Groups["month"].Value;
                        isSpecific.Day = false;
                        isSpecific.Year = false;
                        day = 1;
                        month = ConvertToNumericMonth(monthString);
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        Match match = GetDateMatch(wordLower);
                        GetMatchTagValues(match, ref dayString, ref monthString, ref yearString);
                        ConvertDateStringsToInt(dayString, monthString, yearString, ref day, ref month, ref year);
                    }
                    // no day input
                    if (day == 0)
                    {
                        isSpecific.Day = false;
                        day = 1;
                    }
                    // no month input
                    if (month == 0)
                    {
                        isSpecific.Month = false;
                        month = DateTime.Today.Month;
                    }
                    // no year input
                    if (year == 0 || isSpecific.Year == false)
                    {
                        isSpecific.Year = false;
                        year = DateTime.Today.Year;
                        dateTime = TryParsingDate(year, month, day, true);
                        if (DateTime.Compare(dateTime, DateTime.Today) < 0)
                        {
                            if (isSpecific.Month == false)
                            {
                                month = DateTime.Today.AddMonths(1).Month;
                                year = DateTime.Today.AddMonths(1).Year;
                            }
                            else if (month != DateTime.Now.Month)
                            {
                                year = DateTime.Today.AddYears(1).Year;
                            }
                        }
                    }
                    // if year has less than 4 digit input.
                    else if (year < 1000)
                    {
                        year += 2000;
                    }
                    dateTime = TryParsingDate(year, month, day, false);
                    Logger.Info("Date word found: " + dateTime, "GenerateDateTokens::TokenGenerator");
                    dateToken = new TokenDate(index, dateTime, isSpecific);
                    dateTokens.Add(dateToken);
                }
                index++;
            }
            return dateTokens;
        }

        //@ivan A0086401M
        /// <summary>
        /// This method searches an input list of strings for all valid times and generates a list of time tokens
        /// corresponding to all the found matched time strings using regexes.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <returns>List of time tokens</returns>
        // uses a combined regex to get hour, minute, second via tags and return a TimeSpan.
        public List<Token> GenerateTimeTokens(List<string> input)
        {
            List<Token> timeTokens = new List<Token>();
            Match match;
            int index = 0;
            bool specificity = true;
            bool Format_12Hour = false;
            foreach (string word in input)
            {
                string wordLower = word.ToLower();
                int hours = 0, minutes = 0, seconds = 0;
                match = CustomDictionary.time_12HourFormat.Match(wordLower);
                if (!match.Success)
                {
                    match = CustomDictionary.time_24HourFormat.Match(wordLower);
                }
                else
                {
                    Format_12Hour = true;
                }
                if (match.Success
                    || CustomDictionary.timeSpecificKeywords.TryGetValue(wordLower, out hours))
                {
                    string strHours = match.Groups["hours"].Value;
                    string strMinutes = match.Groups["minutes"].Value;
                    if (strHours.Length != 0)
                    {
                        hours = Int32.Parse(strHours);
                        if (Format_12Hour)
                        {
                            hours = ConvertTo24HoursFormat(match.Groups["format"].Value, hours);
                        }
                    }
                    if (strMinutes.Length != 0)
                    {
                        minutes = Int32.Parse(strMinutes);
                    }
                    TimeSpan time = new TimeSpan(hours, minutes, seconds);
                    Logger.Info("Time word found: " + time, "GenerateTimeTokens::TokenGenerator");
                    TokenTime timeToken = new TokenTime(index, time, specificity);
                    timeTokens.Add(timeToken);
                }
                index++;
            }
            return timeTokens;
        }

        /// <summary>
        /// This method converts a time specified in hours and am/pm i.e. 4am and 5pm into its corresponding
        /// 24 hours format in hundreds i.e. 4 (hundred) hours and 17 (hundred) hours respectively.
        /// </summary>
        /// <param name="format">Either 'am' or 'pm'</param>
        /// <param name="hours">The hours in 12 hours format</param>
        /// <returns>The hour in 24 hours format in hundreds</returns>
        private int ConvertTo24HoursFormat(string format, int hours)
        {
            string formatLower = format.ToLower();
            if (formatLower == "pm" && hours != 12)
            {
                hours += 12;
            }
            if (formatLower == "am" && hours == 12)
            {
                hours = 0;
            }
            return hours;
        }

        /// <summary>
        /// This method searches an input list of strings against the set list of context keywords and returns
        /// a list of tokens corresponding to the matched context keywords. It requires a list of already parsed
        /// tokens as it only returns tokens which are in front of a context accepting token.
        /// </summary>
        /// <param name="input">The list of command phrases, separated words and/or time/date phrases</param>
        /// <param name="parsedTokens">The list of tokens to check against for context accepting tokens.</param>
        /// <returns>List of context tokens</returns>
        public List<Token> GenerateContextTokens(List<string> input, List<Token> parsedTokens)
        {
            int index = 0;
            ContextType context;
            List<Token> contextTokens = new List<Token>();
            foreach (string word in input)
            {
                if (CustomDictionary.contextKeywords.TryGetValue(word.ToLower(), out context))
                {
                    object nextToken = GetTokenAtPosition(parsedTokens, index + 1);
                    Logger.Info("Context word found: " + word.ToLower(), "GenerateContextTokens::TokenGenerator");
                    TokenContext newToken = new TokenContext(index, context);
                    contextTokens.Add(newToken);
                }
                index++;
            }
            
            contextTokens = (from token in contextTokens
                             where TokenAcceptsContext(GetTokenAtPosition(parsedTokens, token.Position + 1)) ||
                                   TokenAcceptsContext((GetTokenAtPosition(contextTokens, token.Position + 1)))
                             select token).ToList();
            return contextTokens;
        }

        /// <summary>
        /// Checks if the token accepts a context token before it.
        /// Returns true if it does; False if it is null or does not.
        /// </summary>
        /// <param name="token">The token to check</param>
        /// <returns>Flag indicating if the token can use a context type.</returns>
        private bool TokenAcceptsContext(Token token)
        {
            if (token == null) return false;
            return token.AcceptsContext();
        }

        /// <summary>
        /// This method compares an input list of strings against a list of parsed Tokens, and returns a list of Tokens
        /// representing all strings which have not been been parsed as Tokens. The purpose of this method is to assign
        /// all unparsed strings as LiteralTokens.
        /// </summary>
        /// <param name="input">The list of input words</param>
        /// <param name="parsedTokens">The list of parsedTokens</param>
        /// <returns>List of context tokens</returns>
        public List<Token> GenerateLiteralTokens(List<string> input, List<Token> parsedTokens)
        {
            List<Token> literalTokens = new List<Token>();
            foreach (Token token in parsedTokens)
            {
                input[token.Position] = null;
            }
            int index = 0;
            string literal = String.Empty;
            foreach (string remainingWord in input)
            {
                if (remainingWord != null)
                {
                    literal = literal + StringParser.UnmarkWordsAsAbsolute(remainingWord) + " ";
                }
                else if (remainingWord == null && literal != String.Empty)
                {
                    AddLiteralToken(ref literal, index, ref literalTokens);
                }
                index++;
            }
            if (literal != String.Empty)
            {
                Logger.Info("Literal found: " + literal, "GenerateLiteralTokens::TokenGenerator");
                AddLiteralToken(ref literal, index, ref literalTokens);
            }
            return literalTokens;
        }

        /// <summary>
        /// This method generates a literal token based on the supplied literal string and index. It then adds
        /// the token to the list of literal tokens before emptying the literal string.
        /// </summary>
        /// <param name="literal">The supplied literal string</param>
        /// <param name="index">The index of the literal string</param>
        /// <param name="literalTokens">The list of literal tokens</param>
        private void AddLiteralToken(ref string literal, int index, ref List<Token> literalTokens)
        {
            literal = literal.Trim();
            TokenLiteral literalToken = new TokenLiteral(index - 1, literal);
            literalTokens.Add(literalToken);
            literal = String.Empty;
        }
        #endregion

        //@jenna A0083536B
        // ******************************************************************
        // Private Methods
        // ******************************************************************

        #region Private Helper Methods
        /// <summary>
        /// This method searches a string for a date match (alphabetic, numeric or just day with suffixes)
        /// and returns the match.
        /// </summary>
        /// <param name="theWord">The string to be searched/matched</param>
        /// <returns>The match found</returns>
        private Match GetDateMatch(string theWord)
        {
            Match theMatch = CustomDictionary.date_numericFormat.Match(theWord);
            if (!theMatch.Success)
            {
                theMatch = CustomDictionary.date_alphabeticFormat.Match(theWord);
            }
            if (!theMatch.Success)
            {
                theMatch = CustomDictionary.date_daysWithSuffixes.Match(theWord);
            }
            return theMatch;
        }

        /// <summary>
        /// This method retrieves the values of the day, month and year groups from an input match.
        /// </summary>
        /// <param name="match">The input match</param>
        /// <param name="day">The string value of the retrieved day group</param>
        /// <param name="month">The string value of the retrieved month group</param>
        /// <param name="year">The string value of the retrieved year group</param>
        private void GetMatchTagValues(Match match, ref string day, ref string month, ref string year)
        {
            day = match.Groups["day"].Value;
            month = match.Groups["month"].Value;
            year = match.Groups["year"].Value;
        }

        /// <summary>
        /// This methods convert the day, month and year strings into their equivalent integers.
        /// If the day and year strings are empty, they will be converted to zeroes.
        /// </summary>
        /// <param name="dayString">The input day string (may contain suffixes)</param>
        /// <param name="monthString">The input month string (may be numeric or alphabetical)</param>
        /// <param name="yearString">The input year string</param>
        /// <param name="dayInt">The output day integer</param>
        /// <param name="monthInt">The output month integer</param>
        /// <param name="yearInt">The output year integer</param>      
        private void ConvertDateStringsToInt(string dayString, string monthString, string yearString, ref int dayInt, ref int monthInt, ref int yearInt)
        {
            dayString = RemoveSuffixesIfRequired(dayString);
            int.TryParse(dayString, out dayInt);
            monthInt = ConvertToNumericMonth(monthString);
            int.TryParse(yearString, out yearInt);
        }

        /// <summary>
        /// This method removes the suffix from a specified day string if it exists and returns the
        /// shortened string.
        /// For example, both "15th" and "15" returns "15".
        /// </summary>
        /// <param name="day">The input day string (may contain suffixes)</param>
        /// <returns>The day string with no suffixes</returns>
        private string RemoveSuffixesIfRequired(string day)
        {
            // No day input
            if (day == String.Empty)
            {
                return day;
            }
            if (!Char.IsDigit(day.Last()))
            {
                day = day.Remove(day.Length - 2, 2);
            }
            return day;
        }

        /// <summary>
        /// This method takes in an input month string and returns its corresponding index as an integer.
        /// If alphabetic, the string is looked up and compared to a dictionary.
        /// For example, "january" or "jan" returns 1.
        /// A 0 is returned if the string is empty.
        /// </summary>
        /// <param name="month">The input month string (can be numeric of alphabetic)</param>
        /// <returns>An integer month index</returns>
        private int ConvertToNumericMonth(string month)
        {
            Month monthType;
            int monthInt = 0;
            bool success;
            if (month == String.Empty)
                return 0;
            if (Char.IsDigit(month[0]))
            {
                success = int.TryParse(month, out monthInt);
            }
            else if (CustomDictionary.monthKeywords.TryGetValue(month, out monthType))
            {
                monthInt = (int)monthType;
            }
            else Debug.Assert(false, "Conversion to numeric month failed! There should always be a valid month matched.");
            return monthInt;
        }

        /// <summary>
        /// This method tries to create a datetime given the supplied year, month and day.
        /// If the datetime specified by the arguments does not exist, a default datetime of day 1, month 1 and year
        /// 1 will be returned if the ignoreFailure flag is true. Else, an exception if thrown.
        /// </summary>
        /// <param name="year">The year of the datetime to be created</param>
        /// <param name="month">The month of the datetime to be created</param>
        /// <param name="day">The day of the datetime to be created</param>
        /// <param name="ignoreFailure">True if datetime creation failure is to be ignored; false if otherwise</param>
        /// <returns>The created datetime</returns>
        private DateTime TryParsingDate(int year, int month, int day, bool ignoreFailure)
        {
            DateTime date;
            try
            {
                date = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                Logger.Warning("Unable to parse date", "TryParsingDate::TokenGenerator");
                if (ignoreFailure)
                {
                    Logger.Info("Failed attempt to parse date was ignored", "TryParsingDate::TokenGenerator");
                    date = new DateTime(1, 1, 1);
                }
                else
                {
                    throw new InvalidDateTimeException("Invalid date input!\n" + day + "/" + month + "/" + year);
                }
            }
            return date;
        }

        /// <summary>
        /// This method checks through the list of tokens for conflicting tokens that were generated based on the
        /// same input text i.e. have the same indexes and deconflicts all conflicting tokens by selecting the
        /// appropriate tokens to keep.
        /// </summary>
        /// <param name="tokens">The list of tokens</param>
        private void DeconflictTokens(ref List<Token> tokens)
        {
            if (tokens.Count == 0)
            {
                return;
            }
            List<Token> deconflictedTokens = new List<Token>();
            bool conflictRemains = true;
            while (conflictRemains)
            {
                foreach (Token token in tokens)
                {
                    var matches = from eachToken in tokens
                                  where token.Position == eachToken.Position
                                  select eachToken;
                    var remainingTokens = from eachToken in tokens
                                          where !matches.Contains(eachToken)
                                          select eachToken;
                    if (matches.Count() > 1)
                    {
                        Logger.Info("Conflicting tokens found", "DeconflictTokens::TokenGenerator");
                        Token highestPriorityToken = GetHighestPriorityToken(matches);
                        deconflictedTokens.Add(highestPriorityToken);
                        deconflictedTokens.AddRange(remainingTokens);
                        break;
                    }
                    if (tokens.Last() == token)
                    {
                        conflictRemains = false;
                        deconflictedTokens = tokens;
                    }
                }
                tokens = deconflictedTokens;
            }
            return;
        }

        /// <summary>
        /// This method picks the token to be kept amongst a collection of conflicting tokens.
        /// </summary>
        /// <param name="matches">The collection of conflicting tokens</param>
        /// <returns>The highest priority token to be kept </returns>
        private Token GetHighestPriorityToken(IEnumerable<Token> matches)
        {
            Token highestPriorityToken = null;
            foreach (Token token in matches)
            {
                if (token.GetType() == typeof(TokenIndexRange))
                {
                    highestPriorityToken = token;
                }
                else if (token.GetType() == typeof(TokenTimeRange))
                {
                    highestPriorityToken = token;
                }
                else if (token.GetType() == typeof(TokenSortType))
                {
                    highestPriorityToken = token;
                }
                else if (highestPriorityToken == null)
                {
                    highestPriorityToken = token;
                }
            }
            return highestPriorityToken;
        }

        /// <summary>
        /// This methods compares the 2 input tokens by their stored integer positions and
        /// returns a -1 if the first input token's position is smaller than the second.
        /// A 1 is returned if the reverse is true.
        /// No 2 tokens should have the same positions. However, should such an error arise, a 0 is returned.
        /// </summary>
        /// <param name="x">The first token</param>
        /// <param name="y">The second token to be compared with</param>
        /// <returns>-1, 1, or 0, indicating the results of the comparison</returns>
        private int CompareByPosition(Token x, Token y)
        {
            int xPosition = x.Position;
            int yPosition = y.Position;
            if (xPosition < yPosition)
            {
                return -1;
            }
            else if (xPosition > yPosition)
            {
                return 1;
            }
            else
            {
                Debug.Assert(false, "Two tokens with same position!");
                return 0;
            }
        }

        /// <summary>
        /// This methods takes in a list of tokens and an index and returns the indicated token
        /// at that indicated position.
        /// </summary>
        /// <param name="tokens">The list of input tokens</param>
        /// <param name="p">The position of the required token</param>
        /// <returns>The retrieved token</returns>
        private Token GetTokenAtPosition(List<Token> tokens, int p)
        {
            foreach (Token token in tokens)
            {
                if (token.Position == p)
                {
                    return token;
                }
            }
            return null;
        }
        #endregion
    }
}