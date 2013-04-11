//@qianpan A0103985Y

namespace ToDo
{
    public class TokenContext : Token
    {
        ContextType contextType;

        internal TokenContext(int position, ContextType val)
            : base(position)
        {
            contextType = val;
            Logger.Info("Created a context token object", "TokenContext::TokenContext");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            if (contextType == ContextType.CURRENT ||
                contextType == ContextType.NEXT ||
                contextType == ContextType.FOLLOWING
                )
            {
                attrb.CurrentSpecifier = contextType;
            }
            else
            {
                attrb.CurrentMode = contextType;
            }
        }

        /// <summary>
        /// Gets a flag indicating if the token accepts a context token at the position
        /// before it.
        /// </summary>
        /// <returns>True if it uses a context token; False if it does not.</returns>
        internal override bool AcceptsContext()
        {
            return true;
        }
    }
}
