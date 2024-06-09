using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string prefabName; //nombre original del prefab
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public ObjectData(string name, Vector3 pos, Quaternion rot, Vector3 scl)
    {
        prefabName = name;
        position = pos;
        rotation = rot;
        scale = scl;
    }
}
