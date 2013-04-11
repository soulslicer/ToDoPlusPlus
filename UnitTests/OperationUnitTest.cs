//@alice A0103985Y
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using ToDo;

namespace OperatingUnitTest
{
    /// <summary>
    /// This test is operation unit test,
    /// using "operation.execute" to to check the process of
    /// operation being executed and return response.
    /// contains 8 test cases.
    /// </summary>

    [TestClass]
    public class OperationUnitTest
    {
        // ******************************************************************
        // Parameters
        // ******************************************************************

        #region Parameters
        TaskFloating testTask = new TaskFloating("test", false, -1);
        TaskFloating testTaskNew = new TaskFloating("testa", false, -1);
        string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

        Storage testStorage;
        List<Task> testTaskList;
        Response result;
        SortType sortType = SortType.DEFAULT;
        #endregion

        [TestMethod]
        public void OperationAddTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(testTask, sortType);
            result = Op.Execute(testTaskList, testStorage);
            Assert.AreEqual("Added new task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationAddFailTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(null, sortType);
            result = Op.Execute(testTaskList, testStorage);
            Assert.AreEqual(result.FeedbackString, "Failed to add task!");
            return;
        }

        [TestMethod]
        public void OperationUndoAddTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            result = Op.Undo(testTaskList, testStorage);
            Assert.AreEqual(result.FormatType.ToString(), "DEFAULT");
            return;
        }

        [TestMethod]
        public void OperationDeleteTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index = new int[2] { 1, 1 };
            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual("Deleted task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteRangeFailTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index = new int[2] { 1, 4 };
            OperationDelete Op1;
            OperationAdd Op = new OperationAdd(testTask, sortType);
            result = Op.Execute(testTaskList, testStorage);
            Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual("Invalid task index!", result.FeedbackString);
            index = new int[2] { 1, 1 };
            Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual("Deleted task \"test\" successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationDeleteMultipleTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            int[] index = new int[2] { 1, 2 };
            OperationAdd Op = new OperationAdd(testTask, sortType);
            Op.Execute(testTaskList, testStorage);
            Op = new OperationAdd(testTaskNew, sortType);
            Op.Execute(testTaskList, testStorage);
            OperationDelete Op1 = new OperationDelete("", index, null, null, null, false, SearchType.NONE, sortType);
            result = Op1.Execute(testTaskList, testStorage);
            Assert.AreEqual("Deleted all indicated tasks successfully.", result.FeedbackString);
            return;
        }

        [TestMethod]
        public void OperationSearchTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();
            DateTime timeTest;
            timeTest = DateTime.ParseExact("10/15/2013 5:00 AM", formats,
                                                new CultureInfo("en-US"),
                                                DateTimeStyles.None);
            DateTimeSpecificity specific = new DateTimeSpecificity();
          
            TaskDeadline testDeadline = new TaskDeadline("test", timeTest, specific);
           OperationAdd Op1 = new OperationAdd(testDeadline, sortType);
            OperationSearch Op2 = new OperationSearch("SearchConditionCannotBeMatching",DateTime.Now,timeTest.AddDays(1),specific,SearchType.NONE,SortType.DEFAULT);
            result = Op2.Execute(testTaskList, testStorage);
             Assert.AreEqual("No matching tasks found!", result.FeedbackString);
            result = Op1.Execute(testTaskList, testStorage);
            result = Op2.Execute(testTaskList, testStorage);
         Assert.AreEqual("No matching tasks found!", result.FeedbackString);
           
            return;
        }

        [TestMethod]
        public void OperationSortTest()
        {
            testStorage = new Storage("OpUnittest.xml", "OpUnittestsettings.xml");
            testTaskList = testStorage.LoadTasksFromFile();

            OperationSort Op = new OperationSort(SortType.NAME);
            result = Op.Execute(testTaskList, testStorage);
            Assert.AreEqual("Sorting by name.", result.FeedbackString);
            return;
        }

    }
}
