using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    /* This is the main management storage layer for the service.
     * This interacts with the storage layer iterface to store/retrieve objects
     * This will hold the centralized view of all the objects that we currently have in memory.
     * It is responsible for making sure updates are propagated to the backing storage.
     * It will also allow for updates and load game objects as needed.
     * */
    public class GameDataStorageLayerManager : BaseGameDataStorageLayer
    {
        private GameDataStorageManagement storageManagement = null;
        private ConcurrentDictionary<string, GameDataStorageObject> storageObjects;
        private GameDataStorageLayerUtils.DataStorageAreas dataAccessType;

        public GameDataStorageLayerManager()
        {
            storageManagement = new GameDataStorageManagement();
            this.dataAccessType = GameDataStorageLayerUtils.DataStorageAreas.None;
            this.storageObjects = null;
        }

        public GameDataStorageLayerManager(GameDataStorageLayerUtils.DataStorageAreas dataAccessType, string dataType, string dataLocation)
        {
            storageManagement = new GameDataStorageManagement(dataAccessType, dataType, dataLocation);
            this.dataAccessType = dataAccessType;
            primeDataFromStorage();
            this.storageObjects = storageManagement.getAllObjects();
        }

        /// <summary>
        /// Call the data storage layer to prime the data into memory, this should only be done once.
        /// </summary>
        /// <returns>Boolean: true on success, false on failure</returns>
        private bool primeDataFromStorage()
        {
            return storageManagement.openData();
        }

        /// <summary>
        /// On shutdown, or after a lot of changes, we'll flush the data to the backing store.
        /// We shouldn't need to re-sync after storage has been updated.
        /// </summary>
        public void writeDataToStorage()
        {
            storageManagement.prepareToStoreObject(this.storageObjects);
        }
    }
}
