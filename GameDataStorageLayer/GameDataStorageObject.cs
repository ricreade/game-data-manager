using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    public class GameDataStorageObject
    {
        private Hashtable attributeData;
        private Hashtable descriptorData;
        private Hashtable modifiedData;
        private Hashtable extraData;

        public GameDataStorageObject(byte[] serializedGameData)
        {
            attributeData = new Hashtable();
            descriptorData = new Hashtable();
            modifiedData = new Hashtable();
            extraData = new Hashtable();
        }

        public bool modifyAttributeData(BaseGameObject obj)
        {
            return true;
        }
    }
}
