/**************************************************************************** 
* Allow saving Dictionaries in Json file
***************************************************************************/
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    //save the dictionary to list
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);      
            values.Add(pair.Value);      
        }
    }

    //load the dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();
        
        if(keys.Count != values.Count)
        {
            Debug.LogError("Al intentar deserealizar un diccionario, se detectó que la cantidad de keys ("
            + keys.Count + ") no concuerda con el número de values (" + values.Count
            + ") que indica que algo anda mal");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this[keys[i]] = values[i];//this.Add(keys[i], values[i]);
        }
    }
}
