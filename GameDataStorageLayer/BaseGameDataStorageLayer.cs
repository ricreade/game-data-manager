using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using log4net;

namespace GameDataStorageLayer
{
    public class BaseGameDataStorageLayer
    {
            private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public static void logData(string data, GameDataStorageLayerUtils.LogLevels level)
            {
                switch(level)
                {
                    case GameDataStorageLayerUtils.LogLevels.Info:
                        log.Info(data);
                        break;
                    case GameDataStorageLayerUtils.LogLevels.Debug:
                        log.Debug(data);
                        break;
                    case GameDataStorageLayerUtils.LogLevels.Error:
                        log.Error(data);
                        break;
                    case GameDataStorageLayerUtils.LogLevels.Fatal:
                        log.Fatal(data);
                        break;
                    default:
                        log.Info(data);
                        break;
                }
            }
    }
}
