using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    public class GameDataStorageLayerUtils
    {
        public enum DataStorageAreas
        {
            None = 0,
            Network = 1,
            FileSystem = 2
        };

        public enum LogLevels
        {
            Error = 1,
            Debug = 2,
            Fatal = 3,
            Info = 4
        };

    }
}
