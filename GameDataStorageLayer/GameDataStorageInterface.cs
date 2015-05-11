using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    /*
     * This class is dedicated to working with whatever storage layer we have chosen
     * it will pick up the serialized object and then transform it into the current
     * game object(s) we want to use and load it into memory.
     * 
     * */
    public class GameDataStorageInterface
    {
        private GameDataStorageLayerUtils.DataStorageAreas dataAccessType;
        private string dataType;
        private byte[] serializedGameData;
        private string locationString;
        private int dataInterfaceChanged;
        /*
         * We take a single constructor, we need to know two things:
         * 1. Where is the data
         * 2. What is the data type (for now)
         * */

        public GameDataStorageInterface()
        {
            this.dataAccessType = GameDataStorageLayerUtils.DataStorageAreas.None;
            this.dataType = null;
            this.serializedGameData = null;
            this.locationString = null;
            dataInterfaceChanged = 0;
        }

        public GameDataStorageInterface(GameDataStorageLayerUtils.DataStorageAreas dataAccessType, string dataType, string locationString)
        {
            this.dataAccessType = dataAccessType;
            this.dataType = dataType;
            this.serializedGameData = null;
            this.locationString = locationString;
            dataInterfaceChanged = 0;
        }


        public void setDataLocation(GameDataStorageLayerUtils.DataStorageAreas dataAccessType, string dataType, string locationString)
        {
            this.dataAccessType = dataAccessType;
            this.dataType = dataType;
            this.locationString = locationString;
            dataInterfaceChanged++;
        }


        //We must have a location and definition to proceed
        public bool openData()
        {
            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.None || dataType == null)
            {
                throw new System.ArgumentException("Storage location and dataTypes need to be defined.");
            }

            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.Network)
            {
                serializedGameData = getNetworkData(locationString);
                return true;
            }

            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.FileSystem)
            {
                serializedGameData = getSerializedDataFromDisk(locationString);
                return true;
            }

            return false;
        }

        public GameDataStorageObject getObject()
        {
            return new GameDataStorageObject(serializedGameData);
        }

        public byte[] getNetworkData(string dataLocation)
        {
            byte[] data = null;

            return data;
        }


        public byte[] getSerializedDataFromDisk(string dataLocation)
        {
            byte[] data = null;

            return data;
        }

    }
}
