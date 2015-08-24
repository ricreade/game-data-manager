using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingEngine;

namespace ScriptingEngineTests
{
    [TestClass]
    public class ScriptDieRollTests
    {
        private const string SCRIPT_NAME = "dieroll";
        /// <summary>
        /// Tests a simple die roll.
        /// </summary>
        [TestMethod]
        public void TestSimpleRollDie()
        {
            string[] instructions = new string[] {
                "{2d6}",
                "{10d6}",
                "{12d4}",
                "{0d8}",
                "aardvaark",
                "{100000d0}",
                "{5D1}",
                "{12D12}"
            };
            IScriptRequest[] requests = GetRequestArray(instructions);
            IScriptResult[] results = GetResultArray(requests);

            AssertIntRange(results[0].Message, 2, 12, "0");
            AssertIntRange(results[1].Message, 10, 60, "1");
            AssertIntRange(results[2].Message, 12, 48, "2");
            AssertIntRange(results[3].Message, 0, 0, "3");
            AssertEmptyString(results[4].Message, "4");
            AssertIntRange(results[5].Message, 0, 0, "5");
            AssertIntRange(results[6].Message, 1, 5, "6");
            AssertIntRange(results[7].Message, 12, 144, "7");

        }

        /// <summary>
        /// Tests a die roll with a type component.
        /// </summary>
        [TestMethod]
        public void TestTypedDieRoll()
        {
            string stype, sval;
            string[] instructions = new string[] {
                "{2d6} fire",
                "{10d6} my dude man",
                "{12d4}BROKEN STUFF  ",
                "{0d8} #@3fee9",
                "aardvaark missing",
                "{100000d0} jfsjei ieslfj seif323 ##$@#%",
                "{5D1}-1",
                "{12D12}/15 * 7"
            };
            IScriptRequest[] requests = GetRequestArray(instructions);
            IScriptResult[] results = GetResultArray(requests);

            GetTypeParts(results[0].Message, out sval, out stype);
            AssertIntRange(sval, 2, 12, "0");
            AssertTypeString("(fire)", stype, "0");

            GetTypeParts(results[1].Message, out sval, out stype);
            AssertIntRange(sval, 10, 60, "1");
            AssertTypeString("(my dude man)", stype, "1");

            GetTypeParts(results[2].Message, out sval, out stype);
            AssertIntRange(sval, 12, 48, "2");
            AssertTypeString("(BROKEN STUFF)", stype, "2");

            GetTypeParts(results[3].Message, out sval, out stype);
            AssertIntRange(sval, 0, 0, "3");
            AssertTypeString("(#@3fee9)", stype, "3");

            AssertEmptyString(results[4].Message, "4");

            GetTypeParts(results[5].Message, out sval, out stype);
            AssertIntRange(sval, 0, 0, "5");
            AssertTypeString("(jfsjei ieslfj seif323 ##$@#%)", stype, "5");

            GetTypeParts(results[6].Message, out sval, out stype);
            AssertIntRange(sval, 1, 5, "6");
            AssertTypeString("(-1)", stype, "6");

            GetTypeParts(results[7].Message, out sval, out stype);
            AssertIntRange(sval, 12, 144, "7");
            AssertTypeString("(/15 * 7)", stype, "7");

        }

        /// <summary>
        /// Tests a die roll with a modifier with an without a type component.
        /// </summary>
        [TestMethod]
        public void TestTypedModifiedDieRoll()
        {
            string stype, sval;
            string[] instructions = new string[] {
                "{2d6 + 6} fire",
                "{10d6-1} my dude man",
                "{12d4*3}BROKEN STUFF  ",
                "{0d8 -2}",
                "aardvaark missing + 8",
                "{100000d0- 8} jfsjei ieslfj seif323 ##$@#%",
                "{5D1  +   4 }-1",
                "{12D12    -    100}"
            };
            IScriptRequest[] requests = GetRequestArray(instructions);
            IScriptResult[] results = GetResultArray(requests);

            GetTypeParts(results[0].Message, out sval, out stype);
            AssertIntRange(sval, 8, 18, "0");
            AssertTypeString("(fire)", stype, "0");

            GetTypeParts(results[1].Message, out sval, out stype);
            AssertIntRange(sval, 9, 59, "1");
            AssertTypeString("(my dude man)", stype, "1");

            AssertEmptyString(results[2].Message, "2");

            GetTypeParts(results[3].Message, out sval, out stype);
            AssertIntRange(sval, -2, -2, "3");
            AssertEmptyString(stype, "3");

            AssertEmptyString(results[4].Message, "4");

            GetTypeParts(results[5].Message, out sval, out stype);
            AssertIntRange(sval, -8, -8, "5");
            AssertTypeString("(jfsjei ieslfj seif323 ##$@#%)", stype, "5");

            GetTypeParts(results[6].Message, out sval, out stype);
            AssertIntRange(sval, 5, 9, "6");
            AssertTypeString("(-1)", stype, "6");

            GetTypeParts(results[7].Message, out sval, out stype);
            AssertIntRange(sval, -88, 44, "7");
            AssertEmptyString(stype, "7");

        }

        /// <summary>
        /// Tests a die roll with and without modifier, with and without a type component
        /// where multiple rolls are combined in the same request.
        /// </summary>
        [TestMethod]
        public void TestTypedModifiedGroupDieRoll()
        {
            string stype, sval;
            string[] resultArray;
            string sep = ScriptUtil.Separator;

            string[] instructions = new string[] {
                "{2d6 + 6} fire" + sep + "{10d6-1} my dude man" + sep + "aardvaark missing + 8",
                "{0d8 -2} #@3fee9" + sep + "{5D1  +   4 }" + sep + "{0d8 + 15} #@3fee9",
                "{12d4*3}BROKEN STUFF  " + sep + "aardvaark missing + 8" + sep,
                "{100000d0- 8} jfsjei ieslfj seif323 ##$@#%" + sep + "{12D1} jfsjei ieslfj seif323 ##$@#%" +
                    sep + "{2d6 + 6} fire" + sep + "{5D1 + 6} lightning"
            };
            IScriptRequest[] requests = GetRequestArray(instructions);
            IScriptResult[] results = GetResultArray(requests);
            
            resultArray = GetResultStringList(results[0].Message);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual<int>(2, resultArray.Length);
            // result 0, part 0
            GetTypeParts(resultArray[0], out sval, out stype);
            AssertIntRange(sval, 8, 18, "(0,0)");
            AssertTypeString("(fire)", stype, "(0,0)");
            // result 0, part 1
            GetTypeParts(resultArray[1], out sval, out stype);
            AssertIntRange(sval, 9, 59, "(0,1)");
            AssertTypeString("(my dude man)", stype, "(0,1)");

            resultArray = GetResultStringList(results[1].Message);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual<int>(2, resultArray.Length);
            // result 1, part 0
            GetTypeParts(resultArray[0], out sval, out stype);
            AssertIntRange(sval, 13, 13, "(1,0)");
            AssertTypeString("(#@3fee9)", stype, "(1,0)");
            // result 1, part 1
            GetTypeParts(resultArray[1], out sval, out stype);
            AssertIntRange(sval, 9, 9, "(1,1)");
            AssertEmptyString(stype, "(1,1)");

            resultArray = GetResultStringList(results[2].Message);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual<int>(0, resultArray.Length);

            resultArray = GetResultStringList(results[3].Message);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual<int>(3, resultArray.Length);
            // result 3, part 0
            GetTypeParts(resultArray[0], out sval, out stype);
            AssertIntRange(sval, 4, 4, "(3,0)");
            AssertTypeString("(jfsjei ieslfj seif323 ##$@#%)", stype, "(3,0)");
            // result 3, part 1
            GetTypeParts(resultArray[1], out sval, out stype);
            AssertIntRange(sval, 8, 18, "(3,1)");
            AssertTypeString("(fire)", stype, "(3,1)");
            // result 3, part 2
            GetTypeParts(resultArray[2], out sval, out stype);
            AssertIntRange(sval, 11, 11, "(3,2)");
            AssertTypeString("(lightning)", stype, "(3,2)");

        }

        /// <summary>
        /// Assert that the value input is an empty string.  Use the result id for feedback.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="resultid">Reference id string.</param>
        private void AssertEmptyString(string value, string resultid)
        {
            Assert.AreEqual<string>(String.Empty, value, String.Format("Result {0} is not empty!", resultid));
        }

        /// <summary>
        /// Assert that the string value is an integer that falls within the low and high
        /// values given, inclusive.  Use the result id for feedback.
        /// </summary>
        /// <param name="value">The string value to test.</param>
        /// <param name="low">The lowest valid value for the integer.</param>
        /// <param name="high">The highest valid value for the integer.</param>
        /// <param name="resultid">Reference id string.</param>
        private void AssertIntRange(string value, int low, int high, string resultid)
        {
            int val;
            if (Int32.TryParse(value, out val))
            {
                Assert.IsTrue(val >= low && val <= high);
            }
            else
            {
                Assert.Fail(String.Format("Result {0} is not a number!  Got '{1}'.", resultid, value));
            }
        }

        /// <summary>
        /// Assert that the string expected and received values match.  Use the result id
        /// for feedback.
        /// </summary>
        /// <param name="expected">The expected type string.</param>
        /// <param name="received">The received type string.</param>
        /// <param name="resultid">Reference id string.</param>
        private void AssertTypeString(string expected, string received, string resultid)
        {
            Assert.AreEqual<string>(expected, received, String.Format(
                "Result {0} type mismatch!  Expected {1}, got {2}.", resultid, expected, received));
        }

        /// <summary>
        /// Returns an array of results from the specified result list.  Each
        /// result will then be evaluated against the expected results.
        /// </summary>
        /// <param name="instructionList"></param>
        /// <returns></returns>
        private string[] GetResultStringList(string resultList)
        {
            return ScriptUtil.SplitScriptString(resultList);
        }

        /// <summary>
        /// Return an array of script requests using the specified instructions array.
        /// </summary>
        /// <param name="instructions">The array of string instructions from which to
        /// create the requests.</param>
        /// <returns>An array of IScriptRequest objects, or null if the string array
        /// is null or empty.</returns>
        private IScriptRequest[] GetRequestArray(string[] instructions)
        {
            if (instructions != null && instructions.Length > 0)
            {
                IScriptRequest[] requests = new IScriptRequest[instructions.Length];
                for (int i = 0; i < instructions.Length; i++)
                {
                    requests[i] = ScriptUtil.CreateRequest(instructions[i], SCRIPT_NAME);
                }
                return requests;
            }
            return null;
        }

        /// <summary>
        /// Return an array of script results using the specified requests array.
        /// </summary>
        /// <param name="requests">The array of requests to execute and retrieve
        /// results.</param>
        /// <returns>An array of IScriptResult objects, or null if the request array
        /// is null or empty.</returns>
        private IScriptResult[] GetResultArray(IScriptRequest[] requests)
        {
            if (requests != null && requests.Length > 0)
            {
                IScriptResult[] results = new IScriptResult[requests.Length];
                for (int i = 0; i < requests.Length; i++)
                {
                    results[i] = ScriptUtil.ExecuteRequest(requests[i]);
                }
                return results;
            }
            return null;
        }

        /// <summary>
        /// Extracts the value and type portions of the resulting die roll result message.
        /// </summary>
        /// <param name="msg">The input message to parse.</param>
        /// <param name="sval">The value portion to return.</param>
        /// <param name="stype">The type portion to return.</param>
        private void GetTypeParts(string msg, out string sval, out string stype)
        {
            string[] parts = msg.Split(new char[] { ' ' }, 2);
            if (parts.Length > 0)
                sval = parts[0];
            else
                sval = "";

            if (parts.Length > 1)
                stype = parts[1];
            else
                stype = "";
        }
    }
}
