using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ScriptingEngineTests
{
    public class PrototypeDataObject
    {
        List<Tuple<string, string>> _values;
        public PrototypeDataObject(_Worksheet sheet)
        {
            _values = new List<Tuple<string, string>>();
            populateValues(sheet);
        }

        private void populateValues(_Worksheet sheet)
        {
            int row = 2;
            bool endofdata = false;
            string key;
            string val;
            Tuple<string, string> tuple;
            
            while (!endofdata)
            {
                key = sheet.Range[row, 1].Value2.ToString();
                if (key.Equals(""))
                {
                    endofdata = true;
                }
                else
                {
                    val = sheet.Range[row, 2].Value2.ToString();
                    tuple = new Tuple<string, string>(key, val);
                    _values.Add(tuple);
                }
            }
        }

        public string getValue(string key)
        {
            foreach (Tuple<string, string> tuple in _values) 
            {
                if (tuple.Item1.Equals(key))
                    return tuple.Item2.ToString();
            }
            return null;
        }
    }
}
