using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeDataImpl
{
    /// <summary>
    /// A simple prototype data object composed of a list of tuples.  Records stored in this data object
    /// is not guaranteed to appear in any order and keys found in this data object are not guaranteed to
    /// be unique.  This object is intended to be very lightweight and as such does not optimize reads
    /// or mutations.
    /// </summary>
    public class PrototypeDataObject
    {
        List<Tuple<string, string>> _values;
 
        public PrototypeDataObject(FileInfo dataSource)
        {
            _values = new List<Tuple<string, string>>();
            populateValues(dataSource);
        }

        /// <summary>
        /// Reads in the data store din the specified data source file and uses it to construct tuples to be
        /// added to the internal list.
        /// </summary>
        /// <param name="dataSource">The source data file.</param>
        private void populateValues(FileInfo dataSource)
        {
            try
            {
                using (StreamReader reader = new StreamReader(dataSource.OpenRead()))
                {
                    string line;
                    Tuple<string, string> tuple;
                    while (!reader.EndOfStream && !(line = reader.ReadLine()).Equals(""))
                    {
                        if (line.Length <= 12)
                        {
                            continue;
                        }
                        else
                        {
                            tuple = new Tuple<string, string>(line.Substring(0, 12).Trim(), line.Substring(12).Trim());
                            _values.Add(tuple);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the first value in the list that matches the specified key.  If multiple entries in the list
        /// have the same key, use the getAllValues method instead.
        /// </summary>
        /// <param name="key">The key to search against</param>
        /// <returns>The first string value associated with the specified key.</returns>
        public string getValue(string key)
        {
            foreach (Tuple<string, string> tuple in _values) 
            {
                if (tuple.Item1.Equals(key))
                    return tuple.Item2.ToString();
            }
            return null;
        }

        /// <summary>
        /// Returns an array of values in the list that match the specified key.  Values returns are not
        /// guaranteed to appear in any order.
        /// </summary>
        /// <param name="key">The key to search against.</param>
        /// <returns>A string array of values associated with the specified key.</returns>
        public string[] getAllValues(string key)
        {
            string[] vals = new string[_values.Count];
            int i = 0;
            foreach (Tuple<string, string> tuple in _values)
            {
                if (tuple.Item1.Equals(key))
                {
                    vals[i++] = tuple.Item2;
                }
            }
            string[] results = new string[i];
            for (int j = 0; j < i; j++)
            {
                results[j] = vals[j];
            }
            return results;
        }
    }
}
