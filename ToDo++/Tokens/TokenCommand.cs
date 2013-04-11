//@qianpan A0103985Y
using System.Collections.Generic;

namespace ToDo
{
    public class TokenCommand : Token
    {
        static private List<CommandType> indexRangeableCommandTypes
            = new List<CommandType> { CommandType.DELETE, CommandType.DONE, CommandType.UNDONE, CommandType.MODIFY, CommandType.POSTPONE, CommandType.SEARCH };

        static private List<CommandType> timeRangeableCommandTypes
            = new List<CommandType> { CommandType.SCHEDULE, CommandType.POSTPONE, CommandType.ADD };

        CommandType commandType;

        internal CommandType Value
        {
            get { return commandType; }
        }

        internal TokenCommand(int position, CommandType val)
            : base(position)
        {
            commandType = val;
            Logger.Info("Created a command token object", "TokenCommand::TokenCommand");
        }

        internal override void ConfigureGenerator(OperationGenerator attrb)
        {
            if (attrb.CommandType != CommandType.INVALID)
            {
                if (Value == CommandType.DONE)
                {
                    attrb.SearchType = SearchType.DONE;
                }
                else if (Value == CommandType.UNDONE)
                {
                    attrb.SearchType = SearchType.UNDONE;
                }
                else if (attrb.CommandType == CommandType.SORT)
                {
                    attrb.CommandType = Value;
                    Logger.Info("Resolved multiple commands to not use Sort as command (lower priority)", "ConfigureGenerator::TokenCommand");
                }
                else if (Value == CommandType.SORT)
                {
                    Logger.Info("Resolved multiple commands to not use Sort as command (lower priority)", "ConfigureGenerator::TokenCommand");
                }
                else
                {
                    Logger.Warning("Multiple commands detected", "ConfigureGenerator::TokenCommand");
                    throw new MultipleCommandsException();
                }
            }
            else
            {
                attrb.CommandType = Value;
                Logger.Warning("commandType is INVALID", "ConfigureGenerator::TokenCommand");
            }
        }

        /// <summary>
        /// This method checks if the command is of a type that accepts index ranges i.e. delete
        /// </summary>
        /// <returns>True if positive; false if otherwise</returns>
        internal bool RequiresIndexRange()
        {
            if (indexRangeableCommandTypes.Contains(Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method checks if the command is of a type that accepts time ranges i.e. schedule
        /// </summary>
        /// <returns>True if positive; false if otherwise</returns>
        internal bool RequiresTimeRange()
        {
            if (timeRangeableCommandTypes.Contains(Value))
                return true;
            else
                return false;
        }

    }
}
