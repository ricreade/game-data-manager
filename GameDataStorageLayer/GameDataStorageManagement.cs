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
    public class GameDataStorageManagement : BaseGameDataStorageLayer
    {
        private GameDataStorageLayerUtils.DataStorageAreas dataAccessType;
        private string dataType;
        private List<Tuple<string,byte[]>> serializedGameData;
        private string locationString;
        private int dataInterfaceChanged;
        /*
         * We take a single constructor, we need to know three things:
         * 1. Where is the data
         * 2. What is the data type (for now)
         * 3. location of the data to write to/read from
         * */

        public GameDataStorageManagement()
        {
            this.dataAccessType = GameDataStorageLayerUtils.DataStorageAreas.None;
            this.dataType = null;
            this.serializedGameData = null;
            this.locationString = null;
            dataInterfaceChanged = 0;
        }

        public GameDataStorageManagement(GameDataStorageLayerUtils.DataStorageAreas dataAccessType, string dataType, string locationString)
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


        /// <summary>
        /// Open the data on the specified area and location (file name/hostname:uri)
        /// </summary>
        /// <returns>Boolean: true on successful opening and retrieving of storage data, false on failure.</returns>
        public bool openData()
        {
            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.None || dataType == null)
            {
                BaseGameDataStorageLayer.logData("Unable to find location or dataType.", GameDataStorageLayerUtils.LogLevels.Fatal);
                throw new System.ArgumentException("Storage location and dataTypes need to be defined.");
            }

            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.Network)
            {
                serializedGameData = getNetworkData(this.locationString);
                return true;
            }

            if (dataAccessType == GameDataStorageLayerUtils.DataStorageAreas.FileSystem)
            {
                serializedGameData = getSerializedDataFromDisk(this.locationString);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Get the location string we're pointing to.
        /// </summary>
        /// <returns>String with location data.</returns>
        public string getLocationString ()
        {
            return this.locationString;
        }

        /// <summary>
        /// Return a built dictionary of all objects that have been loaded by the library.
        /// </summary>
        /// <returns>A concurrent dictionary with all game Objects</returns>
        public ConcurrentDictionary<string, GameDataStorageObject> getAllObjects()
        {
            ConcurrentDictionary<string, GameDataStorageObject> gameObjectTable = new ConcurrentDictionary<string, GameDataStorageObject>();

            return gameObjectTable;
        }

        /// <summary>
        /// Prepare to store the object, we may have to synchonize before we write
        /// We will first check for locks and then write data as needed.
        /// </summary>
        /// <param name="objectToWrite">All Game objects to write in the dictionary.</param>
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

        /// <summary>
        /// Tries to serialize the object fully to whichever storage medium is needed.
        /// </summary>
        /// <param name="objectKey">The data key to write i.e. character/</param>
        /// <param name="dataToSerialize">object data to write</param>
        /// <returns>true for success, false for failure</returns>
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

        /// <summary>
        /// Get serialized data from a network location.
        /// </summary>
        /// <param name="dataLocation">Network location</param>
        /// <returns>A list of byte arrays to deserialize</returns>
        public List<Tuple<string,byte[]>> getNetworkData(string dataLocation)
        {
            List<Tuple<string,byte[]>> data = null;

            return data;
        }

        /// <summary>
        /// Retrieve all serialized data from the disk, minimal processing needed.
        /// </summary>
        /// <param name="dataLocation">Location on disk to load.</param>
        /// <returns>Array of bytes to deserialize into usable data.</returns>
        public List<Tuple<string,byte[]>> getSerializedDataFromDisk(string dataLocation)
        {
            List<Tuple<string,byte[]>> data = null;
             //Perhaps we should replace this with a LINQ statement
            //as noted here: http://stackoverflow.com/questions/13301053/directory-getfiles-of-certain-extension
            string[] fileNames = System.IO.Directory.GetFiles(dataLocation, "*.xml");
            foreach(var file in fileNames)
            {
                try
                {
                    if( File.Exists(dataLocation + "/" + file))
                    {
                        data.Add(new Tuple<string, byte[]>(file, File.ReadAllBytes(dataLocation + "/" + file)));
                    }
                }
                catch (Exception ex)
                {
                    BaseGameDataStorageLayer.logData("Unable to open and read file due to {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error);
                }
            }
            return data;
        }


        public void prepareToStoreObject()
        {
            throw new NotImplementedException();
        }
    }
}
