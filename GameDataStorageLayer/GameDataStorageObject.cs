using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace GameDataStorageLayer
{
    [Serializable()]
    public class GameDataStorageObject : BaseGameDataStorageLayer
    {
        private ConcurrentDictionary<string, BaseGameObject> attributeData;
        private ConcurrentDictionary<string, BaseGameObject> descriptorData;
        private ConcurrentDictionary<string, BaseGameObject> modifiedData;
        private ConcurrentDictionary<string, BaseGameObject> extraData;
        private int totalByteLength;

        public GameDataStorageObject(byte[] serializedGameData)
        {
            attributeData = new ConcurrentDictionary<string, BaseGameObject>();
            descriptorData = new ConcurrentDictionary<string, BaseGameObject>();
            modifiedData = new ConcurrentDictionary<string, BaseGameObject>();
            extraData = new ConcurrentDictionary<string, BaseGameObject>();
            this.totalByteLength = 0;
        }

        public int getByteLengt()
        {
            return this.totalByteLength;
        }

        public bool modifyAttributeData(BaseGameObject obj)
        {
            return true;
        }




    }
}
