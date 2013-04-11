//@raaj A0081202Y
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace CommandParserTest
{
    /// <summary>
    /// This test is commandparser unit test,
    /// using "testCmdParser.ParseOperation" to check the process of
    /// user input being translated into operation.
    /// contains 4 test cases.
    /// </summary>

    [TestClass]
    public class CommandParserTest
    {
        CommandParser testCmdParser;
       
        [TestMethod]
        public void OperationSearchDeadlineParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("search by 2013 oct 30th 5:49 pm");
            Assert.AreEqual("ToDo.OperationSearch", op1.GetType().ToString());
            return;
        }

        [TestMethod]
        public void OperationTimedParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task do stuff add sunday morning to wed 13:20 ");
            Assert.AreEqual("ToDo.OperationAdd", op1.GetType().ToString());
            
            return;
        }

        [TestMethod]
        public void OperationInvalidParseTest()
        {
            testCmdParser = new CommandParser();
            bool flag = false;
            try
            {
                testCmdParser.ParseOperation("add delete modify");
            }
            catch (MultipleCommandsException)
            {
                flag = true;
            }
            Assert.IsTrue(flag);
            return;
        }

        [TestMethod]
        public void OperationScheduleParseTest()
        {
            testCmdParser = new CommandParser();
            Operation op1 = testCmdParser.ParseOperation("task  jan 15th midnight - jan 30th morning schedule 3000 hours");
            Assert.AreEqual("ToDo.OperationSchedule", op1.GetType().ToString());
            return;
        }

    }
}
