//@qianpan A0103985Y

namespace ToDo
{
    public class TokenSortType : Token
    {
        SortType sortType;
        
        internal TokenSortType(int position, SortType val)
            : base(position)
        {
            sortType = val;
            Logger.Info("Created a sort type token", "TokenSortType::TokenSortType");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            attrb.SortType = sortType;
        }

    }
}
