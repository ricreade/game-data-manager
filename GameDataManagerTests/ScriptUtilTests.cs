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
    }
}
