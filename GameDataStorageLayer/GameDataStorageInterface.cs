using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;


namespace GameDataStorageLayer
{
    /*
     * This class is dedicated to working with whatever storage layer we have chosen
     * it will pick up the serialized object and then transform it into the current
     * game object(s) we want to use and load it into memory.
     * 
     * */
    public class GameDataStorageInterface : BaseGameDataStorageLayer
    {
        private GameDataStorageLayerUtils.DataStorageAreas dataAccessType;
        private string dataType;
        private List<byte[]> serializedGameData;
        private string locationString;
        private int dataInterfaceChanged;
        /*
         * We take a single constructor, we need to know three things:
         * 1. Where is the data
         * 2. What is the data type (for now)
         * 3. location of the data to write to/read from
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
                BaseGameDataStorageLayer.logData("Unable to find location or dataType.", GameDataStorageLayerUtils.LogLevels.Fatal);
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

        public string getStorageDestination()
        {
            return this.locationString;
        }

        public ConcurrentDictionary<string, GameDataStorageObject> getObject()
        {
            ConcurrentDictionary<string, GameDataStorageObject> gameObjectTable = new ConcurrentDictionary<string, GameDataStorageObject>();

            return gameObjectTable;
        }

        public void prepareToStoreObject(ConcurrentDictionary<string, GameDataStorageObject> objectToWrite)
        {
            try
            {
                foreach (KeyValuePair<string, GameDataStorageObject> kvpair in objectToWrite)
                {
                    writeObjectToDestination(kvpair.Key, kvpair.Value);
                }
            }
            catch (Exception e)
            {
                string exception = "Error unable to try to store object due to {0}" + e.Message;
                BaseGameDataStorageLayer.logData(exception, GameDataStorageLayerUtils.LogLevels.Error);
            }
        }


        public bool writeObjectToDestination(string objectKey, GameDataStorageObject dataToSerialize)
        {
            bool successfulWrite = false;
            
            switch (this.dataAccessType)
            {
                case GameDataStorageLayerUtils.DataStorageAreas.Network:

                    successfulWrite = true;
                    break;
                case GameDataStorageLayerUtils.DataStorageAreas.FileSystem:
                    try
                    {
                        XmlSerializer x = new System.Xml.Serialization.XmlSerializer(dataToSerialize.GetType());
                        x.Serialize(new StreamWriter(this.locationString), dataToSerialize);
                        successfulWrite = true;
                    } catch(Exception e)
                    {
                        BaseGameDataStorageLayer.logData("Unable to serialize object to destinaton {0} due to {1}" + this.locationString + e.Message, GameDataStorageLayerUtils.LogLevels.Error);
                    }
                    
                    break;

                default:
                    return false;
            }

            return successfulWrite;
        }

        public List<byte[]> getNetworkData(string dataLocation)
        {
            List<byte[]> data = null;

            return data;
        }


        public List<byte[]> getSerializedDataFromDisk(string dataLocation)
        {
            List<byte[]> data = null;

            return data;
        }


        internal void prepareToStoreObject()
        {
            throw new NotImplementedException();
        }
    }
}
