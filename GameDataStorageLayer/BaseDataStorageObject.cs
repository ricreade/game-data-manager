using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace GameDataStorageLayer
{
    /// <summary>
    /// Storage attribute values in a fast retrieval object
    /// Support up to n primary and n secondary attributes
    /// </summary>
    public class BaseGameDataStorageObject<DataType1, DataType2> : BaseGameObject<DataType1, DataType2>
    {
        private List<Tuple<DataType1, DataType2>> dataList;
        private GameDataStorageLayerUtils.objectClassType classType;

        public BaseGameDataStorageObject(GameDataStorageLayerUtils.objectClassType typeOfObject)
        {
            this.classType = typeOfObject;
            dataList = new List<Tuple<DataType1, DataType2>>();
        }

        public BaseGameDataStorageObject(GameDataStorageLayerUtils.objectClassType typeOfObject, int sizeOfList)
        {
            dataList = new List<Tuple<DataType1, DataType2>>(sizeOfList);
            this.classType = typeOfObject;
        }

        /// <summary>
        /// Adds a Tuple object to the end of the list.
        /// </summary>
        /// <param name="insertedData">Tuple of attribute data.</param>
        /// <returns>Return success on successful insert, else false and log an error</returns>
        public bool addTupleToList(Tuple<DataType1,DataType2> insertedData)
        {
            bool insertSuccessful = false;
            try
            {
                dataList.Add(insertedData);
                insertSuccessful = true;
            }
            catch(Exception ex)
            {
                BaseGameDataStorageLayer.logData("Unable to insert into the list due to {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error);
            }
            return insertSuccessful;
        }

        /// <summary>
        /// Size of the list
        /// </summary>
        /// <returns>Returns the size of the list.</returns>
        public int getListSize()
        {
            return dataList.Count;
        }

        /// <summary>
        /// Simple method to verify what class we are.
        /// </summary>
        /// <returns></returns>
        public string getClassType() 
        {
            return this.classType.ToString();
        }

        /// <summary>
        /// Try to fetch the tuple at the index specified, return null if nothing was found or we ran into an exception.
        /// </summary>
        /// <param name="index">Index of an attribute</param>
        /// <returns>Tuple<string,int> if no exception is reached, null otherwise.</returns>
        public Tuple<DataType1, DataType2> getValueAt(int index)
        {
            try
            {
                return dataList[index];
            }
            catch(Exception ex)
            {
                BaseGameDataStorageLayer.logData("Error accessing attributeList {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error); 
            }
            return null;
        }

        /// <summary>
        /// Searches for a key within the data structure returns it if the object is found, returns an empty object on failure.
        /// </summary>
        /// <param name="key">Value to search for</param>
        /// <returns>Empty object if nothing, existing object on success.</returns>
        public Tuple<DataType1, DataType2> searchForKey(string key)
        {
            Tuple<DataType1, DataType2> lookingFor = null;
            foreach(var item in dataList)
            {
                string theItem = Convert.ToString(item.Item1);
                if( theItem == key )
                {
                    lookingFor = item;
                    break;
                }
            }
            return lookingFor;
        }


        /// <summary>
        /// Insert a tuple at the index specified, if we don't want to replace something we'll add it to the end instead.
        /// </summary>
        /// <param name="tupleData">Data we want to add to the list.</param>
        /// <param name="index">Index where we want to add it at.</param>
        /// <param name="replace">Whether we want to replace the object at index, defaultly true.</param>
        /// <returns>Returns boolean saying whether we were succesful or not.</returns>
        public bool insertTupleAt(Tuple<DataType1, DataType2> tupleData, int index, bool replace = true)
        {
            bool insertSuccessful = false;

            if( !replace )
            {
                return addTupleToList(tupleData);
            }

            try
            {
                dataList.Insert(index, tupleData);
                insertSuccessful = true;
            } catch(Exception ex)
            {
                BaseGameDataStorageLayer.logData("Unable to insert data into the list due to {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error);
            }
            return insertSuccessful;
        }

        /// <summary>
        /// Get a full view of all the tuples and return the list.
        /// </summary>
        /// <returns>Returns a full list of all Tuples in this Attribute object</returns>
        public List<Tuple<DataType1, DataType2>> getList()
        {
            return dataList;
        }

        /// <summary>
        /// Try to remove an item from the list at index
        /// </summary>
        /// <param name="index">Index to remove the element at.</param>
        /// <returns>The removed element. Or null if no element.</returns>
        public Tuple<DataType1, DataType2> removeItemAt(int index)
        {
            try
            {
                Tuple<DataType1, DataType2> removedElement = dataList[index];
                dataList.RemoveAt(index);
                return removedElement;
            } catch(Exception ex)
            {
                BaseGameDataStorageLayer.logData("Unable to remove element due to {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error);
            }
            return null;
        }

        /// <summary>
        /// Empty the list.
        /// </summary>
        /// <returns>True if successful, otherwise false.</returns>
        public bool removeAllElements()
        {
            try
            {
                dataList.RemoveRange(0, dataList.Count);
                return true;
            } catch (Exception ex)
            {
                BaseGameDataStorageLayer.logData("Failed to clear out all elements in the list due to {0}" + ex.Message, GameDataStorageLayerUtils.LogLevels.Error);
                return false;
            }
        }
    }
}
