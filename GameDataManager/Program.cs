using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataStorageLayerTests;

namespace GameDataManager
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            GameDataStorageObjectTest test = new GameDataStorageObjectTest();
            test.TestLoadandSearchAttributeData();
        }
    }
}
