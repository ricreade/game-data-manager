using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameDataStorageLayer
{
    [Serializable()]
    public class GameDataStorageObject : BaseGameDataStorageLayer
    {
        private ConcurrentDictionary<string, BaseObject> characterData;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serializedGameData"></param>
        public GameDataStorageObject(byte[] serializedGameData)
        {
            characterData = new ConcurrentDictionary<string, BaseObject>();

            try
            {
                
                string data = System.Text.Encoding.UTF8.GetString(serializedGameData);
                BaseGameDataStorageObject<string, Tuple<string, string>> descriptors = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                BaseGameDataStorageObject<string, Tuple<string, string>> modifiedData = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                BaseGameDataStorageObject<string, Tuple<string, string>> extraData = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                BaseGameDataStorageObject<string, Tuple<string, int>> attributes = new BaseGameDataStorageObject<string, Tuple<string, int>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                BaseGameDataStorageObject<string, Tuple<string, string>> inventoryData = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);

                string mydata = "";
                XDocument xDoc = XDocument.Load(new StringReader(data));


                var nameList = from el in xDoc.Root.Descendants("character").Elements("name")
                            select el;


                var dList = from el in xDoc.Root.Descendants().Elements("attributes").Elements("attribute")
                            where ((string)el.Element("type") != "racial")
                            select new
                            {
                                name = (string)el.Element("name"),
                                bonus = (string)el.Element("bonus"),
                                type = (string)el.Element("type")
                            };

                string attPathName = nameList.First().Value + ":attribute";
                
                foreach( var att in dList )
                {
                    Tuple<string, Tuple<string, int>> tData = new Tuple<string, Tuple<string, int>>(attPathName + ":" + att.type, new Tuple<string, int>(attPathName + ":" + att.name + ":" + att.type, Convert.ToInt32(att.bonus)));
                    attributes.addTupleToList(tData);
                }




                Console.WriteLine("Hooray!");
                

            } catch(Exception ex)
            {
                string err = ex.Message;
                Console.WriteLine(err);
            }
            //BaseGameDataStorageObject<string, Tuple<string,int>> x = new BaseGameDataStorageObject<string, Tuple<string,int>>(GameDataStorageLayerUtils.objectClassType.Attribute);
            //Tuple<string, Tuple<string, int>> theData = new Tuple<string, Tuple<string, int>>("test1", new Tuple<string, int>("strength", 25));
            //x.insertTupleAt(theData, 0, false);
            //characterData.TryAdd("test", (BaseObject)x);
            //Tuple<string, Tuple<string, string>> theData2 = new Tuple<string, Tuple<string, string>>("test1", new Tuple<string, string>("extra strength", "A lot of strength"));
            //y.insertTupleAt(theData2, 0, false);
            //characterData.TryAdd("testng", (BaseObject)y);
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
                    if( characterData.ContainsKey(key))
                    {
                        return (BaseGameObject<TValue1,TValue2>)characterData[key];
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
            return characterData;
        }
    }
}
