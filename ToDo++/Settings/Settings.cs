//@raaj A0081202y
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ToDo
{
    public class Settings
    {
        // ******************************************************************
        // Static Keyword Declarations
        // ******************************************************************

        static SettingInformation settingInfo;

        public Settings()
        {
            InitializeSettings();
        }

        // ******************************************************************
        // Initialization and opening of settings file
        // ******************************************************************

        private void InitializeSettings()
        {
            settingInfo = new SettingInformation();
        }

        /// <summary>
        /// Completely wipes and re-updates Settings Data
        /// </summary>
        /// <param name="updatedInfo">Pass in an instance of SettingsInformation</param>
        public void UpdateSettings(SettingInformation updatedInfo)
        {
            settingInfo = updatedInfo;
            CustomDictionary.UpdateDictionary(settingInfo.userCommandKeywords, settingInfo.userContextKeywords, settingInfo.userTimeRangeKeywordsType, settingInfo.userTimeRangeType, settingInfo.userTimeRangeKeywordsStartTime, settingInfo.userTimeRangeKeywordsEndTime);
            UpdateDictionaryPostponeSchedule();
        }

        // ******************************************************************
        // Getters/Setters
        // ******************************************************************

        #region GettersSetters

        #region MiscGetterSetters

        /// <summary>
        /// Gets whether this is the first time loading ToDo++. Once gotten, it is set to false
        /// </summary>
        /// <returns>First load status</returns>
        public bool GetFirstLoadStatus()
        {
            bool firstLoad=settingInfo.misc.FirstLoad;
            settingInfo.misc.FirstLoad = false;
            EventHandlers.UpdateSettings(settingInfo);
            return firstLoad;
        }

        /// <summary>
        /// Set default text size of Task View
        /// </summary>
        /// <param name="size">text size</param>
        public void SetTextSize(int size) 
        { 
            settingInfo.misc.TextSize = size; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set default text size to {0}..", size);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get the text size of Task View
        /// </summary>
        /// <returns>Text size of Task View</returns>
        public int GetTextSize() { return settingInfo.misc.TextSize; }


        /// <summary>
        /// Sets the load on startup status
        /// </summary>
        /// <param name="status">set status</param>
        public void SetLoadOnStartupStatus(bool status) 
        { 
            settingInfo.misc.LoadOnStartup = status; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set load on startup to {0}..", status);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get the load on startup status
        /// </summary>
        /// <returns>load on startup status</returns>
        public bool GetLoadOnStartupStatus() { return settingInfo.misc.LoadOnStartup; }

        /// <summary>
        /// Set start minimized status
        /// </summary>
        /// <param name="status">start minized status</param>
        public void SetStartMinimized(bool status) 
        { 
            settingInfo.misc.StartMinimized = status; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set start minimized status to {0}..", status);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get the start minimized status
        /// </summary>
        /// <returns>start Minimized status</returns>
        public bool GetStartMinimizeStatus() { return settingInfo.misc.StartMinimized; }

        /// <summary>
        /// Set stay on top status
        /// </summary>
        /// <param name="status">stay on top status</param>
        public void SetStayOnTop(bool status) 
        { 
            settingInfo.misc.StayOnTop = status; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set stay on top to {0}..", status);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get stay on top status
        /// </summary>
        /// <returns>stay on top status</returns>
        public bool GetStayOnTopStatus() { return settingInfo.misc.StayOnTop; }

        /// <summary>
        /// Set Task View font
        /// </summary>
        /// <param name="font">default font</param>
        public void SetFontSelection(string font) 
        { 
            settingInfo.misc.FontSelection = font; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set font to {0}..", font);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Gets Task View font
        /// </summary>
        /// <returns>task view font</returns>
        public string GetFontSelection() { return settingInfo.misc.FontSelection; }

        #endregion

        #region TaskColorsGetterSetters

        /// <summary>
        /// Set task done color
        /// </summary>
        /// <param name="col">task done color</param>
        public void SetTaskDoneColor(Color col) 
        { 
            settingInfo.misc.TaskDoneColor = col; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set task done color to {0}..", col.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get task done color
        /// </summary>
        /// <returns>task done color</returns>
        public Color GetTaskDoneColor() { return settingInfo.misc.TaskDoneColor; }

        /// <summary>
        /// Set task missed deadline color
        /// </summary>
        /// <param name="col">task missed deadline color</param>
        public void SetTaskMissedDeadlineColor(Color col) 
        { 
            settingInfo.misc.TaskMissedDeadlineColor = col; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set task missed deadline color to {0}..", col.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get task missed deadline color
        /// </summary>
        /// <returns>task missed deadline color</returns>
        public Color GetTaskMissedDeadlineColor() { return settingInfo.misc.TaskMissedDeadlineColor; }

        /// <summary>
        /// Set task nearing deadline color
        /// </summary>
        /// <param name="col">task nearing deadline color</param>
        public void SetTaskNearingDeadlineColor(Color col) 
        { 
            settingInfo.misc.TaskNearingDeadlineColor = col; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set task nearing deadline color to {0}..", col.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get task nearing deadline color
        /// </summary>
        /// <returns>task nearing deadline color</returns>
        public Color GetTaskNearingDeadlineColor() { return settingInfo.misc.TaskNearingDeadlineColor; }

        /// <summary>
        /// Set task over color
        /// </summary>
        /// <param name="col">task over color</param>
        public void SetTaskOverColor(Color col) 
        { 
            settingInfo.misc.TaskOverColor = col; 
            EventHandlers.UpdateSettings(settingInfo);
            string loggerString = string.Format("User set task over color to {0}..", col.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get task over color
        /// </summary>
        /// <returns>task over color</returns>
        public Color GetTaskOverColor() { return settingInfo.misc.TaskOverColor; }

        #endregion

        #region TimeRangeTimeTypes

        /// <summary>
        /// Set default time length for Command SCHEDULE
        /// </summary>
        /// <param name="length">default schedule time length</param>
        public void SetDefaultScheduleTimeLength(int length) 
        { 
            settingInfo.misc.DefaultScheduleTimeLength = length; 
            EventHandlers.UpdateSettings(settingInfo); 
            UpdateDictionaryPostponeSchedule();
            string loggerString = string.Format("User set default schedule time length to {0}..", length);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get default time length for Command SCHEDULE
        /// </summary>
        /// <returns>Get default time length</returns>
        public int GetDefaultScheduleTimeLength() { return settingInfo.misc.DefaultScheduleTimeLength; }

        /// <summary>
        /// Set default duration length for Command POSTPONE
        /// </summary>
        /// <param name="length">default duration length</param>
        public void SetDefaultPostponeDurationLength(int length) 
        { 
            settingInfo.misc.DefaultPostponeDurationLength = length; 
            EventHandlers.UpdateSettings(settingInfo); 
            UpdateDictionaryPostponeSchedule();
            string loggerString = string.Format("User set default postpone time duration to {0}..", length);
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get default duration length for Command POSTPONE
        /// </summary>
        /// <returns>default duration length</returns>
        public int GetDefaultPostponeDurationLength() { return settingInfo.misc.DefaultPostponeDurationLength; }

        /// <summary>
        /// Set default time length type (HOUR,DAY etc.) for Command SCHEDULE
        /// </summary>
        /// <param name="timeRange">default time length type</param>
        public void SetDefaultScheduleTimeLengthType(TimeRangeType timeRange) 
        { 
            settingInfo.misc.DefaultScheduleTimeLengthType = timeRange; 
            EventHandlers.UpdateSettings(settingInfo); 
            UpdateDictionaryPostponeSchedule();
            string loggerString = string.Format("User set default schedule time length type to {0}..", timeRange.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get default time length type (HOUR,DAY etc.) for Command SCHEDULE
        /// </summary>
        /// <returns>default time length type</returns>
        public TimeRangeType GetDefaultScheduleTimeLengthType() { return settingInfo.misc.DefaultScheduleTimeLengthType; }

        /// <summary>
        /// Set default duration type (HOUR,DAY etc.) for Command POSTPONE
        /// </summary>
        /// <param name="timeRange">default duration type</param>
        public void SetDefaultPostponeDurationType(TimeRangeType timeRange) 
        { 
            settingInfo.misc.DefaultPostponeDurationType = timeRange; 
            EventHandlers.UpdateSettings(settingInfo); 
            UpdateDictionaryPostponeSchedule();
            string loggerString = string.Format("User set default postpone duration type to {0}..", timeRange.ToString());
            Logger.Info(loggerString, "Logc::Settings");
        }

        /// <summary>
        /// Get default duration type (HOUR,DAY etc.) for Command POSTPONE
        /// </summary>
        /// <returns>default duration type</returns>
        public TimeRangeType GetDefaultPostponeDurationType() { return settingInfo.misc.DefaultPostponeDurationType; }

        #endregion

        private void UpdateDictionaryPostponeSchedule()
        {
            CustomDictionary.defaultScheduleTimeLength = this.GetDefaultScheduleTimeLength();
            CustomDictionary.defaultPostponeDurationLength = this.GetDefaultPostponeDurationLength();
            CustomDictionary.defaultScheduleTimeLengthType = this.GetDefaultScheduleTimeLengthType();
            CustomDictionary.defaultPostponeDurationType = this.GetDefaultPostponeDurationType();
        }

        #endregion;

        // ******************************************************************
        // Keyword Operations
        // ******************************************************************

        #region KeywordOperations

        #region CommandKeywords

        private bool ContainsRepeatKeywords(string newKeyword)
        {
            if (settingInfo.userContextKeywords.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userCommandKeywords.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userTimeRangeKeywordsType.ContainsKey(newKeyword))
                return false;
            if (settingInfo.userTimeRangeType.ContainsKey(newKeyword))
                return false;

            return true;
        }

        /// <summary>
        /// This method adds a new Command to the list of available commands
        /// If a command repeats itself, an exception will be thrown
        /// </summary>
        /// <param name="newKeyword">New Command that is to be added</param>
        /// <param name="flexiCommandType">Specify to which CommandType it is being added to</param>
        public void AddFlexiKeyword(string newKeyword, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType commandType = (CommandType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, commandType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userCommandKeywords.Add(newKeyword, commandType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType contexType = (ContextType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, contexType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userContextKeywords.Add(newKeyword, contexType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType timeRangeKeywordsType = (TimeRangeKeywordsType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, timeRangeKeywordsType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userTimeRangeKeywordsType.Add(newKeyword, timeRangeKeywordsType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType timeRangeType = (TimeRangeType)flexiCommandType;
                        if (settingInfo.ContainsFlexiCommandKeyword(newKeyword, timeRangeType))
                            throw new RepeatCommandException("There is such a command in the list already");
                        if (!ContainsRepeatKeywords(newKeyword))
                            throw new RepeatCommandException("There is such a command in other lists");

                        settingInfo.userTimeRangeType.Add(newKeyword, timeRangeType);
                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }
            }

        }

        /// <summary>
        /// This method removes the specified command.
        /// </summary>
        /// <param name="keywordToRemove">The keyword to remove.</param>
        /// <param name="flexiCommandType">The command type that is being removed.</param>
        public void RemoveFlexiKeyword(string keywordToRemove, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        if (keywordToRemove == "add" || keywordToRemove == "delete" || keywordToRemove == "display" || keywordToRemove == "sort"
                        || keywordToRemove == "search" || keywordToRemove == "modify" || keywordToRemove == "undo" || keywordToRemove == "redo"
                        || keywordToRemove == "done" || keywordToRemove == "undone" || keywordToRemove == "postpone")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userCommandKeywords.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.ContextType":
                    {
                        if (keywordToRemove == "by" || keywordToRemove == "on" || keywordToRemove == "from" || keywordToRemove == "to"
                        || keywordToRemove == "-" || keywordToRemove == "this" || keywordToRemove == "next" || keywordToRemove == "nxt"
                        || keywordToRemove == "following")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userContextKeywords.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        if (keywordToRemove == "morning" || keywordToRemove == "morn" || keywordToRemove == "afternoon" || keywordToRemove == "evening"
                        || keywordToRemove == "night")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userTimeRangeKeywordsType.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }

                case "ToDo.TimeRangeType":
                    {
                        if (keywordToRemove == "hours" || keywordToRemove == "hour" || keywordToRemove == "hrs" || keywordToRemove == "hr"
                        || keywordToRemove == "days" || keywordToRemove == "day" || keywordToRemove == "month" || keywordToRemove == "months"
                        || keywordToRemove == "mnth" || keywordToRemove == "mths")
                            throw new InvalidDeleteFlexiException("This is a default keyword and can't be removed");
                        settingInfo.userTimeRangeType.Remove(keywordToRemove);

                        EventHandlers.UpdateSettings(settingInfo);
                        break;
                    }
            }
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <param name="flexiCommandType">Specify the type of Command you wish to see User Commands of</param>
        /// <returns>Returns a list of added commands</returns>
        public List<string> GetFlexiKeywordList(Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType commandType = (CommandType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userCommandKeywords)
                        {
                            if (pair.Value == commandType)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType contextType = (ContextType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userContextKeywords)
                        {
                            if (pair.Value == contextType)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType timeRangeKeyword = (TimeRangeKeywordsType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userTimeRangeKeywordsType)
                        {
                            if (pair.Value == timeRangeKeyword)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType timeRange = (TimeRangeType)flexiCommandType;
                        List<string> getCommands = new List<string>();
                        foreach (var pair in settingInfo.userTimeRangeType)
                        {
                            if (pair.Value == timeRange)
                                getCommands.Add(pair.Key);
                        }

                        return getCommands;
                    }
            }

            return null;
        }

        #endregion

        #region TimeDictionary

        /// <summary>
        /// Set the Default Time Range of Morning,Afternoon,Evening and Night
        /// </summary>
        /// <param name="timeRange">Set type of TimeRange</param>
        /// <param name="startTime">Set Starting Time</param>
        /// <param name="endTime">Set Ending Time</param>
        public void SetTimeRange(TimeRangeKeywordsType timeRange, int startTime, int endTime)
        {
            settingInfo.userTimeRangeKeywordsStartTime[timeRange] = startTime;
            settingInfo.userTimeRangeKeywordsEndTime[timeRange] = endTime;
            EventHandlers.UpdateSettings(settingInfo);
        }

        public int GetStartTime(TimeRangeKeywordsType timeRange) { return settingInfo.userTimeRangeKeywordsStartTime[timeRange]; }
        public int GetEndTime(TimeRangeKeywordsType timeRange) { return settingInfo.userTimeRangeKeywordsEndTime[timeRange]; }

        #endregion


        #endregion

    }
}
