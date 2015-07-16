using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameDataStorageLayer;

namespace GameDataStorageLayerTests
{
    /// <summary>
    /// Summary description for GameDataStorageObjectTest
    /// </summary>
    [TestClass]
    public class GameDataStorageObjectTest
    {
        
        private ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, int>>> attributeData;
        private ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>> modifiedData;
        private ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>> extraData;
        private ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>> descriptorData;
        public GameDataStorageObjectTest()
        {
            ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, int>>> attributeData = new ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, int>>>();
            ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>> modifiedData = new ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>>();
            ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>> extraData = new ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>>();
            ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>> descriptorData = new ConcurrentDictionary<string,BaseGameDataStorageObject<string, Tuple<string, string>>>();

        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestLoadandSearchAttributeData()
        {
            attributeData = GameDataStorageLayerTestUtils.fillAttributeObjectWithData();
            string keyToSearch = "testChar:attributes";
            Tuple<string,Tuple<string,int>> t = attributeData[keyToSearch].searchForKey("testChar:attributes:Strength");
            Assert.AreEqual("testChar:attributes:Strength", t.Item1);
            Assert.AreEqual(10, t.Item2.Item2);
            Assert.AreEqual("testChar/attributes/Strength", t.Item2.Item1);
        }

        [TestMethod]
        public void TestLoadandSearchmodifiedData()
        {
            modifiedData = GameDataStorageLayerTestUtils.fillOtherDataObjectsWithData(GameDataStorageLayerUtils.objectClassType.Modified);
            string keyToSearch = "testChar:modifiedData";
            Tuple<string, Tuple<string, string>> t = modifiedData[keyToSearch].searchForKey("testChar:modifiedData:Extra Strength");
            Assert.AreEqual("testChar:modifiedData:Extra Strength", t.Item1);
            Assert.AreEqual("This creature has extra stength of 8.", t.Item2.Item2);
            Assert.AreEqual("testChar/modifiedData/Extra Strength", t.Item2.Item1);
        }

        [TestMethod]
        public void TestLoadandSearchextraData()
        {
            extraData = GameDataStorageLayerTestUtils.fillOtherDataObjectsWithData(GameDataStorageLayerUtils.objectClassType.Extra);
            string keyToSearch = "testChar:extraData";
            Tuple<string, Tuple<string, string>> t = extraData[keyToSearch].searchForKey("testChar:extraData:Always Fail");
            Assert.AreEqual("testChar:extraData:Always Fail", t.Item1);
            Assert.AreEqual("Always fail a critical hit.", t.Item2.Item2);
            Assert.AreEqual("testChar/extraData/Always Fail", t.Item2.Item1);
        }

        [TestMethod]
        public void TestLoadandSearchdescriptorData()
        {
            descriptorData = GameDataStorageLayerTestUtils.fillOtherDataObjectsWithData(GameDataStorageLayerUtils.objectClassType.Descriptor);
            string keyToSearch = "testChar:descriptorData";
            Tuple<string, Tuple<string, string>> t = descriptorData[keyToSearch].searchForKey("testChar:descriptorData:Demon");
            Assert.AreEqual("testChar:descriptorData:Demon", t.Item1);
            Assert.AreEqual("Demonic Being", t.Item2.Item2);
            Assert.AreEqual("testChar/descriptorData/Demon", t.Item2.Item1);
        }

        [TestMethod]
        public void tryInstantiateGameObject()
        {

            byte[] d = GameDataStorageLayerTestUtils.getSampleCharacterFromXML();
            
            GameDataStorageObject gds = new GameDataStorageObject(d);
            ConcurrentDictionary<string, BaseObject> cd = (ConcurrentDictionary<string, BaseObject>)gds.getDataFromStorageObject(GameDataStorageLayerUtils.objectClassType.Attribute);
            foreach(KeyValuePair<string,BaseObject> kv in cd)
            {
                string type = kv.Value.getClassType();
                if(type == GameDataStorageLayerUtils.objectClassType.Attribute.ToString())
                {
                    BaseGameDataStorageObject<string, Tuple<string, int>> bgso = (BaseGameDataStorageObject<string, Tuple<string, int>>)kv.Value;
                    Tuple<string, Tuple<string,int>> x = bgso.getValueAt(0);
                    Console.WriteLine("Hello world");
                }
                else if(type == GameDataStorageLayerUtils.objectClassType.Descriptor.ToString())
                {
                    BaseGameDataStorageObject<string, Tuple<string, string>> bgso = (BaseGameDataStorageObject<string, Tuple<string, string>>)kv.Value;
                    Tuple<string, Tuple<string, string>> x = bgso.getValueAt(0);
                    Console.WriteLine("Hello world");
                }
            }
        }
    }
}
