//@jenna A0083536B
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("ParsingLogicUnitTest")]

namespace ToDo
{
    // ******************************************************************
    // Enumerations
    // ******************************************************************
    public enum CommandType { ADD = 0, DELETE, DISPLAY, SORT, SEARCH, MODIFY, UNDO, REDO, DONE, UNDONE, POSTPONE, SCHEDULE, EXIT, INVALID };
    public enum ContextType { STARTTIME = 0, ENDTIME, DEADLINE, CURRENT, NEXT, FOLLOWING };
    public enum TimeRangeKeywordsType { MORNING, AFTERNOON, EVENING, NIGHT, NONE };
    public enum TimeRangeType { DEFAULT = 0, HOUR, DAY, WEEK, MONTH };
    public enum SortType { DEFAULT, NAME, DATE_TIME };
    public enum SearchType { NONE, DONE, UNDONE }
    public enum Month { JAN = 1, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC };

    static class CustomDictionary
    {
        public const int HOURS_IN_DAY = 24;
        public const int DAYS_IN_WEEK = 7;
        public const int DAYS_IN_MONTH = 30;

        static public int defaultScheduleTimeLength = 1;
        static public TimeRangeType defaultScheduleTimeLengthType = TimeRangeType.HOUR;
        static public int defaultPostponeDurationLength = 1;
        static public TimeRangeType defaultPostponeDurationType = TimeRangeType.DAY;
        static public Dictionary<string, CommandType> commandKeywords;
        static public Dictionary<string, ContextType> contextKeywords;
        static public Dictionary<string, TimeRangeKeywordsType> timeRangeKeywords;
        static public Dictionary<string, TimeRangeType> timeRangeType;
        static public Dictionary<TimeRangeKeywordsType, int> timeRangeKeywordsStartTime;
        static public Dictionary<TimeRangeKeywordsType, int> timeRangeKeywordsEndTime;
        static public Dictionary<string, int> timeSpecificKeywords;
        static public Dictionary<string, Month> monthKeywords;
        static public Dictionary<string, DayOfWeek> dayKeywords;
        static public Dictionary<string, SortType> sortTypeKeywords;
        static public List<string> timeSuffixes;
        static public List<string> todayKeywords;
        static public List<string> tomorrowKeywords;
        static public List<string> rangeAllKeywords;

        // ******************************************************************
        // Regular Expressions
        // ******************************************************************

        #region Regexes For Time & Date Parsing
        // matches 00:00 to 23:59 or 0000 to 2359, with or without hours. requires a leading zero if colon or dot is not specified.
        static public Regex time_24HourFormat =
            new Regex(@"(?i)^(?<hours>(?<flag>0)?[0-9]|(?<flag>1[0-9])|(?<flag>2[0-3]))(?(flag)(?:\.|:)?|(?:\.|:))(?<minutes>[0-5][0-9]) ?(h(ou)?rs?)?$");
        // matches the above but with AM and PM (case insensitive). colon/dot is optional.
        static public Regex time_12HourFormat =
            new Regex(@"(?i)^(?<hours>([0-9]|1[0-2]))(\.|:)?(?<minutes>[0-5][0-9])? ?(?<format>am|pm)$");
        // checks day-month-year and month-day-year format; the formal takes precedence if the input matches both
        static public Regex date_numericFormat =
            new Regex(@"^
                        (?:
                        (
                        # DD/MM
                        (?:
                        ((?<day>(0?[1-9]|[12][0-9]|3[01]))
                        (?<separator>[-/.]))?
                        (?<month>(0?[1-9]|1[012]))
                        )
                        |
                        # MM/DD
                        (?:
                        (?<month>(0?[1-9]|1[012]))
                        (?<separator>[-/.])
                        (?<day>(0?[1-9]|[12][0-9]|3[01]))
                        )
                        )
                        # (YY)YY
                        (?:(?(day)((\<separator>(?<year>(\d\d)?\d\d))?)
                        |([-/.](?<year>\d\d\d\d))
                        )
                        ))
                        $"
            , RegexOptions.IgnorePatternWhitespace);

        // checks day-month-year and month-day-year format; the formal takes precedence if the input matches both
        // note that inputs such as "15th" will not result in a match; need to recheck later
        static public Regex date_alphabeticFormat =
            new Regex(@"^
                        (
                        # DD/MM
                        (?:
                        ((?<day>(([23]?1(?:st)?)|(2?2(?:nd)?)|(2?3(?:rd)?)|([12]?[4-9](?:th)?)|([123]0(?:th)?)|(1[123](?:th)?)))\s)?
                        (?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))
                        )
                        |
                        # MM/DD
                        (?:
                        (?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?))
                        \s
                        (?<day>(([23]?1(?:st)?)|(2?2(?:nd)?)|(2?3(?:rd)?)|([12]?[4-9](?:th)?)|([123][0](?:th)?)|(1[123](?:th)?)))
                        ))
                        ,?
                        # (YY)YY
                        (?:(?(day)(\s(?<year>(\d\d)?\d\d))?|(\s(?<year>\d\d\d\d))))$"
            , RegexOptions.IgnorePatternWhitespace);

        static public Regex date_daysWithSuffixes =
             new Regex(@"^(?<day>(([23]?1(?:st))|(2?2(?:nd))|(2?3(?:rd))|([12]?[4-9](?:th))|([123][0](?:th))|(1[123](?:th))))$");

        static public Regex date_alphabeticMonth =
            new Regex(@"^(?<month>(jan(?:(uary))?|feb(?:(ruary))?|mar(?:(ch))?|apr(?:(il))?|may|jun(?:e)?|jul(?:y)?|aug((?:ust))?|sep((?:t|tember))?|oct((?:ober))?|nov((?:ember))?|dec((?:ember))?)),?$");

        static public Regex isNumericalRange =
            new Regex(@"^(((?<start>\d?\d?\d)(\-(?<end>\d?\d?\d))?)|((?<start>\d?\d?\d)\-)),?$");

        static public Regex isTimeRange =
               new Regex(@"^(?<index>(\d*) )?(?<type>(h(?:ou)?r(?:s)?|day(?:s)?|w(?:ee)?k(?:s)?|m(?:on)?th(?:s)?))$");
        #endregion

        static CustomDictionary()
        {
            InitializeCommandKeywords();
            InitializeContextKeywords();
            InitializeMonthKeywords();
            InitializeDateTimeKeywords();
            InitializeTimeRangeKeywords();
            InitializeSortTypeKeywords();
        }

        // ******************************************************************
        // Initialization Methods
        // ******************************************************************

        #region Initialization Methods
        private static void InitializeCommandKeywords()
        {
            commandKeywords = new Dictionary<string, CommandType>();
            commandKeywords.Add("add", CommandType.ADD);
            commandKeywords.Add("delete", CommandType.DELETE);
            commandKeywords.Add("default", CommandType.DISPLAY);
            commandKeywords.Add("sort", CommandType.SORT);
            commandKeywords.Add("show", CommandType.SEARCH);
            commandKeywords.Add("display", CommandType.SEARCH);
            commandKeywords.Add("search", CommandType.SEARCH);
            commandKeywords.Add("modify", CommandType.MODIFY);
            commandKeywords.Add("undo", CommandType.UNDO);
            commandKeywords.Add("redo", CommandType.REDO);
            commandKeywords.Add("done", CommandType.DONE);
            commandKeywords.Add("undone", CommandType.UNDONE);
            commandKeywords.Add("postpone", CommandType.POSTPONE);
            commandKeywords.Add("schedule", CommandType.SCHEDULE);
            commandKeywords.Add("exit", CommandType.EXIT);
        }

        private static void InitializeContextKeywords()
        {
            contextKeywords = new Dictionary<string, ContextType>();
            contextKeywords.Add("by", ContextType.DEADLINE);
            contextKeywords.Add("on", ContextType.STARTTIME);
            contextKeywords.Add("from", ContextType.STARTTIME);
            contextKeywords.Add("to", ContextType.ENDTIME);
            contextKeywords.Add("-", ContextType.ENDTIME);
            contextKeywords.Add("this", ContextType.CURRENT);
            contextKeywords.Add("next", ContextType.NEXT);
            contextKeywords.Add("nxt", ContextType.NEXT);
            contextKeywords.Add("following", ContextType.FOLLOWING);
        }

        private static void InitializeMonthKeywords()
        {
            monthKeywords = new Dictionary<string, Month>();
            monthKeywords.Add("jan", Month.JAN);
            monthKeywords.Add("january", Month.JAN);
            monthKeywords.Add("feb", Month.FEB);
            monthKeywords.Add("february", Month.FEB);
            monthKeywords.Add("mar", Month.MAR);
            monthKeywords.Add("march", Month.MAR);
            monthKeywords.Add("may", Month.MAY);
            monthKeywords.Add("jun", Month.JUN);
            monthKeywords.Add("june", Month.JUN);
            monthKeywords.Add("jul", Month.JUL);
            monthKeywords.Add("july", Month.JUL);
            monthKeywords.Add("aug", Month.AUG);
            monthKeywords.Add("august", Month.AUG);
            monthKeywords.Add("sep", Month.SEP);
            monthKeywords.Add("sept", Month.SEP);
            monthKeywords.Add("september", Month.SEP);
            monthKeywords.Add("oct", Month.OCT);
            monthKeywords.Add("october", Month.OCT);
            monthKeywords.Add("nov", Month.NOV);
            monthKeywords.Add("november", Month.NOV);
            monthKeywords.Add("dec", Month.DEC);
            monthKeywords.Add("december", Month.DEC);
        }

        private static void InitializeDateTimeKeywords()
        {
            dayKeywords = new Dictionary<string, DayOfWeek>();
            dayKeywords.Add("mon", DayOfWeek.Monday);
            dayKeywords.Add("monday", DayOfWeek.Monday);
            dayKeywords.Add("tue", DayOfWeek.Tuesday);
            dayKeywords.Add("tues", DayOfWeek.Tuesday);
            dayKeywords.Add("tuesday", DayOfWeek.Tuesday);
            dayKeywords.Add("wed", DayOfWeek.Wednesday);
            dayKeywords.Add("wednesday", DayOfWeek.Wednesday);
            dayKeywords.Add("thur", DayOfWeek.Thursday);
            dayKeywords.Add("thurs", DayOfWeek.Thursday);
            dayKeywords.Add("thursday", DayOfWeek.Thursday);
            dayKeywords.Add("fri", DayOfWeek.Friday);
            dayKeywords.Add("friday", DayOfWeek.Friday);
            dayKeywords.Add("sat", DayOfWeek.Saturday);
            dayKeywords.Add("saturday", DayOfWeek.Saturday);
            dayKeywords.Add("sun", DayOfWeek.Sunday);
            dayKeywords.Add("sunday", DayOfWeek.Sunday);
            dayKeywords.Add("weekend", DayOfWeek.Sunday);
            // NYI
            timeSuffixes = new List<string> { "am", "pm", "hr", "hrs", "hour", "hours" };
            timeSpecificKeywords = new Dictionary<string, int>();
            timeSpecificKeywords.Add("noon", 12);
            timeSpecificKeywords.Add("midnight", 0);
            todayKeywords = new List<string> { "today" };
            tomorrowKeywords = new List<string> { "tmr", "tomorrow" };
            rangeAllKeywords = new List<string> { "all" };
        }

        private static void InitializeTimeRangeKeywords()
        {
            timeRangeKeywords = new Dictionary<string, TimeRangeKeywordsType>();
            timeRangeKeywords.Add("morning", TimeRangeKeywordsType.MORNING);
            timeRangeKeywords.Add("morn", TimeRangeKeywordsType.MORNING);
            timeRangeKeywords.Add("afternoon", TimeRangeKeywordsType.AFTERNOON);
            timeRangeKeywords.Add("evening", TimeRangeKeywordsType.EVENING);
            timeRangeKeywords.Add("night", TimeRangeKeywordsType.NIGHT);
            timeRangeType = new Dictionary<string, TimeRangeType>();
            timeRangeType.Add("hours", TimeRangeType.HOUR);
            timeRangeType.Add("hour", TimeRangeType.HOUR);
            timeRangeType.Add("hrs", TimeRangeType.HOUR);
            timeRangeType.Add("hr", TimeRangeType.HOUR);
            timeRangeType.Add("days", TimeRangeType.DAY);
            timeRangeType.Add("day", TimeRangeType.DAY);
            timeRangeType.Add("week", TimeRangeType.WEEK);
            timeRangeType.Add("wk", TimeRangeType.WEEK);
            timeRangeType.Add("weeks", TimeRangeType.WEEK);
            timeRangeType.Add("wks", TimeRangeType.WEEK);
            timeRangeType.Add("months", TimeRangeType.MONTH);
            timeRangeType.Add("month", TimeRangeType.MONTH);
            timeRangeType.Add("mths", TimeRangeType.MONTH);
            timeRangeType.Add("mth", TimeRangeType.MONTH);
            timeRangeKeywordsStartTime = new Dictionary<TimeRangeKeywordsType, int>();
            timeRangeKeywordsStartTime.Add(TimeRangeKeywordsType.MORNING, 5);
            timeRangeKeywordsStartTime.Add(TimeRangeKeywordsType.AFTERNOON, 12);
            timeRangeKeywordsStartTime.Add(TimeRangeKeywordsType.EVENING, 17);
            timeRangeKeywordsStartTime.Add(TimeRangeKeywordsType.NIGHT, 22);
            timeRangeKeywordsEndTime = new Dictionary<TimeRangeKeywordsType, int>();
            timeRangeKeywordsEndTime.Add(TimeRangeKeywordsType.MORNING, 12);
            timeRangeKeywordsEndTime.Add(TimeRangeKeywordsType.AFTERNOON, 17);
            timeRangeKeywordsEndTime.Add(TimeRangeKeywordsType.EVENING, 22);
            timeRangeKeywordsEndTime.Add(TimeRangeKeywordsType.NIGHT, 5);
        }

        private static void InitializeSortTypeKeywords()
        {
            sortTypeKeywords = new Dictionary<string, SortType>();
            sortTypeKeywords.Add("name", SortType.NAME);
            sortTypeKeywords.Add("date", SortType.DATE_TIME);
        }

        #endregion

        // ******************************************************************
        // Getter Methods
        // ******************************************************************

        #region Getter Methods For Dictionaries & List
        public static Dictionary<string, CommandType> GetCommandKeywords()
        {
            return commandKeywords;
        }

        public static Dictionary<string, ContextType> GetContextKeywords()
        {
            return contextKeywords;
        }

        public static Dictionary<string, TimeRangeKeywordsType> GetTimeRangeKeywordKeywords()
        {
            return timeRangeKeywords;
        }

        public static Dictionary<string, TimeRangeType> GetTimeRangeKeywords()
        {
            return timeRangeType;
        }

        public static Dictionary<string, SortType> GetSortTypeKeywords()
        {
            return sortTypeKeywords;
        }

        public static Dictionary<TimeRangeKeywordsType, int> GetTimeRangeStartTime()
        {
            return timeRangeKeywordsStartTime;
        }

        public static Dictionary<TimeRangeKeywordsType, int> GetTimeRangeEndTime()
        {
            return timeRangeKeywordsEndTime;
        }
        #endregion

        // ******************************************************************
        // Auxilliary Methods
        // ******************************************************************

        #region Comparison Methods
        public static bool IsToday(string word)
        {
            if (todayKeywords.Exists(e => (e == word)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsTomorrow(string word)
        {
            if (tomorrowKeywords.Exists(e => (e == word)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDate(string theDate)
        {
            return IsValidNumericDate(theDate) || IsValidAlphabeticDate(theDate);
        }

        public static bool IsValidTime(string theTime)
        {
            return time_24HourFormat.IsMatch(theTime) || time_12HourFormat.IsMatch(theTime);
        }

        // Note that the following methods do not validate that the dates do actually exist.
        // i.e. does not check for erroneous non-existent dates such as 31st feb
        public static bool IsValidNumericDate(string theDate)
        {
            return date_numericFormat.IsMatch(theDate) || date_daysWithSuffixes.IsMatch(theDate);
        }

        public static bool IsValidAlphabeticDate(string theDate)
        {
            return date_alphabeticFormat.IsMatch(theDate);
        }

        public static bool IsValidMonthWord(string theWord)
        {
            return date_alphabeticMonth.IsMatch(theWord);
        }

        public static bool IsTimeRange(string theWord)
        {
            return isTimeRange.IsMatch(theWord);
        }
        
        /// <summary>Checks if a word is a time keyword and returns a boolean indicating whether it is.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>True if the word is a time keyword, false if otherwise</returns>
        public static bool IsWordTimeSuffix(string word)
        {
            foreach (string keyword in timeSuffixes)
            {
                if (word.ToLower() == keyword)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a time range crosses the day boundary and returns a boolean indicating whether it does.
        /// </summary>
        /// <param name="timeRangeKeyword"></param>
        /// <returns>True if positive; false if otherwise</returns>
        public static bool IsTimeRangeOverDayBoundary(TimeRangeKeywordsType timeRangeKeyword)
        {
            int timeRangeStartHour, timeRangeEndHour;
            timeRangeKeywordsStartTime.TryGetValue(timeRangeKeyword, out timeRangeStartHour);
            timeRangeKeywordsEndTime.TryGetValue(timeRangeKeyword, out timeRangeEndHour);
            if (timeRangeEndHour <= timeRangeStartHour)
            {
                return true;
            }
            return false;
        }
        #endregion

        // ******************************************************************
        // Updates Command and Context Keywords from FlexiCommands
        // ******************************************************************

        #region Update Dictionary With FlexiCommands
        /// <summary>
        /// Updates the CustomDictionary keywords with new Dictionaries.
        /// </summary>
        public static void UpdateDictionary(
            Dictionary<string, CommandType> passedCommandKeywords,
            Dictionary<string, ContextType> passedContextKeywords,
            Dictionary<string, TimeRangeKeywordsType> passedTimeRangeKeywords, 
            Dictionary<string, TimeRangeType> passedTimeRangeType,
            Dictionary<TimeRangeKeywordsType, int> passedTimeRangeStartTime,
            Dictionary<TimeRangeKeywordsType, int> passedTimeRangeEndTime)
        {
            commandKeywords = passedCommandKeywords;
            contextKeywords = passedContextKeywords;
            timeRangeKeywords = passedTimeRangeKeywords;
            timeRangeType = passedTimeRangeType;
            timeRangeKeywordsStartTime = passedTimeRangeStartTime;
            timeRangeKeywordsEndTime = passedTimeRangeEndTime;
        }
        #endregion
    }
}