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

        /// <summary>
        /// Verifies that the ScriptUtil factory method returns a script request
        /// with appropriate values if the instruction and script name (but not
        /// class name) are provided.
        /// </summary>
        [TestMethod]
        public void TestCreateScriptRequestNoClassName()
        {
            string instr = "This is an instruction with a pre-defined statement.";
            string script = "This is a script file name.";

            IScriptRequest request = ScriptUtil.CreateRequest(instr, script, "test");

            Assert.AreEqual<string>(instr, request.Instruction);
            Assert.AreEqual<string>(script, request.ScriptName);
            Assert.IsNull(request.ScriptClassName);
        }

        /// <summary>
        /// Verifies that the ScriptUtil factory method returns a script request
        /// with appropriate values if the instruction, script name, and class 
        /// name are provided.
        /// </summary>
        [TestMethod]
        public void TestCreateScriptRequestClassName()
        {
            string instr = "My instruction.";
            string script = "myscript.cs";
            string classname = "MyScript";

            IScriptRequest request = ScriptUtil.CreateRequest(instr, script, classname, "test");

            Assert.AreEqual<string>(instr, request.Instruction);
            Assert.AreEqual<string>(script, request.ScriptName);
            Assert.AreEqual<string>(classname, request.ScriptClassName);
        }

        /// <summary>
        /// Verifies that the ScriptUtil factory method returns a script result
        /// with appropriate values.
        /// </summary>
        [TestMethod]
        public void TestCreateScriptResult()
        {
            ScriptResult.ResultType resulttype = ScriptResult.ResultType.Success;
            string message = "My result message.";

            IScriptResult result = ScriptUtil.CreateResult(resulttype, message);

            Assert.AreEqual<ScriptResult.ResultType>(resulttype, result.Result);
            Assert.AreEqual<string>(message, result.Message);
        }

        /// <summary>
        /// Verifies that the ScriptUtil factory method returns a functional transaction
        /// object and that the transaction object add and remove methods work as
        /// expected.
        /// </summary>
        [TestMethod]
        public void TestCreateTransaction()
        {
            bool rollback = true;
            string instr = "My instruction.";
            string script = "script.cs";
            string className = "Script";
            string instr2 = "My other instruction.";
            IScriptRequest request;

            IScriptTransaction trans = ScriptUtil.CreateTransaction(rollback);

            Assert.AreEqual<bool>(rollback, trans.RollbackOnFail);
            Assert.IsNotNull(trans.Requests);
            Assert.AreEqual<int>(0, trans.Requests.Count);

            trans.AddRequest(ScriptUtil.CreateRequest(instr, script, className));

            Assert.AreEqual<int>(1, trans.Requests.Count);

            request = trans.GetRequest(false);

            Assert.AreEqual<string>(instr, request.Instruction);
            Assert.AreEqual<int>(1, trans.Requests.Count);

            trans.AddRequest(ScriptUtil.CreateRequest(instr2, script, className));
            request = trans.GetRequest(true);

            Assert.AreEqual<string>(instr, request.Instruction);
            Assert.AreEqual<int>(1, trans.Requests.Count);

            request = trans.GetRequest();

            Assert.AreEqual<string>(instr2, request.Instruction);
            Assert.AreEqual<int>(0, trans.Requests.Count);

            request = trans.GetRequest();

            Assert.IsNull(request);

            request = trans.GetRequest(true);

            Assert.IsNull(request);
        }

        /// <summary>
        /// Verifies that the ScriptUtil.CreateDelimitedArgString and
        /// ScriptUtil.SplitScriptString function as expected.
        /// </summary>
        [TestMethod]
        public void TestDelimitedArgString()
        {
            string expected = string.Format(
                "agent=testagent{0}target=testtarget,othertarget{0}source=somesource{0}options=someoptions", ScriptUtil.Separator);
            string[] args = new string[] { 
                "agent=testagent",
                "target=testtarget,othertarget",
                "source=somesource",
                "options=someoptions"
            };
            string result1 = ScriptUtil.CreateDelimitedArgString(args);
            Assert.AreEqual<string>(expected, result1);

            string[] result2 = ScriptUtil.SplitScriptString(result1);

            for (int i = 0; i < result2.Length; i++)
            {
                Assert.AreEqual<string>(args[i], result2[i]);
            }
        }
    }
}
