using System;
using ScriptingEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScriptingEngineTests
{
    [TestClass]
    public class ScriptAbilityModifierTests
    {
        [TestMethod]
        public void TestScriptAbilityModifiers()
        {
            string[] scores = new string[] {
                "-2", "-1", "0", "1", "2", "3", "4", "5", 
                "6", "7", "8", "9", "10",
                "11", "12", "13", "14", "15", 
                "16", "17", "18", "19", "20",
                " 1", " 10", "1 ", "10 "
            };

            string[] modifiers = new string[] {
                "-6", "-6", "-5", "-5", "-4", "-4", "-3", "-3", 
                "-2", "-2", "-1", "-1", "0",
                "0", "1", "1", "2", "2", 
                "3", "3", "4", "4", "5",
                "-5", "0", "-5", "0"
            };

            Assert.AreEqual<int>(27, modifiers.Length);
            Assert.AreEqual<int>(modifiers.Length, scores.Length);

            for (int i = 0; i < scores.Length; i++)
            {
                IScriptRequest request = ScriptUtil.CreateRequest(scores[i], "abilitymodifier", "test");
                IScriptResult result = ScriptUtil.ExecuteRequest(request);
                Assert.AreEqual<ScriptResult.ResultType>(ScriptResult.ResultType.Success, result.Result, result.Message);
                Assert.AreEqual<string>(modifiers[i], result.Message, request.Instruction);
            }
        }

        [TestMethod]
        public void TestScriptAbilityModifierError()
        {
            string[] scores = new string[] {
                "alfafa", "", null, "1 0"
            };

            for (int i = 0; i < scores.Length; i++)
            {
                IScriptRequest request = ScriptUtil.CreateRequest(scores[i], "abilitymodifier", "test");
                IScriptResult result = ScriptUtil.ExecuteRequest(request);
                Assert.AreEqual<ScriptResult.ResultType>(ScriptResult.ResultType.Fail, result.Result, request.Instruction);
            }
        }
    }
}
