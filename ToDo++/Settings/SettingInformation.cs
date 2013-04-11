//@raaj A0081202y
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ToDo
{
    public class SettingInformation
    {
        //DEFAULT SETTING VALUES TO BE LOADED
        private const bool defaultLoadOnStartup = false;
        private const bool defaultStartMinimized = false;
        private const bool defaultStayOnTop = false;
        private const int defaultTextSize = 9;
        private const string defaultFont = "Century Gothic";

        private static readonly Color defaultTaskDoneColor = Color.Green;
        private static readonly Color defaultTaskMissedDeadlineColor = Color.Red;
        private static readonly Color defaultTaskNearingDeadlineColor = Color.OrangeRed;
        private static readonly Color defaultTaskOverColor = Color.MediumVioletRed;

        private const int defDefaultScheduleTimeLength = 1;
        private const int defDefaultPostponeDurationLength = 1;
        private const TimeRangeType defDefaultScheduleTimeLengthType = TimeRangeType.HOUR;
        private const TimeRangeType defDefaultPostponeDurationType = TimeRangeType.DAY;

        public struct MiscSettings
        {
            private bool _firstLoad;

            private bool _loadOnStartup;
            private bool _startMinimized;
            private bool _stayOnTop;
            private int _textSize;
            private string _fontSelection;

            private Color _taskDoneColor;
            private Color _taskMissedDeadlineColor;
            private Color _taskNearingDeadlineColor;
            private Color _taskOverColor;

            private int _defaultScheduleTimeLength;
            private TimeRangeType _defaultScheduleTimeLengthType;
            private int _defaultPostponeDurationLength;
            private TimeRangeType _defaultPostponeDurationType;

            public bool FirstLoad { get { return _firstLoad; } set { _firstLoad = value; } }

            public bool LoadOnStartup { get { return _loadOnStartup; } set { _loadOnStartup = value; } }
            public bool StartMinimized { get { return _startMinimized; } set { _startMinimized = value; } }
            public bool StayOnTop { get { return _stayOnTop; } set { _stayOnTop = value; } }
            public int TextSize { get { return _textSize; } set { _textSize = value; } }
            public string FontSelection { get { return _fontSelection; } set { _fontSelection = value; } }

            public Color TaskDoneColor { get { return _taskDoneColor; } set { _taskDoneColor = value; } }
            public Color TaskMissedDeadlineColor { get { return _taskMissedDeadlineColor; } set { _taskMissedDeadlineColor = value; } }
            public Color TaskNearingDeadlineColor { get { return _taskNearingDeadlineColor; } set { _taskNearingDeadlineColor = value; } }
            public Color TaskOverColor { get { return _taskOverColor; } set { _taskOverColor = value; } }

            public int DefaultScheduleTimeLength { get { return _defaultScheduleTimeLength; } set { _defaultScheduleTimeLength = value; } }
            public int DefaultPostponeDurationLength { get { return _defaultPostponeDurationLength; } set { _defaultPostponeDurationLength = value; } }
            public TimeRangeType DefaultScheduleTimeLengthType { get { return _defaultScheduleTimeLengthType; } set { _defaultScheduleTimeLengthType = value; } }
            public TimeRangeType DefaultPostponeDurationType { get { return _defaultPostponeDurationType; } set { _defaultPostponeDurationType = value; } }

            public MiscSettings(bool firstLoad,bool loadOnStartup, bool startMinimized, bool stayOnTop, int textSize, string fontSelection,
                                Color taskDoneColor, Color taskMissedDeadlineColor, Color taskNearingDeadlineColor, Color taskOverColor,
                                int defaultScheduleTimeLength, TimeRangeType defaultScheduleTimeLengthType,int defaultPostponeDurationLength,TimeRangeType defaultPostponeDurationType)
            {
                _firstLoad = firstLoad;
                _loadOnStartup = loadOnStartup;
                _startMinimized = startMinimized;
                _stayOnTop = stayOnTop;
                _textSize = textSize;
                _fontSelection = fontSelection;
                _taskDoneColor = taskDoneColor;
                _taskMissedDeadlineColor = taskMissedDeadlineColor;
                _taskNearingDeadlineColor = taskNearingDeadlineColor;
                _taskOverColor = taskOverColor;
                _defaultScheduleTimeLength = defaultScheduleTimeLength;
                _defaultScheduleTimeLengthType = defaultScheduleTimeLengthType;
                _defaultPostponeDurationLength = defaultPostponeDurationLength;
                _defaultPostponeDurationType = defaultPostponeDurationType;
            }
        }

        public MiscSettings misc;
        public Dictionary<string, CommandType> userCommandKeywords;
        public Dictionary<string, ContextType> userContextKeywords;
        public Dictionary<string, TimeRangeKeywordsType> userTimeRangeKeywordsType;
        public Dictionary<string, TimeRangeType> userTimeRangeType;
        public Dictionary<TimeRangeKeywordsType, int> userTimeRangeKeywordsStartTime;
        public Dictionary<TimeRangeKeywordsType, int> userTimeRangeKeywordsEndTime;

        public SettingInformation()
        {
            misc = new MiscSettings(true,defaultLoadOnStartup, defaultStartMinimized, defaultStayOnTop, defaultTextSize, defaultFont,
                                    defaultTaskDoneColor,defaultTaskMissedDeadlineColor,defaultTaskNearingDeadlineColor,defaultTaskOverColor,
                                    defDefaultScheduleTimeLength,defDefaultScheduleTimeLengthType,defDefaultPostponeDurationLength,defDefaultPostponeDurationType);

            userCommandKeywords = CustomDictionary.GetCommandKeywords();
            userContextKeywords = CustomDictionary.GetContextKeywords();
            userTimeRangeKeywordsType = CustomDictionary.GetTimeRangeKeywordKeywords();
            userTimeRangeType = CustomDictionary.GetTimeRangeKeywords();
            userTimeRangeKeywordsStartTime = CustomDictionary.GetTimeRangeStartTime();
            userTimeRangeKeywordsEndTime = CustomDictionary.GetTimeRangeEndTime();
        }

        public bool ContainsFlexiCommandKeyword(string userKeyword, Enum flexiCommandType)
        {
            string flexiType = flexiCommandType.GetType().ToString();
            switch (flexiType)
            {
                case "ToDo.CommandType":
                    {
                        CommandType passed;
                        if (userCommandKeywords.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (CommandType)flexiCommandType)
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.ContextType":
                    {
                        ContextType passed;
                        if (userContextKeywords.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (ContextType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.TimeRangeKeywordsType":
                    {
                        TimeRangeKeywordsType passed;
                        if (userTimeRangeKeywordsType.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (TimeRangeKeywordsType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }

                case "ToDo.TimeRangeType":
                    {
                        TimeRangeType passed;
                        if (userTimeRangeType.TryGetValue(userKeyword, out passed))
                        {
                            if (passed == (TimeRangeType)flexiCommandType)
                                return true;
                            else return false;
                        }
                        else
                            return false;
                    }
            }


            return false;
        }

        public string ToXML()
        {
            string serializedXML = this.Serialize();
            return serializedXML;
        }

        static SettingInformation GenerateSettingInfoFromXML(string xml)
        {
            return xml.Deserialize<SettingInformation>();
        }
    }
}
