/*************************************************************************** 
*   Properties for the GameObject with the 3DModel
*   I'll save each of them in dictionary <id, position>
****************************************************************************/
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectData : MonoBehaviour, IDataPersistence
{
    public GameObject modelPrefab;
    public string id;

    private void Awake()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = UniqueIDGenerator.GenerateID();
        }
    }
    public void LoadData(ProjectData data)
    {
        //ubicar los objetos según las posiciones guardadas en el archivo json
        if (data.objectsPlaced.TryGetValue(id, out Vector3 position))
        {
            if (modelPrefab == null)
            {
                modelPrefab = gameObject;
            }
            modelPrefab.transform.position = position;
            Debug.Log("Cargando datos para ID: " + id + " en posición: " + position);
        }
    }

    public void SaveData(ref ProjectData data) //send to ProjectData
    {
        if (modelPrefab != null)
        {
            if (data.objectsPlaced.ContainsKey(id))
            {
                data.objectsPlaced[id] = modelPrefab.transform.position;
            }
            else
            {
                data.objectsPlaced.Add(id, modelPrefab.transform.position);
            }
            Debug.Log("Guardando datos para ID: " + id + " en posición: " + modelPrefab.transform.position);
        }
    }
}
