using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    /// <summary>
    /// Interface class for GameDataStorageLayer this will be the main entry point for accessing and setting values for the storage.
    /// </summary>
    public interface IGameDataStorageLayer
    {
        Tuple<string, Tuple<string, string>> getValue(string uuid, string key);
        string setValue(string key, string value, string guid = "");
        Tuple<string, Tuple<string, string>> search(string key);
    }
}
