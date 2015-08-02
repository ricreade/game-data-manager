using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ScriptingEngineTests
{
    /// <summary>
    /// Represents a concrete collection of data objects that represent discrete game entities.
    /// </summary>
    public class PrototypeDataLayer
    {
        private ConcurrentDictionary<string, PrototypeDataObject> _dict;
        
        /// <summary>
        /// Instantiates a new data object collection using all data files stored in the specified 
        /// directory.
        /// </summary>
        /// <param name="dataDir">The directory containing the data files.</param>
        public PrototypeDataLayer(DirectoryInfo dataDir)
        {
            _dict = new ConcurrentDictionary<string, PrototypeDataObject>();
            populateDictionary(dataDir);
        }

        /// <summary>
        /// Iterates through all files in the specified data directory and constructs data objects
        /// from each file.  The data objects are then added to the internal dictionary.
        /// </summary>
        /// <param name="dataDir">The directory containing the data files.</param>
        private void populateDictionary(DirectoryInfo dataDir)
        {
            PrototypeDataObject dataobj;
            string id;

            if (dataDir == null || !dataDir.Exists){
                throw new ArgumentException("A data source reference was not provided.");
            }

            foreach (FileInfo file in dataDir.GetFiles())
            {
                dataobj = new PrototypeDataObject(file);
                id = dataobj.getValue("id");
                if (id != null && id.Length > 0)
                {
                    _dict.TryAdd(id, dataobj);
                }
            }
        }

        /// <summary>
        /// Getter method for the internal dictionary.
        /// </summary>
        public ConcurrentDictionary<string, PrototypeDataObject> Library
        {
            get { return _dict; }
        }
    }
}
