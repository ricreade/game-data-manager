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
    public class PrototypeDataLayer
    {
        private ConcurrentDictionary<string, PrototypeDataObject> _dict;
        //private const string _INPUT = @"E:\D&D Programs\Development Notes\diagrams\samp data\sample tuples.xlsm";

        public PrototypeDataLayer(DirectoryInfo dataDir)
        {
            _dict = new ConcurrentDictionary<string, PrototypeDataObject>();
            populateDictionary(dataDir);
        }

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

        public ConcurrentDictionary<string, PrototypeDataObject> Library
        {
            get { return _dict; }
        }
    }
}
