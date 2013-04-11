//@qianpan A0103985Y
using System;
using System.Diagnostics;

namespace ToDo
{
    public class TokenTime : Token
    {
        TimeSpan time;
        Boolean specific;

        internal TokenTime(int position, TimeSpan val, Boolean specific_flag)
            : base(position)
        {
            time = val;
            specific = specific_flag;
            Logger.Info("Created an time token object", "TokenTime::TokenTime");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            switch (attrb.CurrentMode)
            {
                case ContextType.STARTTIME:
                    attrb.StartTimeOnly = time;
                    attrb.IsSpecific.StartTime = specific;
                    break;
                case ContextType.ENDTIME:
                    attrb.SetConditionalEndTime(time, specific);
                    Logger.Info("Successfully set conditional end time.", "ConfigureGenerator::TokenTime");
                    break;
                case ContextType.DEADLINE:
                    attrb.EndTimeOnly = time;
                    attrb.IsSpecific.EndTime = specific;
                    break;
                default:
                    Logger.Error("Fell through switch statement.", "ConfigureGenerator::TokenTime");
                    Debug.Assert(false, "Fell through switch statement in ConfigureGenerator, TokenTime!");
                    break;
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
