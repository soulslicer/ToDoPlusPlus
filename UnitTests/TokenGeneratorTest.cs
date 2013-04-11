//@alice A0103985Y
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace TokenGeneratorTest
{
    /// <summary>
    /// This test is unit test for tokengenerator,
    /// which generates corresponding tokens from given string list.
    /// contains 9 test cases.
    /// </summary>
  
    [TestClass]
    public class TokenGeneratorTest
    {
        TokenGenerator testGenerator = new TokenGenerator();
        List<string> input;
        List<Token> result;

        [TestMethod]
        public void SimpleDaterangeAddTest()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("morning"); 
            input.Add("tmr");
            input.Add("8AM");
            input.Add("TASK");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenTimeRange);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenTime);
            Assert.IsTrue(result[4] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void AddFringeCase()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("task");
            input.Add("today");
            input.Add("to");
            input.Add("sunday");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDay);
            return;
        }

        [TestMethod]
        public void IndexRangeTest()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("1-5");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenIndexRange);
            return;
        }

        [TestMethod] 
        public void IndexRangeFailTest()
        {
            input = new List<string>();
            input.Add("delete");
            input.Add("-1");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            return;
        }

        [TestMethod]
        public void DateTimeDetectTest()
        {
            input = new List<string>();
            input.Add("03.08.14");
            input.Add("03/05/2013");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenDate);
            Assert.IsTrue(result[1] is TokenDate);
            return;
        }

        [TestMethod]
        public void CombinedTest()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaaaa");
            input.Add("1/1/2013");
            input.Add("morning");
            input.Add("to");
            input.Add("3/2/2013");
            input.Add("evening");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(7, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenTimeRange);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenDate);
            Assert.IsTrue(result[6] is TokenTimeRange);
            return;
        }

        [TestMethod]
        public void SortTypeTest() 
        {
            input = new List<string>();
            input.Add("sort");
            input.Add("date");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0] is TokenCommand);
            Assert.IsTrue(result[1] is TokenSortType);
            return;
        }

        [TestMethod]
        public void TimeRangeTest()
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaa");
            input.Add("aa");
            input.Add("tmr");
            input.Add("13:00");
            input.Add("-");
            input.Add("19:00");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(6, result.Count);
            Assert.IsTrue(result[0] is TokenCommand); 
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenTime);
            Assert.IsTrue(result[4] is TokenContext);
            Assert.IsTrue(result[5] is TokenTime);
            return;
        }

        [TestMethod]
        public void DateRangeTest() 
        {
            input = new List<string>();
            input.Add("add");
            input.Add("aaa");
            input.Add("1/6");
            input.Add("-");
            input.Add("5/6");
            input.Add("2013");
            result = testGenerator.GenerateAllTokens(input);
            Assert.AreEqual(6, result.Count);
            Assert.IsTrue(result[0] is TokenCommand); 
            Assert.IsTrue(result[1] is TokenLiteral);
            Assert.IsTrue(result[2] is TokenDate);
            Assert.IsTrue(result[3] is TokenContext);
            Assert.IsTrue(result[4] is TokenDate);
            Assert.IsTrue(result[5] is TokenTime);
            return;
        }
    }
}
