//@ivan A0086401M
namespace ToDo
{
    /// <summary>
    /// This class contains flags representing whether a date
    /// was fully specified by the user or was left ambiguous.
    /// </summary>
    public class Specificity
    {
        private bool day;
        private bool month;
        private bool year;
        public Specificity()
        {
            day = month = year = true;
        }
        public Specificity(Specificity copy)
        {
            this.day = copy.day;
            this.month = copy.month;
            this.year = copy.year;
        }
        public Specificity(bool d, bool m, bool y)
        {
            this.day = d;
            this.month = m;
            this.year = y;
        }
        public bool Day { get { return day; } set { day = value; } }
        public bool Month { get { return month; } set { month = value; } }
        public bool Year { get { return year; } set { year = value; } }
        /// <summary>
        /// Gets a flag indicating if the entire date was fully specified.
        /// </summary>
        /// <returns>True if the entire date was fully specified.</returns>
        public bool Full() { return (day && month && year); }
    }

    /// <summary>
    /// This class contains flags representing whether a set of two dates and times
    /// were fully specified by the user or were left ambiguous.</summary>
    public class DateTimeSpecificity
    {
        private Specificity startDate;
        private Specificity endDate;
        private bool startTime;
        private bool endTime;
        public DateTimeSpecificity()
        {
            startTime = endTime = true;
            startDate = new Specificity();
            endDate = new Specificity();
        }
        public DateTimeSpecificity(bool startTime, bool endTime, Specificity startDate, Specificity endDate)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public Specificity StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public Specificity EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public bool StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public bool EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        /// <summary>
        /// Gets a flag indicating if all dates and times were fully specified.
        /// </summary>
        /// <returns>True if all dates and times were fully specified.</returns>
        public bool Full()
        {
            return (startTime && endTime && startDate.Full() && endDate.Full());
        }

        /// <summary>
        /// Finalizes this specificity for pushing to the operation. Assigns all less specific
        /// types to take on their parent specificity.
        /// </summary>
        public void NormalizeSpecificity()
        {
            if (startTime) startDate.Day = true;
            if (endTime) endDate.Day = true;
            if (startDate.Day) startDate.Month = true;
            if (endDate.Day) endDate.Month = true;
            if (startDate.Month) startDate.Year = true;
            if (endDate.Month) endDate.Year = true;
        }
    }
}
