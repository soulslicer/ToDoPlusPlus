//@ivan A0086401M
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace StringParserTest
{
    /// <summary>
    /// This test is unit test for stringparser,
    /// which parses string into separate string and return list of string.
    /// contains 9 test cases.
    /// </summary>

    [TestClass]
    public class StringParserTest
    {
        StringParser testStrParser = new StringParser();
        string input;
        List<string> output;

        [TestMethod]
        public void AddSimpleTimedTaskTest()
        {
            input = "add morning 8 AM TASK";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("add");
            outputCheck.Add("morning");
            outputCheck.Add("8 AM");
            outputCheck.Add("TASK");
            Assert.IsTrue(output.Count()==outputCheck.Count);
            for (int i = 0; i < output.Count(); i++ )
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void ParseSortCommandTest()
        {
            input = "sort    name";
            output = testStrParser.ParseStringIntoWords(input); 
            List<string> outputCheck = new List<string>();
            outputCheck.Add("sort");
            outputCheck.Add("name");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void AddDatedTaskTest()
        {
            input = "buy milk tmr 3 am to 12/12/12 5 pm add";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("buy");
            outputCheck.Add("milk");
            outputCheck.Add("tmr");
            outputCheck.Add("3 am");
            outputCheck.Add("to");
            outputCheck.Add("12/12/12");
            outputCheck.Add("5 pm");
            outputCheck.Add("add");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void AddComplicatedDatedTaskTest()
        {
            input = "add  milk jan 23rd 2016 to feb 29th 2016";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("add");
            outputCheck.Add("milk");
            outputCheck.Add("jan 23rd 2016");
            outputCheck.Add("to");
            outputCheck.Add("feb 29th 2016");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void DateTimeParseTest1()
        {
            input = "delete 3.5.2013 morning";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("delete");
            outputCheck.Add("3.5.2013");
            outputCheck.Add("morning");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void DateTimeParseTest2()
        {
            input = "postpone tmr   to wed";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("postpone");
            outputCheck.Add("tmr");
            outputCheck.Add("to");
            outputCheck.Add("wed");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void CombineTest()
        {
            input = "ADD aaa feb 13th to jun 22nd";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("ADD");
            outputCheck.Add("aaa");
            outputCheck.Add("feb 13th");
            outputCheck.Add("to");
            outputCheck.Add("jun 22nd"); 
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void TimeRangeTest()
        {
            input = "add aaa aa tmr 13:00 - 19:00";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("add");
            outputCheck.Add("aaa");
            outputCheck.Add("aa");
            outputCheck.Add("tmr");
            outputCheck.Add("13:00");
            outputCheck.Add("-");
            outputCheck.Add("19:00");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }

        [TestMethod]
        public void DateRangeTest()
        {
            input = "add aaa 1/6 - 5/6 2013";
            output = testStrParser.ParseStringIntoWords(input);
            List<string> outputCheck = new List<string>();
            outputCheck.Add("add");
            outputCheck.Add("aaa");
            outputCheck.Add("1/6");
            outputCheck.Add("-");
            outputCheck.Add("5/6");
            outputCheck.Add("2013");
            Assert.IsTrue(output.Count() == outputCheck.Count);
            for (int i = 0; i < output.Count(); i++)
            {
                Assert.AreEqual(outputCheck[i], output[i]);
            }
            return;
        }
    }
}

