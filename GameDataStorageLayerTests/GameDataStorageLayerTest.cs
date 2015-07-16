using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameDataStorageLayer;

namespace GameDataStorageLayerTests
{
    [TestClass]
    public class GameDataStorageLayerTest
    {
        [TestMethod]
        public void QuickTest()
        {
            GameDataStorageLayerClass gdc = new GameDataStorageLayerClass();
            Assert.AreEqual(1000, gdc.listLength());
            gdc.printList();
        }
    }
}
