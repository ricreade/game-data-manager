using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingEngine;

namespace GameDataManagerTests
{
    [TestClass]
    public class ScriptUtilTests
    {
        [TestMethod]
        public void SubmitTransaction()
        {
            Assert.IsNull(ScriptUtil.SubmitTransaction(null));
        }

        [TestMethod]
        public void TestScriptSpecificClass()
        {
            string expectedResult = "Returned value from TestScript!";
            IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs", "TestScript");
            Assert.AreEqual<string>(expectedResult, script.GetTestValue());
        }

        [TestMethod]
        public void TestScriptAnyClass()
        {
            string expectedResult = "Returned value from TestScript!";
            IScriptInstanceTest script = (IScriptInstanceTest)ScriptUtil.GetScriptObject(@"testscript.cs");
            Assert.IsNotNull(script, "The returned script object cannot be null!");
            Assert.AreEqual<string>(expectedResult, script.GetTestValue());
        }
    }
}
