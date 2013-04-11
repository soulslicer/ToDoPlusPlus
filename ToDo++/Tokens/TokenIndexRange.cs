//@qianpan A0103985Y

namespace ToDo
{
    public class TokenIndexRange : Token
    {
        public const int START_INDEX = 0;
        public const int END_INDEX = 1;
        public const int RANGE = 2;
        int[] indexes;
        bool isAll;

        internal bool IsAll
        {
            get { return isAll; }
        }
        internal int[] Value
        {
            get { return indexes; }
        }
        internal TokenIndexRange(int position, int[] val, bool isAll)
            : base(position)
        {
            indexes = val;
            this.isAll = isAll;
            Logger.Info("Created an index range token object", "TokenIndexRange::TokenIndexRange");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            if (indexes != null)
            {
                attrb.TaskRangeIndex = indexes;
            }
            attrb.RangeIsAll = isAll;
        }
    }
}
