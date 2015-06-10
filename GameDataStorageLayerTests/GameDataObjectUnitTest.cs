using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GameDataStorageLayer;

namespace GameDataStorageLayerTests
{
    [TestClass]
    public class GameDataObjectUnitTest
    {
        BaseGameDataStorageObject<string, Tuple<string, int>> testObject = new BaseGameDataStorageObject<string, Tuple<string, int>>(GameDataStorageLayerUtils.objectClassType.Attribute);

        /// <summary>
        /// Simple test, validate we get back the class string we initialized the object with
        /// </summary>
        [TestMethod]
        public void TestAttributeObjectHasClassType()
        {
            Assert.AreEqual(testObject.getClassType(), GameDataStorageLayerUtils.objectClassType.Attribute.ToString());
        }

        /// <summary>
        /// Validate we can insert an object into the list we created and it grows as appropriate.
        /// </summary>
        [TestMethod]
        public void TestAttributeObjectInsertIntoList()
        {
            Tuple<string, Tuple<string,int>> testTuple = new Tuple<string, Tuple<string,int>>("Test", new Tuple<string,int>("basePath", 1));
            Assert.IsFalse(testObject.getListSize() > 0);
            Assert.IsTrue(testObject.addTupleToList(testTuple));
            Assert.AreEqual(testObject.getValueAt(0), testTuple);
        }

        /// <summary>
        /// Insert a list and test to make sure we get that list back.
        /// </summary>
        [TestMethod]
        public void TestGetListIsSuccessful()
        {
            //reset the object and release the memory 
            testObject = null;
            testObject = new BaseGameDataStorageObject<string, Tuple<string, int>>(GameDataStorageLayerUtils.objectClassType.Attribute);
            List<Tuple<string, Tuple<string,int>>> l = new List<Tuple<string, Tuple<string,int>>>(20);
            for( int i = 0; i < 20; i++)
            {
                Tuple<string, Tuple<string,int>> test = new Tuple<string,Tuple<string,int>>("listItem{0}" + i, new Tuple<string,int>("path {0}" +i, i));
                l.Add(test);
                testObject.addTupleToList(test);
            }
            Assert.IsTrue(l.Count == 20);
            Assert.IsTrue(testObject.getListSize() == 20);
            Assert.AreEqual(l.GetType(), testObject.getList().GetType());
            for( int i = 0; i < 20; i++)
            {
                Assert.AreEqual(l[i], testObject.getValueAt(i));
            }
        }

        [TestMethod]
        public void removeItemAt()
        {
            for (int i = 0; i < 20; i++)
            {

                Tuple<string, Tuple<string,int>> test = new Tuple<string, Tuple<string,int>>("listItem{0}" + i, new Tuple<string,int>("path {0}"
                     + i, i));
                testObject.addTupleToList(test);
            }
            int itemIndex = 2;
            int listSize = testObject.getListSize();
            Tuple<string, Tuple<string,int>> myData = testObject.removeItemAt(itemIndex);
            Assert.AreNotEqual(myData, testObject.getValueAt(itemIndex));
            Assert.IsTrue(testObject.getListSize() < listSize);

        }

        [TestMethod]
        public void testSetObjectAtAndReplace()
        {
            for (int i = 0; i < 20; i++)
            {
                Tuple<string, Tuple<string, int>> test = new Tuple<string, Tuple<string, int>>("listItem{0}" + i, new Tuple<string, int>("path {0}"
                     + i, i));
                testObject.addTupleToList(test);
            }
            Assert.IsTrue(testObject.getListSize() == 20);
            Tuple<string, Tuple<string,int>> t = new Tuple<string, Tuple<string,int>>("TEST", new Tuple<string,int>("Path 123456",123456));
            testObject.insertTupleAt(t, 4);
            Assert.IsTrue(testObject.getListSize() == 21);
            Assert.IsTrue(testObject.getValueAt(4) == t);

        }

        [TestMethod]
        public void testSetObjectAtAndDontReplace()
        {
            for (int i = 0; i < 20; i++)
            {
                Tuple<string, Tuple<string, int>> test = new Tuple<string, Tuple<string, int>>("listItem{0}" + i, new Tuple<string, int>("path {0}"
                     + i, i));
                testObject.addTupleToList(test);
            }
            Assert.IsTrue(testObject.getListSize() == 20);
            Tuple<string, Tuple<string, int>> t = new Tuple<string, Tuple<string, int>>("TEST", new Tuple<string, int>("Path 123456", 123456));
            testObject.insertTupleAt(t, 8, false);
            Assert.IsTrue(testObject.getListSize() == 21);
            Assert.IsFalse(testObject.getValueAt(4) == t);
            Assert.IsTrue(testObject.getValueAt(20) == t);
        }

        [TestMethod]
        public void removeAllItems()
        {
            for (int i = 0; i < 20; i++)
            {
                Tuple<string, Tuple<string, int>> test = new Tuple<string, Tuple<string, int>>("listItem{0}" + i, new Tuple<string, int>("path {0}"
                     + i, i));
                testObject.addTupleToList(test);
            }

            Assert.IsTrue(testObject.getListSize() == 20);
            testObject.removeAllElements();
            Assert.IsTrue(testObject.getListSize() == 0);
        }

    }
}
