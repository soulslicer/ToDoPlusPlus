//@qianpan A0103985Y

namespace ToDo
{
    public class TokenLiteral : Token
    {
        string literal;

        internal TokenLiteral(int position, string val)
            : base(position)
        {
            literal = val;
            Logger.Info("Created a literal token", "TokenLiteral::TokenLiteral");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            if (attrb.TaskName == null)
            {
                attrb.TaskName = literal;
                Logger.Info("Found task name", "ConfigureGenerator::TokenLiteral");
            }
            else
            {
                attrb.TaskName += " " + literal;
                Logger.Warning("Task name already defined but more literals present. Appending to task name.", "ConfigureGenerator::TokenLiteral");
            }
        }
    }
}
