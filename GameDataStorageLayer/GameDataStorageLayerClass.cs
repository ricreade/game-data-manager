using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    /// <summary>
    /// Concrete implementation for the game data storage layer interface.
    /// </summary>
    public class GameDataStorageLayerClass : IGameDataStorageLayer
    {
        private List<Tuple<string, Tuple<string, string>>> dataStorageList;
        public GameDataStorageLayerClass()
        {
            dataStorageList = new List<Tuple<string, Tuple<string, string>>>();
            fillList();
        }


        /// <summary>
        /// Convenience function to fill a list with initial data values.
        /// For convenience's sake, the guid will be the index, we can always generate a guid with System.Guid.NewGuid().ToString()
        /// </summary>
        private void fillList()
        {
            Random r = new Random();
            List<string> randomPhraseList = new List<string>()
            {
                "Strength bonus", "Grobo Gnomehands", "Demon", "Angel", "Fireball", "Timestop", "Dualwield", "Constitution Bonus", "Wisdom Bonus",
                "Wisdom", "Strength", "Intelligence", "Charisma", "Dexterity", "Constitution", "Reflex Save", "Will save", "Reflex Save", "Fortitude Save",
                "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"
            };
            for(int i = 0; i < 1000; i++)
            {
                string newguid = i.ToString();
                int firstIndex = r.Next(0, randomPhraseList.Count);
                int secondIndex = r.Next(0, randomPhraseList.Count);
                Tuple<string, Tuple<string,string>> testData = new Tuple<string, Tuple<string, string>>(newguid, new Tuple<string,string>(randomPhraseList[firstIndex], randomPhraseList[secondIndex]));
                dataStorageList.Add(testData);
            }
        }


        public void printList()
        {
            foreach(Tuple<string,Tuple<string,string>> key in dataStorageList)
            {
                string theData = String.Format("{0}; {1}; {2}", key.Item1, key.Item2.Item1, key.Item2.Item2);
                Console.WriteLine("{0}; {1}; {2}", key.Item1, key.Item2.Item1, key.Item2.Item2);
            }

        }

        public int listLength()
        {
            return dataStorageList.Count;
        }

        /// <summary>
        /// Gets a value from the underlying storage layer and returns that Tuple back to the caller.
        /// </summary>
        /// <param name="uuid">UUID for the particular value</param>
        /// <param name="key">Key to find a particular value</param>
        /// <returns>The tuple that matched the query or nothing if there were no matches</returns>
        public Tuple<string, Tuple<string,string>> getValue(string uuid, string key)
        {
            if(String.IsNullOrEmpty(uuid) && String.IsNullOrEmpty(key))
            {
                return null;
            }
            
            foreach(Tuple<string, Tuple<string,string>> dataTuple in dataStorageList)
            {
                if(dataTuple.Item1 == uuid)
                {
                    return dataTuple;
                }
                if(String.IsNullOrEmpty(uuid))
                {
                    if(dataTuple.Item2.Item1 == key)
                    {
                        return dataTuple;
                    }
                }
            }
            return null;
        }

       /// <summary>
       /// Store a new value in the storage layer.  If a guid is specified return that if successful, 
       /// otherwise return a randomly generated guid or nothing in case of failure
       /// </summary>
       /// <param name="key">Key that belongs to the data</param>
       /// <param name="value">Value we wish to store</param>
       /// <param name="theguid">optional guid that we wish to store the values with</param>
       /// <returns>returns the guid that was set, or the new guid, or empty string if it failed</returns>
        public string setValue(string key, string value, string theguid = "")
        {
            string guid = theguid;
            if(String.IsNullOrEmpty(theguid))
            {
                int guidValue = dataStorageList.Count + 1;
                guid = guidValue.ToString();
            }
            Tuple<string, Tuple<string,string>> newData = new Tuple<string, Tuple<string,string>>(theguid, new Tuple<string,string>(key, value));
            try
            {
                dataStorageList.Add(newData);
            } catch(Exception ex)
            {
                Console.WriteLine("Something bad happened! {0}", ex.Message);
            }
            return guid; 
        }

        /// <summary>
        /// Simple function to search the tuple
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns the matched data if found, otherwise null</returns>
        public Tuple<string, Tuple<string, string>> search(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return null;
            }
            foreach (Tuple<string, Tuple<string, string>> dataTuple in dataStorageList)
            {           
                if (dataTuple.Item2.Item1 == key)
                {
                    return dataTuple;
                }
            }
            return null;
        }


    }
}
