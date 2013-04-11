//@qianpan A0103985Y
using System;
using System.Diagnostics;

namespace ToDo
{
    public class TokenDay : Token
    {
        DayOfWeek dayOfWeek;

        internal TokenDay(int position, DayOfWeek val)
            : base(position)
        {
            dayOfWeek = val;
            Logger.Info("Created a day token object", "TokenDay::TokenDay");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            switch (attrb.CurrentMode)
            {
                case ContextType.STARTTIME:
                    attrb.StartDayOfWeekSet = true;
                    attrb.StartDateOnly = GetDateFromDay(attrb.CurrentSpecifier, dayOfWeek);
                    break;
                case ContextType.ENDTIME:
                    attrb.EndDayOfWeekSet = true;
                    attrb.SetConditionalEndDate(GetDateFromDay(attrb.CurrentSpecifier, dayOfWeek), new Specificity());
                    break;
                case ContextType.DEADLINE:
                    attrb.EndDayOfWeekSet = true;
                    attrb.EndDateOnly = GetDateFromDay(attrb.CurrentSpecifier, dayOfWeek);
                    break;
                default:
                    Logger.Error("Fell through currentMode switch statement.", "ConfigureGenerator::TokenDay");
                    Debug.Assert(false, "Fell through switch statement in GenerateOperation, TokenDay case!");
                    break;
            }                    
        }

        /// <summary>
        /// This method accepts a day of the week and returns the corresponding date depending on what the preposition is.
        /// </summary>
        /// <param name="preposition">The prefix specifying how many weeks to add to the next found day.</param>
        /// <param name="desiredDay">The required day.</param>
        /// <returns>Date on which the <var>preposition</var> <var>desiredDay</var> is found.</returns>
        private static DateTime GetDateFromDay(ContextType preposition, DayOfWeek desiredDay)
        {
            DateTime startDate;
            DateTime todayDate = DateTime.Today;
            int daysToAdd = GetDaysToAdd(todayDate.DayOfWeek, desiredDay);
            switch (preposition)
            {
                case ContextType.CURRENT:
                    break;
                case ContextType.NEXT:
                    Logger.Info("preposition is NEXT", "GetDateFromDay::TokenDay");
                    daysToAdd += 7;
                    break;
                case ContextType.FOLLOWING:
                    Logger.Info("preposition is FOLLOWING", "GetDateFromDay::TokenDay");
                    daysToAdd += 14;
                    break;
                default:
                    Logger.Error("Fell through preposition switch statement.", "GetDateFromDay::TokenDay");
                    Debug.Assert(false, "Fell through switch statement in GetDateFromDay!");
                    break;
            }
            startDate = todayDate.AddDays(daysToAdd);
            Logger.Info("Updated startDate", "GetDateFromDay::TokenDay");
            return startDate;
        }

        /// <summary>
        /// Calculates the number of days to add to the given day of
        /// the week in order to return the next occurrence of the
        /// desired day of the week.
        /// </summary>
        /// <param name="current">The starting day of the week.</param>
        /// <param name="desired">The desired day of the week.</param>
        /// <returns>
        ///		The number of days to add to <var>current</var> day of week
        ///		in order to achieve the next <var>desired</var> day of week.
        /// </returns>
        private static int GetDaysToAdd(DayOfWeek current, DayOfWeek desired)
        {
            // f( c, d ) = [7 - (c - d)] mod 7
            //   where 0 <= c < 7 and 0 <= d < 7
            int c = (int)current;
            int d = (int)desired;
            return (7 - c + d) % 7;
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
