//@jenna A0083536B
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    /// <summary>
    /// Parser class which breaks / merges the strings into tokenable substrings
    /// so that a TokenGenerator can process them.     
    /// </summary>
    public class StringParser
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        const int START_INDEX = 0;
        const int END_INDEX = 1;
        static char[,] delimitingCharacters = { { '\'', '\'' }, { '\"', '\"' }, { '[', ']' }, { '(', ')' }, { '{', '}' } };
        
        // ******************************************************************
        // Public Methods   
        // ******************************************************************

        #region Public Methods
        /// <summary>
        /// This method parses a string of words into a list of substrings determined by their meaning,
        /// by spacing, or by delimiting characters.
        /// </summary>
        /// <param name="input">The string of words to be parsed</param>
        /// <returns>The list of tokens</returns>
        public List<string> ParseStringIntoWords(string input)
        {
            List<int[]> indexOfDelimiters = FindPositionOfDelimiters(input);
            List<string> words = SplitStringIntoSubstrings(input, indexOfDelimiters);
            Logger.Info("Successfully split string into substrings", "ParseStringIntoTokens::StringParser");
            return words;
        }
        
        /// <summary>
        /// This method marks each and every word within the input string (as absolute) with a
        /// pair of inverted commas at the start and end of the word
        /// </summary>
        /// <param name="absoluteSubstr">The input string of words containing all of the words to be marked</param>
        /// <returns>The marked string of words</returns>
        public static string MarkWordsAsAbsolute(string absoluteSubstr)
        {
            string[] words = absoluteSubstr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
            string output = "";
            foreach (string word in words)
            {
                output += "\"" + word + "\" ";
            }
            output.TrimEnd();
            return output;
        }

        /// <summary>
        /// This method unmarks each and every word within the input string. The words were originally marked
        /// by a pair of inverted commas.
        /// </summary>
        /// <param name="absoluteSubstr">The input string of words containing all of the words to be unmarked</param>
        /// <returns>The unmarked string of words</returns>
        public static string UnmarkWordsAsAbsolute(string absoluteSubstr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in absoluteSubstr)
            {
                if (c != '\"')
                    sb.Append(c);
            }
            return sb.ToString();
        }
        #endregion

        // ******************************************************************
        // String Parsing Algorithms
        // ******************************************************************

        #region String Splitting and Merging Methods
        /// <summary>
        /// This method splits a string and returns a list of substrings, each containing either a word delimited by a
        /// space, or a substring delimited by positions in the parameter indexOfDelimiters.
        /// </summary>
        /// <param name="input">The string of words to be split</param>
        /// <param name="indexOfDelimiters">The position in the string where delimiting characters mark the absolute substrings</param>
        /// <returns>List of substrings</returns>
        private List<string> SplitStringIntoSubstrings(string input, List<int[]> indexOfDelimiters)
        {
            List<string> words = new List<string>();
            int processedIndex = 0, removedCount = 0;

            if (indexOfDelimiters == null)
                return input.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (int[] substringIndex in indexOfDelimiters)
            {
                int count = substringIndex[END_INDEX] - substringIndex[START_INDEX] + 1;
                int startIndex = substringIndex[START_INDEX];
                string subStr = input.Substring(processedIndex, startIndex - processedIndex);

                // Add words leading up to the delimiting character
                words.AddRange(subStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());

                // Get absolute substring without the delimiter characters and add to return list
                string absoluteSubstr = input.Substring(startIndex + 1, count - 2);
                absoluteSubstr = MarkWordsAsAbsolute(absoluteSubstr);
                words.Add(absoluteSubstr);

                // Update processed index state and count of removed characters
                processedIndex = substringIndex[END_INDEX] + 1;
                removedCount += count;
            }
            // Add remaining words
            string remainingStr = input.Substring(processedIndex);
            words.AddRange(remainingStr.Split(null as string[], StringSplitOptions.RemoveEmptyEntries).ToList());            
            words = MergeDateAndTimeWords(words);
            words = MergeNumericalRangeWords(words);
            words = MergeTimeRangeWords(words);
            return words;
        }

        /// <summary>
        /// This method merges numerical words separated by a hyphen into one word.
        /// </summary>
        /// <param name="words">The list of strings to check through</param>
        /// <returns>The list of strings with merged numerical range words</returns>
        private List<string> MergeNumericalRangeWords(List<string> words)
        {               
            int j = 1;
            List<string> output = new List<string>();
            for (int i = 0; i < words.Count; i++)
            {
                bool success = true;
                string matchCheck = words[i];
                if (j > 1)
                {
                    j--;
                    continue;
                }
                while (i + j < words.Count)  // Don't check last word.
                {
                    success = CustomDictionary.isNumericalRange.IsMatch(matchCheck + words[i + j]);
                    if (success)
                    {
                        if (AdjacentCharsAreNumerical(words, i, j))
                        {
                            break;
                        }
                        matchCheck += words[i + j];
                        Logger.Info("Numerical range words (" + matchCheck + ") and merged", "MergeNumericalRangeWords::StringParser");
                    }
                    else break;
                    j++;
                }                
                output.Add(matchCheck);
            }
            return output;
        }

        /// <summary>
        /// This method checks if the end characters of the 2 specified words in a list of strings
        /// are both numerals.
        /// </summary>
        /// <param name="words">The input list of strings</param>
        /// <param name="i">The index of the base word in the list of strings</param>
        /// <param name="j">The number of words from the base word where the second of the 2 specified words is</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool AdjacentCharsAreNumerical(List<string> words, int i, int j)
        {            
            int temp;
            if (i + j - 1 >= 0)
            {
                try
                {
                    string firstWord = words[i + j - 1];
                    string secondWord = words[i + j];
                    string firstEndChar = firstWord.Substring(firstWord.Length - 1, 1);
                    string secondEndChar = secondWord.Substring(secondWord.Length - 1, 1);
                    if (Int32.TryParse(firstEndChar, out temp) && Int32.TryParse(secondEndChar, out temp))
                    {
                        return true;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Logger.Error(ex, "AdjacentCharsAreNumerical::StringParser");
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// This method merges time range amounts with time range keywords
        /// i.e. "3" and "days" are merged to become "3 days".
        /// </summary>
        /// <param name="words">The list of strings to check through</param>
        /// <returns>The list of strings with merged time range words</returns>
        private List<string> MergeTimeRangeWords(List<string> words)
        {
            List<string> output = new List<string>();
            int index = 0;
            foreach (string word in words)
            {
                string mergedWord = null;
                int userDefinedIndex = 0;
                if (index > 1 && CustomDictionary.IsTimeRange(word.ToLower()))
                {
                    if (int.TryParse(words[index - 1], out userDefinedIndex))
                    {
                        mergedWord = String.Concat(words[index - 1], " ", word);
                    }
                }
                if (mergedWord != null)
                {
                    Logger.Info("Time range words  (" + mergedWord + ") found and merged", "MergeTimeRangeWords::StringParser");
                    output.RemoveAt(output.Count-1);
                    output.Add(mergedWord);
                }
                else
                {
                    output.Add(word);
                }
                index++;
            }
            return output;
        }

        /// <summary>
        /// This method detects and merges all the date and time words into a single string
        /// while keeping the other words separate and unmerged.
        /// For example, the list input "add", "task", "friday", "5", "pm", "28", "sept", "2012"
        /// will return "add", "task", "friday", "5pm", "28 sept 2012"
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time/date phrases</returns>
        private List<string> MergeDateAndTimeWords(List<string> input)
        {
            input = MergeTimeWords(input);
            input = MergeDateWords(input);
            return input;
        }

        // ******************************************************************
        // Time Merging Methods   
        // ******************************************************************

        #region Time merging methods
        /// <summary>
        /// This method checks all words within an input list of words for valid times and returns a list of words
        /// where all times are merged as a single word.
        /// For example, if there is a valid time such as i.e. 5 pm, it combines "5" and "pm" in the returned list of words as "5pm".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged time phrases</returns>
        private List<string> MergeTimeWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0;
            foreach (string word in input)
            {
                bool isWordAdded = false;
                if (CustomDictionary.IsWordTimeSuffix(word))
                {
                    isWordAdded = MergeWord_IfValidTime(ref output, input, position);
                }
                if (!isWordAdded)
                {
                    output.Add(word);
                }
                position++;
            }
            return output;
        }

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of a time phrase
        /// and merges it with the other words constituting the time phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged time phrases up to the indicated word/time phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word in the input list to be checked</param>
        /// <returns>True if the indicated word is part of a time phrase and false if otherwise</returns>
        private bool MergeWord_IfValidTime(ref List<string> output, List<string> input, int position)
        {
            string backHalf = input.ElementAt(position);
            string frontHalf;
            if (position == 0)
            {
                return false;
            }
            frontHalf = input.ElementAt(position - 1);
            string mergedWord = String.Concat(frontHalf, " ", backHalf);
            if (CustomDictionary.IsValidTime(mergedWord))
            {
                Logger.Info("Valid time word (" + mergedWord + ") found and merged", "MergeWord_IfValidTime::StringParser");
                output.RemoveAt(output.Count - 1);
                output.Add(mergedWord);
                return true;
            }
            else return false;
        }
        #endregion

        // ******************************************************************
        // Date Merging Methods   
        // ******************************************************************

        #region Date merging methods
        /// <summary>
        /// This method checks all words within an input list of words for valid date and returns a list of words
        /// where all dates are merged as a single word.
        /// For example, if there is a valid time such as i.e. 23 sept 2012, it combines "23", "sept" and "2012"
        /// in the returned list of words as "23 sept 2012".
        /// </summary>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <returns>List of separate words or merged date phrases</returns>
        private List<string> MergeDateWords(List<string> input)
        {
            List<string> output = new List<string>();
            int position = 0, skipWords = 0;
            bool isWordAdded = false;
            foreach (string word in input)
            {
                // skip word if it has been combined with the last determined date keyword into a date phrase
                if (skipWords > 0)
                {
                    skipWords--;
                    position++;
                    continue;
                }
                if (CustomDictionary.monthKeywords.ContainsKey(word.ToLower()))
                {
                    isWordAdded = MergeWord_IfValidAlphabeticDate(ref output, input, position, ref skipWords);
                }
                if (!isWordAdded)
                {
                    output.Add(word);
                }
                isWordAdded = false;
                position++;
            }
            // dates in numeric date formats and dates that are only specified by day with suffixes i.e. "15th"
            // need not be checked for and merged since they are already whole words on their own.
            return output;
        }

        /* Note that "12 may 2012 2012" will produce merged word "12 may 2012"
        * and "12 may 23 2012" will produce the merged word "12 may 23".
        */

        /// <summary>
        /// This method checks if the indicated word in a list of string is part of an alphabetic date phrase
        /// and merges it with the other words constituting the date phrase into one string if it is.
        /// </summary>
        /// <param name="output">The list of words and merged alphabetic date phrases up to the indicated date phrase</param>
        /// <param name="input">The list of unmerged delimited words</param>
        /// <param name="position">The index of the word  in the input list to be checked</param>
        /// <param name="numberOfWords">The number of words behind the indicated word that were merged to form the date</param>
        /// <returns>True if the indicated word is part of a date phrase and false if otherwise</returns>
        private bool MergeWord_IfValidAlphabeticDate(ref List<string> output, List<string> input, int position, ref int numberOfWords)
        {
            string month = input.ElementAt(position);
            string mergedWord = month;
            bool isWordUsed = false;
            int i = 1;
            // Backward check
            if ((position > 0) &&
                (CustomDictionary.IsValidAlphabeticDate(input[position - 1] + " " + mergedWord.ToLower())))
            {
                mergedWord = input[position - 1] + " " + mergedWord;
                isWordUsed = true;
            }
            // Forward check
            while (position + i < input.Count)
            {
                if (CustomDictionary.IsValidAlphabeticDate(mergedWord.ToLower() + " " + input[position + i]))
                {
                    mergedWord = mergedWord + " " + input[position + i];
                }
                else break;
                i++;
            }
            if (mergedWord == month)
            {
                return false;
            }
            if (isWordUsed == true)
            {
                output.RemoveAt(output.Count - 1);
            }
            Logger.Info("Valid alphabetic word (" + mergedWord + ") found and merged", "MergeWord_IfValidAlphabeticDate::StringParser");
            output.Add(mergedWord);
            numberOfWords = i - 1;
            return true;
        }
        #endregion

        // ******************************************************************
        // Delimter Search Methods   
        // ******************************************************************

        #region Delimiter searching methods
        /// <summary>
        /// This method searches the input string against the set delimiters'
        /// and return the positions of the delimiters as a list of integer pairs.
        /// </summary>
        /// <param name="input">The input string to be checked</param>
        /// <returns>List containing all matching sets of delimiters as integer pairs</returns>
        private List<int[]> FindPositionOfDelimiters(string input)
        {
            List<int[]> indexOfDelimiters = new List<int[]>();
            int startIndex = 0, endIndex = -1;
            for (int i = 0; i < delimitingCharacters.GetLength(0); i++)
            {
                startIndex = 0;
                endIndex = -1;
                do
                {
                    startIndex = input.IndexOf(delimitingCharacters[i, START_INDEX], endIndex + 1);
                    endIndex = input.IndexOf(delimitingCharacters[i, END_INDEX], startIndex + 1);
                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        int[] index = new int[2] { startIndex, endIndex };
                        indexOfDelimiters.Add(index);
                    }
                    else break;
                } while (true);

            }
            indexOfDelimiters = indexOfDelimiters.OrderBy(e => e[0]).ToList();
            return indexOfDelimiters;
        }

        /// <summary>
        /// This method checks each pair of indexes in a List and removes those
        /// that overlaps with the previous pair.
        /// </summary>
        /// <param name="indexOfDelimiters"></param>
        /// <returns></returns>
        private void RemoveBadIndexes(ref List<int[]> indexOfDelimiters)
        {
            int previousEndIndex = -1;
            List<int[]> indexesToRemove = new List<int[]>();
            foreach (int[] set in indexOfDelimiters)
            {
                if (set[START_INDEX] < previousEndIndex)
                {
                    indexesToRemove.Add(set);
                }
                previousEndIndex = set[END_INDEX];
            }
            indexOfDelimiters.RemoveAll(x => indexesToRemove.Contains(x));
        }
        #endregion

        #endregion
    }
}
