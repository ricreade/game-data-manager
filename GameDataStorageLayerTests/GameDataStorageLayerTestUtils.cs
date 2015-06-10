using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataStorageLayer;

namespace GameDataStorageLayerTests
{
    public class GameDataStorageLayerTestUtils
    {

        public static ConcurrentDictionary<string, BaseGameDataStorageObject<string,Tuple<string,int>>> fillAttributeObjectWithData()
        {
            ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, int>>> dataDict = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, int>>>();
            string[] labels = new string[6]{"Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma"};
            int i = 10;
            string path = "testChar/attributes";
            BaseGameDataStorageObject<string, Tuple<string, int>> tObject = new BaseGameDataStorageObject<string, Tuple<string, int>>(GameDataStorageLayerUtils.objectClassType.Attribute);
            foreach( var label in labels)
            { 
                Tuple<string, Tuple<string,int>> testData = new Tuple<string,Tuple<string,int>>("testChar" + ":attributes:" + label, new Tuple<string,int>(path+"/"+label, i));
                tObject.addTupleToList(testData);
                
                i++;
            }
            dataDict.TryAdd("testChar:attributes", tObject);
            return dataDict;
        }

        /// <summary>
        /// Most other data objects are defined using string,Tuple<string,string> instead of string, Tuple<string,int>
        /// We can leverage filling out the rest by implementing this function and passing a type param
        /// </summary>
        /// <returns>Returns a concurrent dict with the filled out data</returns>
        public static ConcurrentDictionary<string, BaseGameDataStorageObject<string,Tuple<string,string>>> fillOtherDataObjectsWithData(GameDataStorageLayerUtils.objectClassType typeOfObjectToFill)
        {
            ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>> dataDict = new ConcurrentDictionary<string, BaseGameDataStorageObject<string, Tuple<string, string>>>();
            string path = "";
            Tuple<string, Tuple<string, string>> testData = null;
            Tuple<string, Tuple<string, string>> testData1 = null;
            Tuple<string, Tuple<string, string>> testData2 = null;
            Tuple<string, Tuple<string, string>> testData3 = null;
            BaseGameDataStorageObject<string, Tuple<string, string>> tObject = null;
            switch(typeOfObjectToFill)
            {
                case GameDataStorageLayerUtils.objectClassType.Descriptor:
                    string[] descriptorLabels = new string[4] { "Angel", "Ghostly", "Demon", "Monster" };
                    path = "testChar/descriptorData";
                    testData = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Angel", new Tuple<string, string>(path + "/" + descriptorLabels[0], "Angelic Being"));
                    testData1 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Ghostly", new Tuple<string, string>(path + "/" + descriptorLabels[1], "Ghostly Being"));
                    testData2 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Demon", new Tuple<string, string>(path + "/" + descriptorLabels[2], "Demonic Being"));
                    testData3 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Monster", new Tuple<string, string>(path + "/" + descriptorLabels[3], "Monster HD:24"));
                    tObject = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                    break;

                case GameDataStorageLayerUtils.objectClassType.Extra:
                    string[] extraLabels = new string[4] { "Note", "Always Fail", "Secret Note", "Extra Item" };
                    path = "testChar/extraData";
                    testData = new Tuple<string, Tuple<string, string>>("testChar:extraData:Note", new Tuple<string, string>(path + "/" + extraLabels[0], "Note: this character has a note!"));
                    testData1 = new Tuple<string, Tuple<string, string>>("testChar:extraData:Always Fail", new Tuple<string, string>(path + "/" + extraLabels[1], "Always fail a critical hit."));
                    testData2 = new Tuple<string, Tuple<string, string>>("testChar:extraData:Secret Note", new Tuple<string, string>(path + "/" + extraLabels[2], "Secret note: this character has a secret."));
                    testData3 = new Tuple<string, Tuple<string, string>>("testChar:extraData:Extra Item", new Tuple<string, string>(path + "/" + extraLabels[3], "Extra item: this character always gets two potions."));
                    tObject = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Extra);
                    break;

                case GameDataStorageLayerUtils.objectClassType.Modified:
                    string[] modifiedLabels = new string[4] { "Extra Strength", "Haste", "Flying", "Dancing" };
                    path = "testChar/modifiedData";
                    testData = new Tuple<string, Tuple<string, string>>("testChar:modifiedData:Extra Strength", new Tuple<string, string>(path + "/" + modifiedLabels[0], "This creature has extra stength of 8."));
                    testData1 = new Tuple<string, Tuple<string, string>>("testChar:modifiedData:Haste", new Tuple<string, string>(path + "/" + modifiedLabels[1], "This creature is hasted."));
                    testData2 = new Tuple<string, Tuple<string, string>>("testChar:modifiedData:Flying", new Tuple<string, string>(path + "/" + modifiedLabels[2], "This creature is flying."));
                    testData3 = new Tuple<string, Tuple<string, string>>("testChar:modifiedData:Dancing", new Tuple<string, string>(path + "/" + modifiedLabels[3], "This creature is dancing."));
                    tObject = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Modified);
                    break;

                default:
                    descriptorLabels = new string[4] { "Angel", "Ghostly", "Demon", "Monster" };
                    path = "testChar/descriptorData";
                    testData = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Angel", new Tuple<string, string>(path + "/" + descriptorLabels[0], "Angelic Being"));
                    testData1 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Ghostly", new Tuple<string, string>(path + "/" + descriptorLabels[0], "Ghostly Being"));
                    testData2 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Demon", new Tuple<string, string>(path + "/" + descriptorLabels[0], "Demonic Being"));
                    testData3 = new Tuple<string, Tuple<string, string>>("testChar:descriptorData:Monster", new Tuple<string, string>(path + "/" + descriptorLabels[0], "Monster HD:24"));
                    tObject = new BaseGameDataStorageObject<string, Tuple<string, string>>(GameDataStorageLayerUtils.objectClassType.Descriptor);
                    break;
            }

            tObject.addTupleToList(testData);
            tObject.addTupleToList(testData1);
            tObject.addTupleToList(testData2);
            tObject.addTupleToList(testData3);
            string tPath = path.Replace("/", ":");
            dataDict.TryAdd(tPath, tObject);
            return dataDict;
        }
    }
}
