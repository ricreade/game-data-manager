using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingEngine;

namespace ScriptingEngineTests
{
    [TestClass]
    public class ScriptDieRollTests
    {
        /// <summary>
        /// Tests a simple die roll.
        /// </summary>
        [TestMethod]
        public void TestSimpleRollDie()
        {
            IScriptRequest[] requests = new IScriptRequest[] {
                ScriptUtil.CreateRequest("{2d6}", "dieroll"),
                ScriptUtil.CreateRequest("{10d6}", "dieroll"),
                ScriptUtil.CreateRequest("{12d4}", "dieroll"),
                ScriptUtil.CreateRequest("{0d8}", "dieroll"),
                ScriptUtil.CreateRequest("aardvaark", "dieroll"),
                ScriptUtil.CreateRequest("{100000d0}", "dieroll"),
                ScriptUtil.CreateRequest("{5D1}", "dieroll"),
                ScriptUtil.CreateRequest("{12D12}", "dieroll")
            };

            IScriptResult res0 = ScriptUtil.ExecuteRequest(requests[0]);
            IScriptResult res1 = ScriptUtil.ExecuteRequest(requests[1]);
            IScriptResult res2 = ScriptUtil.ExecuteRequest(requests[2]);
            IScriptResult res3 = ScriptUtil.ExecuteRequest(requests[3]);
            IScriptResult res4 = ScriptUtil.ExecuteRequest(requests[4]);
            IScriptResult res5 = ScriptUtil.ExecuteRequest(requests[5]);
            IScriptResult res6 = ScriptUtil.ExecuteRequest(requests[6]);
            IScriptResult res7 = ScriptUtil.ExecuteRequest(requests[7]);

            int val = 0;
            if (Int32.TryParse(res0.Message, out val))
            {
                Assert.IsTrue(val >= 2 && val <= 12);
            }
            else
            {
                Assert.Fail("result 0 is not a number");
            }

            if (Int32.TryParse(res1.Message, out val))
            {
                Assert.IsTrue(val >= 10 && val <= 60);
            }
            else
            {
                Assert.Fail("result 1 is not a number");
            }

            if (Int32.TryParse(res2.Message, out val))
            {
                Assert.IsTrue(val >= 12 && val <= 48);
            }
            else
            {
                Assert.Fail("result 2 is not a number");
            }

            if (Int32.TryParse(res3.Message, out val))
            {
                Assert.AreEqual<int>(0, val);
            }
            else
            {
                Assert.Fail("result 3 is not a number");
            }

            Assert.AreEqual<string>(string.Empty, res4.Message, "result 4 is not empty");

            if (Int32.TryParse(res5.Message, out val))
            {
                Assert.AreEqual<int>(0, val);
            }
            else
            {
                Assert.Fail("result 5 is not a number");
            }

            if (Int32.TryParse(res6.Message, out val))
            {
                Assert.IsTrue(val >= 1 && val <= 5);
            }
            else
            {
                Assert.Fail("result 6 is not a number");
            }

            if (Int32.TryParse(res7.Message, out val))
            {
                Assert.IsTrue(val >= 12 && val <= 144);
            }
            else
            {
                Assert.Fail("result 7 is not a number");
            }
        }
    }
}
