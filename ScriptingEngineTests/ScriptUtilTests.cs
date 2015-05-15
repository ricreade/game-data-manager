using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingEngine;
using System.Collections.Generic;

namespace GameDataManagerTests
{
    /// <summary>
    /// Set of tests for the ScriptUtil class.
    /// </summary>
    [TestClass]
    public class ScriptUtilTests
    {
        [TestMethod]
        public void SubmitTransaction()
        {
            Assert.IsNull(ScriptUtil.SubmitTransaction(null));
        }

        /// <summary>
        /// Verifies that the ScriptUtil engine correctly invokes a scripted object instance
        /// method when the script class is known.
        /// </summary>
        [TestMethod]
        public void TestScriptSpecificClass()
        {
            string expectedResult = "Returned value from TestScript!";
            IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs", "TestScript");
            Assert.AreEqual<string>(expectedResult, script.GetTestValue());
        }

        /// <summary>
        /// Verifies that the ScriptUtil engine correctly invokes a scripted object instance
        /// method when the script class is not specified as long as the script file name and
        /// class name are the same (but with different case).
        /// </summary>
        [TestMethod]
        public void TestScriptAnyClass()
        {
            string expectedResult = "Returned value from TestScript!";
            IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs");
            Assert.IsNotNull(script, "The returned script object cannot be null!");
            Assert.AreEqual<string>(expectedResult, script.GetTestValue());
        }

        /// <summary>
        /// Verifies that the ScriptUtil engine correctly invokes scripted object instance
        /// methods to pass an object back and forth with a known mutation in the instance
        /// method.
        /// </summary>
        [TestMethod]
        public void TestScriptPassObject()
        {
            string expectedResult1 = "world";
            string expectedResult2 = "hello";
            int expectedSize = 2;
            IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs");
            List<string> list = new List<string>();
            list.Add("world");
            script.StoreTestObject(list);
            IList<string> newlist = script.RetrieveTestObject();
            Assert.IsNotNull(newlist);
            Assert.AreEqual<int>(expectedSize, newlist.Count);
            Assert.AreEqual<string>(expectedResult1, newlist[0]);
            Assert.AreEqual<string>(expectedResult2, newlist[1]);
        }

        /// <summary>
        /// Verifies that the ScriptUtil correctly throws an exception if the script
        /// invocation references a class name that does not exist.
        /// </summary>
        [TestMethod]
        public void TestScriptBadClassName()
        {
            try
            {
                IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs", "Oops");
            }
            catch (NullReferenceException ex)
            {
                return;
            }
            Assert.Fail("The method should have returned by now.");
        }

        /// <summary>
        /// Verifies that a mismatched file and class name correctly throws an exception
        /// if the class name is not provided to ScriptUtil, and that it functions
        /// correctly if the correct class name is provided.
        /// </summary>
        [TestMethod]
        public void TestScriptMismatchedClassName()
        {
            string expectedResult = "some value";

            IScriptInstanceTest script = null;
            try
            {
                script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testClassMismatch.cs");
            }
            catch (NullReferenceException ex)
            {
                Assert.IsNull(script);
            }

            try
            {
                script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testClassMismatch.cs", "MismatchedClassName");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.IsNotNull(script);
            Assert.AreEqual<string>(expectedResult, script.GetTestValue());
        }
    }
}
