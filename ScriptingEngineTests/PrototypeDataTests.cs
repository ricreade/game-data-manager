using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingEngine;

namespace ScriptingEngineTests
{
    [TestClass]
    public class PrototypeDataTests
    {
        const string DIR_PATH = @"../../../ScriptingEngineTests/data/";

        /// <summary>
        /// The implementation can read from the data store without errors.
        /// </summary>
        [TestMethod]
        public void TestScriptReadDataSource()
        {
            PrototypeDataLayer datalayer = new PrototypeDataLayer(new System.IO.DirectoryInfo(DIR_PATH));
        }

        /// <summary>
        /// The implementation can retrieve a data record from the dictionary
        /// and use the information stored in that reference to look up
        /// another related data record and retrieve an expected keyed
        /// string value.
        /// </summary>
        [TestMethod]
        public void TestScriptReadAndRetrieve()
        {
            PrototypeDataLayer datalayer = new PrototypeDataLayer(new System.IO.DirectoryInfo(DIR_PATH));

            PrototypeDataObject player = null;
            datalayer.Library.TryGetValue("0x7d9e01", out player);
            PrototypeDataObject character = null;
            datalayer.Library.TryGetValue(player.getValue("char"), out character);
            Assert.AreEqual("Grobo Orinockle", character.getValue("name"), true);
        }

        /// <summary>
        /// Attempt to cast a spell at a character.
        /// </summary>
        [TestMethod]
        public void TestScriptCastSpell()
        {
            IScriptRequest request;
            IScriptResult result;
            string instr = "agent=0x159a753|source=|target=0x053d7a|options=cl10,ref15";
            PrototypeDataLayer datalayer = new PrototypeDataLayer(new System.IO.DirectoryInfo(DIR_PATH));

            request = ScriptUtil.CreateRequest(instr, "spells");

            result = ScriptUtil.ExecuteRequest(request);

            Assert.AreEqual<ScriptResult.ResultType>(ScriptResult.ResultType.Success, result.Result);
        }
    }
}
