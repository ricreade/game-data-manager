using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataStorageLayer
{
    public interface BaseGameObject<DataType1, DataType2> : BaseObject
    {
        //string getClassType();
        int getListSize();
        bool addTupleToList(Tuple<DataType1, DataType2> insertedData);
        Tuple<DataType1, DataType2> getValueAt(int index);
        bool insertTupleAt(Tuple<DataType1, DataType2> tupleData, int index, bool replace = true);
        List<Tuple<DataType1, DataType2>> getList();
        Tuple<DataType1, DataType2> removeItemAt(int index);
        bool removeAllElements();
    }
}
