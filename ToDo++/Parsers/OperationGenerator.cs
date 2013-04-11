//@ivan A0086401M
using System;

namespace ToDo
{
    // ****************************************************************************
    // Factory class for creating Operations.
    // This class is first configured by Tokens, and once finalized,
    // can generate the appropriate operations using the GenerateOperation method.
    // ****************************************************************************
    class OperationGenerator
    {
        // ******************************************************************
        // Operation Properties
        // ******************************************************************
        
        #region Operation Properties       
 
        // These properties control the type of operation to be generated.
        private CommandType commandType;
        public CommandType CommandType
        {
            get { return commandType; }
            set { commandType = value; }
        }        

        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        private int[] taskRangeIndex;
        public int[] TaskRangeIndex
        {
            get { return taskRangeIndex; }
            set { taskRangeIndex = value; }
        }

        private DateTimeSpecificity isSpecific;
        public DateTimeSpecificity IsSpecific
        {
            get { return isSpecific; }
            set { isSpecific = value; }
        }

        private TimeRangeType timeRangeType;
        public TimeRangeType TimeRangeType
        {
            get { return timeRangeType; }
            set { timeRangeType = value; }
        }

        private TimeRangeKeywordsType timeRangeOne;
        public TimeRangeKeywordsType TimeRangeFirst
        {
            get { return timeRangeOne; }
            set { timeRangeOne = value; }
        }

        private TimeRangeKeywordsType timeRangeTwo;
        public TimeRangeKeywordsType TimeRangeSecond
        {
            get { return timeRangeTwo; }
            set { timeRangeTwo = value; }
        }

        private int timeRangeIndex;
        public int TimeRangeIndex
        {
            get { return timeRangeIndex; }
            set { timeRangeIndex = value; }
        }

        private SortType sortType;
        public SortType SortType
        {
            set { sortType = value; }
        }

        private SearchType searchType;
        public SearchType SearchType
        {
            set { searchType = value; }
        }

        private bool rangeIsAll;
        public bool RangeIsAll
        {
            set { rangeIsAll = value; }
        }
        #endregion

        // ******************************************************************
        // Configuration Attributes For Operation Generation
        // ******************************************************************

        #region Configuration Attributes For Operation Generation
        // The following properties are only used internally once set and hence cannot be "get".
        // Set as private to prevent confusion.
        private TimeSpan? startTimeOnly, endTimeOnly;
        private DateTime? startDateOnly, endDateOnly;
        private bool startDayOfWeekSet, endDayOfWeekSet;

        // Setter methods
        public TimeSpan? EndTimeOnly { set { endTimeOnly = value; } }
        public TimeSpan? StartTimeOnly { set { startTimeOnly = value; } }
        public DateTime? EndDateOnly { set { endDateOnly = value; } }
        public DateTime? StartDateOnly { set { startDateOnly = value; } }
        public bool EndDayOfWeekSet { set { endDayOfWeekSet = value; } }
        public bool StartDayOfWeekSet { set { startDayOfWeekSet = value; } }

        // The following attributes are used during derivation of Operation type and should not be otherwised used.
        private ContextType currentSpecifier;
        public ContextType CurrentSpecifier
        {
            get { return currentSpecifier; }
            set { currentSpecifier = value; }
        }
        private ContextType currentMode;
        public ContextType CurrentMode
        {
            get { return currentMode; }
            set { currentMode = value; }
        }
        private DateTime? startDateTime, endDateTime;
        private bool crossDayBoundary;
        #endregion

        // ******************************************************************
        // Constructors and initializers
        // ******************************************************************

        #region Constructors and initializers
        /// <summary>
        /// Constructor for the generator which initializes it's 
        /// settings to the default values.
        /// </summary>
        /// <returns>Nothing.</returns>
        public OperationGenerator()
        {
            InitializeNewConfiguration();
        }

        /// <summary>
        /// Initializes the generator's configuration to it's default values.
        /// </summary>
        /// <returns></returns>
        public void InitializeNewConfiguration()
        {
            commandType = new CommandType();
            isSpecific = new DateTimeSpecificity();
            timeRangeType = new TimeRangeType();
            timeRangeOne = new TimeRangeKeywordsType();
            timeRangeTwo = new TimeRangeKeywordsType();
            sortType = new SortType();
            searchType = new SearchType();
            taskName = null;
            taskRangeIndex = null;
            timeRangeIndex = 0;
            rangeIsAll = false;
            startDateTime = null; endDateTime = null;            
            startTimeOnly = null; endTimeOnly = null;
            startDateOnly = null; endDateOnly = null;
            startDayOfWeekSet = false; endDayOfWeekSet = false;
            currentSpecifier = new ContextType();
            currentMode = new ContextType();
            crossDayBoundary = false;

            ResetEnumerations(); 
        }

        /// <summary>
        /// Resets enums to their default values.
        /// </summary>
        /// <returns>Nothing.</returns>
        private void ResetEnumerations()
        {
            commandType = CommandType.INVALID;
            currentMode = ContextType.STARTTIME;
            currentSpecifier = ContextType.CURRENT;
            sortType = SortType.DEFAULT;
            searchType = SearchType.NONE;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRangeOne = TimeRangeKeywordsType.NONE;
            timeRangeTwo = TimeRangeKeywordsType.NONE;
        }
        #endregion

        // ******************************************************************
        // Finalize generator for operation creation
        // ******************************************************************

        #region Finalize Generator
        /// <summary>
        /// Finalizes the generator so that it can begin generating operations
        /// with the correct time ranges.
        /// </summary>
        public void FinalizeGenerator()
        {
            GetTimeRangeValues();
            if (commandType == CommandType.SCHEDULE)
            {
                FinalizeSchedulingTime();
            }
            else if (CommandIsSearchableType())
            {
                FinalizeSearchTime();
            }
            CombineDateTimes();
        }

        /// <summary>
        /// Returns true if the operation to be generated can carry out a search
        /// on the task list, and false if not.
        /// </summary>
        /// <returns>True if the command is of a searchable type; false if otherwise</returns>
        private bool CommandIsSearchableType()
        {
            return !((commandType == CommandType.ADD) ||
                     (commandType == CommandType.SCHEDULE) ||
                     (commandType == CommandType.MODIFY && taskRangeIndex != null));
        }

        // ******************************************************************
        // Finalize Search Times
        // ******************************************************************

        #region Finalize Search Times
        /// <summary>
        /// Finalizes the date/times of the operation to be generated
        /// by setting it as an appropriate search range.
        /// </summary>
        private void FinalizeSearchTime()
        {
            // If searching only for a single time, assume it's the end time.
            if (IsOnlyStartTimeSet())
            {
                endTimeOnly = startTimeOnly;
                isSpecific.EndTime = isSpecific.StartTime;
                startTimeOnly = null;
            }

            // If searching for a single date, assume the whole range is that date.
            if (IsOnlyStartDateSet())
            {
                endDateOnly = startDateOnly;
                isSpecific.EndDate = isSpecific.StartDate;
            }

            // If end time is not specific, extend search range to cover appropriate period.
            if (endDateOnly != null && endTimeOnly == null)
            {
                ExtendEndSearchDate();
            }
        }

        /// <summary>
        /// Returns a boolean indicating if only the start time for the generated
        /// Operation is set and the end date/times are not.
        /// </summary>
        /// <returns>True if the start time is set, false if not.</returns>
        private bool IsOnlyStartTimeSet()
        {
            return startTimeOnly != null && endTimeOnly == null && endDateOnly == null;
        }

        /// <summary>
        /// Returns a boolean indicating if only the start date for the generated
        /// Operation is set, and the start time, end date/times are not.
        /// </summary>
        /// <returns>True if the only the start date is set, false if not.</returns>
        private bool IsOnlyStartDateSet()
        {
            return startDateOnly != null && endDateOnly == null && startTimeOnly == null && endTimeOnly == null;
        }

        /// <summary>
        /// Extends the end date to the end of the day, month or year,
        /// depending on the already set Specificity of the generator.
        /// </summary>
        /// <returns>Nothing.</returns>
        private void ExtendEndSearchDate()
        {
            if (!isSpecific.EndDate.Day)
            {
                ExtendEndMonthOrYear();
                endDateOnly = endDateOnly.Value.AddMinutes(-1);
            }
            else if (isSpecific.StartDate.Day == true)
            {
                ExtendEndDay();
            }
        }

        /// <summary>
        /// Extends the end date to the end of the month or year,
        /// depending on the already set Specificity of the generator.
        /// </summary>
        /// <returns>Nothing.</returns>
        private void ExtendEndMonthOrYear()
        {
            if (!isSpecific.EndDate.Month)
            {
                endDateOnly = new DateTime(endDateOnly.Value.Year + 1, 1, 1);
            }
            else
            {
                endDateOnly = endDateOnly.Value.AddMonths(1);
                endDateOnly = new DateTime(endDateOnly.Value.Year, endDateOnly.Value.Month, 1);
            }
        }

        /// <summary>
        /// Extends the end date to the end of the day.
        /// </summary>
        /// <returns>Nothing.</returns>
        private void ExtendEndDay()
        {
            endDateOnly = new DateTime(endDateOnly.Value.Year, endDateOnly.Value.Month, endDateOnly.Value.Day, 23, 59, 0);
        }
        #endregion

        // ******************************************************************
        // Finalize Scheduling Time
        // ******************************************************************

        #region Finalize Scheduling Time
        /// <summary>
        /// Finalizes the scheduling time range.
        /// </summary>
        private void FinalizeSchedulingTime()
        {
            FinalizeScheduleStartDate();
        }

        /// <summary>
        /// Sets the start date to today if no starting date was given.
        /// </summary>
        private void FinalizeScheduleStartDate()
        {
            if (startDateOnly == null)
            {
                startDateOnly = DateTime.Today;
                isSpecific.StartDate.Day = false;
                isSpecific.StartDate.Month = false;
                isSpecific.StartDate.Year = false;
            }
        }
        #endregion

        //@jenna A0083536B
        // ******************************************************************
        // Set Time Ranges
        // ******************************************************************

        #region Set Time Ranges
        /// <summary>
        /// This method sets the final startTimeOnly and endTimeOnly values based on the input
        /// time (3am, 4pm...) and time range keyword(s) (morning, afternoon...).
        /// </summary>
        private void GetTimeRangeValues()
        {
            int startTimeHour = 0, endTimeHour = 0;
            if (TryGetTimeRangeValues(ref startTimeHour, ref endTimeHour))
            {
                // pick the correct start time and end time if other times were
                // specified beyond the time range keywords i.e. by time tokens
                if (IsSpecificTimeSupplied())
                {
                    RetrieveFinalStartAndEndTimes(startTimeHour, endTimeHour);
                }
                else
                {
                    if (currentMode != ContextType.DEADLINE)
                    {
                        startTimeOnly = new TimeSpan(startTimeHour, 0, 0);
                    }
                    else
                    {
                        startTimeOnly = null;
                    }
                    endTimeOnly = new TimeSpan(endTimeHour, 0, 0);
                }
            }
        }

        /// <summary>
        /// This method retrieves the start and end hours of the input time range keyword(s) (morning, afternoon...).
        /// It also updates the crossDayBoundary boolean to true if the time ranges crosses the day boundary
        /// i.e. 11pm to 1am.
        /// </summary>
        /// <param name="startTimeHour">The time range's start hour</param>
        /// <param name="endTimeHour">The time range's end hour</param>
        /// <returns>Returns true if there were input time range keyword(s); false if otherwise</returns>
        private bool TryGetTimeRangeValues(ref int startTimeHour, ref int endTimeHour)
        {
            if (timeRangeOne != TimeRangeKeywordsType.NONE)
            {
                // getting values from specified time range keywords
                CustomDictionary.timeRangeKeywordsStartTime.TryGetValue(timeRangeOne, out startTimeHour);
                if (timeRangeTwo == TimeRangeKeywordsType.NONE)
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeOne, out endTimeHour);
                    if (CustomDictionary.IsTimeRangeOverDayBoundary(timeRangeOne))
                    {
                        crossDayBoundary = true;
                    }
                }
                else
                {
                    CustomDictionary.timeRangeKeywordsEndTime.TryGetValue(timeRangeTwo, out endTimeHour);
                    if (CustomDictionary.IsTimeRangeOverDayBoundary(timeRangeTwo))
                    {
                        crossDayBoundary = true;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method checks if a specific time was supplied.
        /// </summary>
        /// <returns>True if both the start time and end time were not specified; false if otherwise</returns>
        private bool IsSpecificTimeSupplied()
        {
            if (startTimeOnly == null && endTimeOnly == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method picks the final hours values to be used for the time range start and end hours values
        /// from the input time (3am, 4pm...) and time range keyword(s) (morning, afternoon...).
        /// </summary>
        /// <param name="startTimeHour">The time range's start hour</param>
        /// <param name="endTimeHour">The time range's end hour</param>
        private void RetrieveFinalStartAndEndTimes(int startTimeHour, int endTimeHour)
        {
            TimeSpan startTimeRange = new TimeSpan(startTimeHour, 0, 0);
            TimeSpan endTimeRange = new TimeSpan(endTimeHour, 0, 0);
            if (startTimeOnly != null && endTimeOnly == null)
            {
                if (!IsStartTimeWithinTimeRange(startTimeRange, endTimeRange))
                {
                    AlertBox.Show("Specified end time not within specified time range.");
                }
                endTimeOnly = startTimeOnly;
                startTimeOnly = startTimeRange;
            }
            else if (startTimeOnly != null && endTimeOnly != null)
            {
                if (!IsStartAndEndTimeWithinTimeRange(startTimeRange, endTimeRange))
                {
                    AlertBox.Show("Specified start and end times are not within specified time range.");
                }
            }
        }

        /// <summary>
        /// This method checks if the startTimeOnly is within the specified start and end hours
        /// </summary>
        /// <param name="startTimeRange">The specified start hour</param>
        /// <param name="endTimeRange">The specified end hour</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool IsStartTimeWithinTimeRange(TimeSpan startTimeRange, TimeSpan endTimeRange)
        {
            if (!(startTimeRange <= startTimeOnly
                && startTimeOnly <= endTimeRange))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method checks if the startTimeOnly and endTimeOnly are within the specified start and end hours
        /// </summary>
        /// <param name="startTimeRange">The specified start hour</param>
        /// <param name="endTimeRange">The specified end hour</param>
        /// <returns>True if positive; false if otherwise</returns>
        private bool IsStartAndEndTimeWithinTimeRange(TimeSpan startTimeRange, TimeSpan endTimeRange)
        {
            if (!(startTimeRange <= startTimeOnly
                && startTimeOnly <= endTimeRange
                && startTimeRange <= endTimeOnly
                && endTimeOnly <= endTimeRange))
            {
                return false;
            }
            return true;
        }
        #endregion

        //@ivan A0086401M
        // ******************************************************************
        // Combine Date And Times As Single Date Time
        // ******************************************************************

        #region CombineDateTimes
        /// <summary>
        /// Combines the start and end DateOnly and TimeOnly fields of the configuration properties
        /// into a two properly formatted start and end DateTimes for operation generation.
        /// </summary>
        private void CombineDateTimes()
        {
            NormalizeDeadlines();

            SetSpecificities();

            NormalizeDates();

            startDateTime = CombineDateAndTime(startTimeOnly, startDateOnly, isSpecific.StartDate, DateTime.Now, true);
            if (startDateTime == null)
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, isSpecific.EndDate, DateTime.Now, true);
            else
                endDateTime = CombineDateAndTime(endTimeOnly, endDateOnly, isSpecific.EndDate, startDateTime.Value, false);

            isSpecific.NormalizeSpecificity();

            if (startDateTime > endDateTime)
                AlertBox.Show("Warning: End date is before start date");
        }

        /// <summary>
        /// Sets both dates to the same date if only one date is specified but two times are present.
        /// </summary>
        private void NormalizeDates()
        {
            // If only one date is specified, we assume both dates is that date.
            if (isSpecific.StartTime && isSpecific.EndTime)
            {
                // assign end date to start date
                if (startDateOnly == null && endDateOnly != null)
                {
                    startDateOnly = endDateOnly;
                    //isSpecific.StartDate = isSpecific.EndDate;
                }
                // assign start date to end date
                else if (startDateOnly != null && endDateOnly == null)
                {
                    endDateOnly = startDateOnly;
                    //isSpecific.EndDate = isSpecific.StartDate;
                }
            }
        }

        /// <summary>
        /// Sets start date as end date to normalize deadlines if a task's start date and end time is set and not the rest, or vice versa.
        /// </summary>
        private void NormalizeDeadlines()
        {
            // Start time is set but not start date; End date is set but not end time => Deadline task.
            if (startTimeOnly != null && startDateOnly == null && endTimeOnly == null && endDateOnly != null)                
            {
                endTimeOnly = startTimeOnly;
                startTimeOnly = null;
            }
            // Start date is set but not start time; End time is set but not end date => Deadline task.
            if (startTimeOnly == null && startDateOnly != null && endTimeOnly != null && endDateOnly == null)
            {
                endDateOnly = startDateOnly;
                startDateOnly = null;
            }
        }

        /// <summary>
        /// Sets specificities based on whether start/end date/time has been set.
        /// </summary>
        private void SetSpecificities()
        {
            if (startTimeOnly == null)
            {
                isSpecific.StartTime = false;
            }
            if (endTimeOnly == null)
            {
                isSpecific.EndTime = false;
            }
            if (startDateOnly == null)
            {
                isSpecific.StartDate = new Specificity(false, false, false);
            }
            if (endDateOnly == null)
            {
                isSpecific.EndDate = new Specificity(false, false, false);
            }
        }
        /// <summary>
        /// Combines the time and date fields into a single DateTime that is after the specified limit.
        /// </summary>
        /// <param name="time">The time to merge.</param>
        /// <param name="date">The date to merge.</param>
        /// <param name="dateSpecificity">The specificity of the date.</param>
        /// <param name="limit">The limit to ensure the combined date/time is after.</param>
        /// <param name="allowCurrent">Flag indicating whether to allow ambiguous dates matching today to pass the limit check.</param>
        /// <returns></returns>
        private DateTime? CombineDateAndTime(TimeSpan? time, DateTime? date, Specificity dateSpecificity, DateTime limit, bool allowCurrent)
        {
            DateTime? combinedDT = null;
            // Time defined but not date
            if (date == null && time != null)
            {
                TimeSpan limitTime = limit.TimeOfDay;
                TimeSpan taskTime = time.Value;
                combinedDT = new DateTime(limit.Year, limit.Month, limit.Day, taskTime.Hours, taskTime.Minutes, taskTime.Seconds);                
            }
            // Date and Time both defined
            else if (date != null && time != null)
            {
                DateTime setDate = date.Value;
                TimeSpan setTime = time.Value;                
                combinedDT = new DateTime(setDate.Year, setDate.Month, setDate.Day, setTime.Hours, setTime.Minutes, setTime.Seconds);
            }
            // Date defined but not time
            else if (time == null && date != null)
            {
                combinedDT = date;
            }

            if (limit > combinedDT)
                PushDateToBeyondLimit(ref combinedDT, ref dateSpecificity, limit, allowCurrent && time == null);

            return combinedDT;
        }

        /// <summary>
        /// Ensures a given unspecific date is beyond a specified limit. Moves it to the first appropriate date if not.
        /// </summary>
        /// <param name="dateToCheck">The given date to check.</param>
        /// <param name="dateSpecificity">The given date's specificity</param>
        /// <param name="limit">The limit to check the given date against.</param>
        /// <param name="allowCurrent">Flag to indicate if the date should be unchanged or not if it falls within a range close to the limit
        /// and cannot be determined accurately due to its specificity.</param>
        /// <returns></returns>
        private void PushDateToBeyondLimit(ref DateTime? dateToCheck, ref Specificity dateSpecificity, DateTime limit, bool allowCurrent)
        {
            if (this.commandType == CommandType.ADD)
            {
                if (IsDayOfWeekSet(allowCurrent))
                {
                    while (limit > dateToCheck)
                    {
                        dateToCheck = dateToCheck.Value.AddDays(7);
                    }
                }

                if (DateIsAmbiguous(ref dateToCheck, ref dateSpecificity) &&
                    allowCurrent)
                    return;

                // Move by days until day is beyond limits.
                if (!dateSpecificity.Day && !dateSpecificity.Month)
                {
                    while (limit > dateToCheck)
                        dateToCheck = dateToCheck.Value.AddDays(1);

                    dateSpecificity.Day = true;
                }

                // Move by months until date is beyond limits
                else if (!dateSpecificity.Month)
                {
                    while (limit > dateToCheck)
                        dateToCheck = dateToCheck.Value.AddMonths(1);

                    dateSpecificity.Month = true;
                }
                // Move by years until date is beyond limits
                else if (!dateSpecificity.Year)
                {
                    if (dateToCheck.Value.Date == DateTime.Today.Date && allowCurrent)
                        return;

                    while (limit > dateToCheck)
                        dateToCheck = dateToCheck.Value.AddYears(1);

                    dateSpecificity.Year = true;
                }
            }
        }

        /// <summary>
        /// Returns if a date is ambiguous enough to fit today's date.
        /// </summary>
        /// <param name="dateToCheck">The date to check.</param>
        /// <param name="dateSpecificity">The specificity of the date.</param>
        /// <returns></returns>
        private bool DateIsAmbiguous( ref DateTime? dateToCheck, ref Specificity dateSpecificity )
        {
	        return dateToCheck.Value.Month == DateTime.Today.Month &&
                   dateToCheck.Value.Year == DateTime.Today.Year &&
                   dateSpecificity.Day == false;
        }

        /// <summary>
        /// Returns a flag indicating if the day of week was set for the start day if the input flag is true,
        /// or if the day of week was set for the end day if the input flag is false.
        /// </summary>
        /// <param name="startEndFlag">Input flag.</param>
        /// <returns></returns>
        private bool IsDayOfWeekSet(bool startEndFlag)
        {
            return (startDayOfWeekSet && startEndFlag) ||
                   (endDayOfWeekSet && !startEndFlag);
        }
        #endregion
        #endregion

        // ******************************************************************
        // Operation Generation
        // ******************************************************************

        #region Operation Generation
        /// <summary>
        /// This operation generates an operation based on how this generator has been configured.
        /// </summary>
        /// <returns>The generated operation.</returns>
        public Operation CreateOperation()
        {
            Task task;
            Operation newOperation = null;
            switch (commandType)
            {
                case CommandType.ADD:
                    task = Task.CreateNewTask(taskName, startDateTime, endDateTime, isSpecific);
                    newOperation = new OperationAdd(task, sortType);
                    break;
                case CommandType.DELETE:
                    newOperation = new OperationDelete(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.DISPLAY:
                    newOperation = new OperationDisplayDefault(sortType);
                    break;
                case CommandType.MODIFY:
                    newOperation = new OperationModify(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.SEARCH:
                    newOperation = new OperationSearch(taskName, startDateTime, endDateTime, isSpecific, searchType, sortType);
                    break;
                case CommandType.SORT:
                    newOperation = new OperationSort(sortType);
                    break;
                case CommandType.REDO:
                    newOperation = new OperationRedo(sortType);
                    break;
                case CommandType.UNDO:
                    newOperation = new OperationUndo(sortType);
                    break;
                case CommandType.DONE:
                    newOperation = new OperationMarkAsDone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.UNDONE:
                    newOperation = new OperationMarkAsUndone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, sortType);
                    break;
                case CommandType.POSTPONE:
                    TimeSpan postponeDuration = new TimeSpan();
                    if (timeRangeType == TimeRangeType.DEFAULT)
                    {
                        timeRangeType = CustomDictionary.defaultPostponeDurationType;
                        timeRangeIndex = CustomDictionary.defaultPostponeDurationLength;
                    }
                    switch (timeRangeType)
                    {
                        case TimeRangeType.HOUR:
                            postponeDuration = new TimeSpan(timeRangeIndex, 0, 0);
                            break;
                        case TimeRangeType.DAY:
                            postponeDuration = new TimeSpan(timeRangeIndex, 0, 0, 0);
                            break;
                        case TimeRangeType.WEEK:
                            postponeDuration = new TimeSpan(timeRangeIndex * CustomDictionary.DAYS_IN_WEEK, 0, 0, 0);
                            break;
                        case TimeRangeType.MONTH:
                            postponeDuration = new TimeSpan(timeRangeIndex * CustomDictionary.DAYS_IN_MONTH, 0, 0, 0);
                            break;
                    }
                    newOperation = new OperationPostpone(taskName, taskRangeIndex, startDateTime, endDateTime, isSpecific, rangeIsAll, searchType, postponeDuration, sortType);
                    break;
                case CommandType.SCHEDULE:
                    newOperation = new OperationSchedule(taskName, (DateTime)startDateTime, endDateTime, isSpecific, timeRangeIndex, timeRangeType, sortType);
                    break;
                case CommandType.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            return newOperation;
        }
        #endregion

        // ******************************************************************
        // Generator configuration methods
        // ******************************************************************

        #region Generator configuration methods
        /// <summary>
        /// Sets the configured end time to the specified time and specificity.
        /// Moves the end time to the start time if necessary.
        /// </summary>
        /// <param name="Value">The end time to be set.</param>
        /// <param name="IsSpecific">The specificity of the end time.</param>
        /// <returns></returns>
        internal void SetConditionalEndTime(TimeSpan Value, bool IsSpecific)
        {
            if (startTimeOnly == null && endTimeOnly != null)
            {
                this.startTimeOnly = this.endTimeOnly;
                this.isSpecific.StartTime = this.isSpecific.EndTime;
            }
            this.endTimeOnly = Value;
            this.isSpecific.EndTime = IsSpecific;
        }

        /// <summary>
        /// Sets the configured end date to the specified date and specificity.
        /// Moves the end date to the start date if necessary.
        /// </summary>
        /// <param name="Value">The end daate to be set.</param>
        /// <param name="IsSpecific">The specificity of the end date.</param>
        /// <returns></returns>
        internal void SetConditionalEndDate(DateTime Value, Specificity IsSpecific)
        {
            if (startDateOnly == null && endDateOnly != null)
            {
                this.startDateOnly = this.endDateOnly;
                this.isSpecific.StartDate = this.isSpecific.EndDate;
            }
            this.endDateOnly = Value;
            this.isSpecific.EndDate = IsSpecific;
        }
        #endregion
    }
}