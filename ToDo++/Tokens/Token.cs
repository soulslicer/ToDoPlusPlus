//@qianpan A0103985Y

namespace ToDo
{
    public abstract class Token
    {
        // Position of the Token within a string.
        private int position;
        internal Token(int position)
        {
            this.position = position;
        }  
        internal int Position
        {
            get { return position; }           
        }

        /// <summary>
        /// The base method which should be overriden by derived classes.
        /// It allows the token to configure an OperationGenerator to create
        /// an appropriate Operation.
        /// </summary>
        /// <param name="attrb">The OperationGenerator to configure.</param>
        internal abstract void ConfigureGenerator(OperationGenerator attrb);
        
        /// <summary>
        /// Gets a flag indicating if the token accepts a context token at the position
        /// before it.
        /// </summary>
        /// <returns>True if it uses a context token; False if it does not.</returns>
        internal virtual bool AcceptsContext()
        {
            return false;
        }
    }
}