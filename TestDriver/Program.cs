using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataStorageLayerTests;

namespace TestDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDataStorageObjectTest test = new GameDataStorageObjectTest();
            test.TestLoadandSearchAttributeData();
        }
    }
}
