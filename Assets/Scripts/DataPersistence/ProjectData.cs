using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectData
{
    //what I wanna save
    //public int nObjects;
    public SerializableDictionary<string, Vector3> objectsPlaced;

    
    public ProjectData(){
        //this.nObjects = 0;
        objectsPlaced = new SerializableDictionary<string, Vector3>();
    }
}
