using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    public class GameDataStorageLayer
    {
        private GameDataStorageInterface storageInterface = null;


        public GameDataStorageLayer()
        {
            storageInterface = new GameDataStorageInterface();
        }

        public GameDataStorageLayer(GameDataStorageLayerUtils.DataStorageAreas dataAccessType, string dataType, string dataLocation)
        {
            storageInterface = new GameDataStorageInterface(dataAccessType, dataType, dataLocation);
            primeDataFromStorage();
        }


        private bool primeDataFromStorage()
        {
            return storageInterface.openData();
        }
    }
}
