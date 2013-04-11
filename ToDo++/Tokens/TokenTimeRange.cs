//@qianpan A0103985Y

namespace ToDo
{
    public class TokenTimeRange : Token
    {
        TimeRangeKeywordsType timeRange;
        TimeRangeType timeRangeType;
        int index;

        internal int Value
        {
            get { return index; }
        }
        internal TimeRangeType Type
        {
            get { return timeRangeType; }
        }
        internal TimeRangeKeywordsType Range
        {
            get { return timeRange; }
        }
        internal TokenTimeRange(int position, int val, TimeRangeType type)
            : base(position)
        {
            index = val;
            timeRangeType = type;
            timeRange = TimeRangeKeywordsType.NONE;
            Logger.Info("Created an time range token object", "TokenTimeRange::TokenTimeRange");
        }
        internal TokenTimeRange(int position, TimeRangeKeywordsType range)
            : base(position)
        {
            index = 0;
            timeRangeType = TimeRangeType.DEFAULT;
            timeRange = range;
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            bool multipleTaskDurations = false;
            if (index != 0)
            {
                if (attrb.TimeRangeIndex == 0)
                {
                    attrb.TimeRangeIndex = index;
                }
                else
                {
                    multipleTaskDurations = true;
                    Logger.Warning("Attempted to update timeRangeIndex again", "ConfigureGenerator::TokenTimeRange");
                }
            }
            if (timeRangeType != TimeRangeType.DEFAULT)
            {
                if (attrb.TimeRangeType == TimeRangeType.DEFAULT)
                {
                    attrb.TimeRangeType = timeRangeType;
                }
                else
                {
                    multipleTaskDurations = true;
                    Logger.Warning("Attempted to update timeRangeType again", "ConfigureGenerator::TokenTimeRange");
                }
            }
            if (timeRange != TimeRangeKeywordsType.NONE)
            {
                if (attrb.TimeRangeFirst == TimeRangeKeywordsType.NONE)
                {
                    attrb.TimeRangeFirst = timeRange;
                }
                else if (attrb.TimeRangeSecond == TimeRangeKeywordsType.NONE
                    || attrb.TimeRangeSecond <= timeRange)
                {
                    attrb.TimeRangeSecond = timeRange;
                }
            }
            if (multipleTaskDurations)
            {
                Logger.Warning("Multiple task durations were detected.", "ConfigureGenerator::TokenTimeRange");
                AlertBox.Show("Multiple task durations specified. Only the first is accepted.");
            }
        }
    }
}