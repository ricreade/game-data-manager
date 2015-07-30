using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ScriptingEngineTests
{
    public class PrototypeDataLayer
    {
        private ConcurrentDictionary<string, PrototypeDataObject> _dict;
        private const string _INPUT = @"E:\D&D Programs\Development Notes\diagrams\samp data\sample tuples.xlsm";

        public PrototypeDataLayer()
        {
            _dict = new ConcurrentDictionary<string, PrototypeDataObject>();
            populateDictionary();
        }

        private void populateDictionary()
        {
            PrototypeDataObject dataobj;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wkbk = xlApp.Workbooks.Open(_INPUT);
            foreach (Excel.Worksheet wkst in wkbk.Worksheets)
            {
                dataobj = new PrototypeDataObject(wkst);
                string id = dataobj.getValue("id");
                if (id != null)
                {
                    _dict.TryAdd(id, dataobj);
                }
            }

        }
    }
}
