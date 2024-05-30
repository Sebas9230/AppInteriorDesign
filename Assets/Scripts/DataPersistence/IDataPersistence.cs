using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(ProjectData data); //reading data
    void SaveData(ref ProjectData data); //'ref' for allowing to modify data
}
