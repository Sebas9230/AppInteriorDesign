using System.Collections.Generic;

public class SerializationHelper<T>
{
    public List<T> data;

    public SerializationHelper(List<T> dataList)
    {
        data = dataList;
    }
}
