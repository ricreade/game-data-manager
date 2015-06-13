using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace GameDataStorageLayer
{
    [Serializable()]
    public class GameDataStorageObject : BaseGameDataStorageLayer
    {
        private ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, int>>> attributeData;
        private ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>> descriptorData;
        private ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>> modifiedData;
        private ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>> extraData;
        private int totalByteLength;

        public GameDataStorageObject(byte[] serializedGameData)
        {
            attributeData = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string,int>>>();
            descriptorData = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string,string>>>();
            modifiedData = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>>();
            extraData = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>>();
            this.totalByteLength = 0;
        }

        /// <summary>
        /// Insert BaseGameDataStorageObject into a concurrent dictionary
        /// </summary>
        /// <typeparam name="TValue1">Templated value type for the first BaseGameDataStorageObject parameter</typeparam>
        /// <typeparam name="TValue2">Templated value type for the second BaseGameDataStorageObject parameter</typeparam>
        /// <param name="classType"></param>
        /// <param name="objectToStore"></param>
        /// <param name="key"></param>
        public void insertDataFromSerializedObject<TValue1, TValue2>(GameDataStorageLayerUtils.objectClassType classType, BaseGameDataStorageObject<TValue1,TValue2> objectToStore, string key)
        {

        }

        /// <summary>
        /// Overridden insert for mulitple BaseGameDataStorageObjects into a concurrent dictionary
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="objectsToStore"></param>
        public void insertDataFromSerializedObject<TValue1, TValue2>(List<Tuple<GameDataStorageLayerUtils.objectClassType, Tuple<string,BaseGameDataStorageObject<TValue1, TValue2>>>> objectsToStore)
        {

        }

        /// <summary>
        /// Searches through the concurrent dictionaries for the object specified and then modifies it on the fly.
        /// </summary>
        /// <param name="type">Type of dictionary we should address.</param>
        /// <param name="obj">Object data to modify</param>
        /// <returns>True for success, false on failure</returns>
        public bool modifyData(GameDataStorageLayerUtils.objectClassType type, BaseGameDataStorageObject<string, Tuple<string,int>> obj)
        {
            return true;
        }

        /// <summary>
        /// Return the interface BaseGameObject to the caller, based on the type of class we can recast it once returned.
        /// </summary>
        /// <typeparam name="TValue1">usually the string value (key)</typeparam>
        /// <typeparam name="TValue2">Tuple of string,int or string,string</typeparam>
        /// <param name="type">type of data to search for</param>
        /// <param name="key">key value to search for</param>
        /// <returns>Interface to a game object that will need to be recast to the original type.</returns>
        public BaseGameObject<TValue1,TValue2> getDataFromStorageObject<TValue1, TValue2>(GameDataStorageLayerUtils.objectClassType type, string key)
        {
            BaseGameObject<TValue1,TValue2> obj = null;
            switch(type)
            {
                case GameDataStorageLayerUtils.objectClassType.Attribute:
                    if( attributeData.ContainsKey(key))
                    {
                        return (BaseGameObject<TValue1,TValue2>)attributeData[key];
                    }
                    break;
                case GameDataStorageLayerUtils.objectClassType.Descriptor:
                    if( descriptorData.ContainsKey(key))
                    {
                        return (BaseGameObject<TValue1, TValue2>)descriptorData[key];
                    }
                    break;
                case GameDataStorageLayerUtils.objectClassType.Extra:
                    if(modifiedData.ContainsKey(key))
                    {
                        return (BaseGameObject<TValue1, TValue2>)extraData[key];
                    }
                    break;

                case GameDataStorageLayerUtils.objectClassType.Modified:
                    if(modifiedData.ContainsKey(key))
                    {
                        return (BaseGameObject<TValue1,TValue2>)modifiedData[key];
                    }
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Overridden get function which returns an interface to a dictionary to the caller.
        /// </summary>
        /// <param name="type">Type of dictionary required</param>
        /// <returns>Interface to dictionary.</returns>
        public IDictionary getDataFromStorageObject(GameDataStorageLayerUtils.objectClassType type)
        {
            switch (type)
            {
                case GameDataStorageLayerUtils.objectClassType.Attribute:
                    return attributeData;
                case GameDataStorageLayerUtils.objectClassType.Descriptor:
                    return descriptorData;
                case GameDataStorageLayerUtils.objectClassType.Extra:
                    return extraData;
                case GameDataStorageLayerUtils.objectClassType.Modified:
                    return modifiedData;
            }
            return null;
        }
    }
}
