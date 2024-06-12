using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaverManager : MonoBehaviour
{
    // Lista para almacenar los objetos instanciados
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    public void RegisterInstantiatedObject(GameObject obj)
    {
        instantiatedObjects.Add(obj);
    }

    public List<ObjectData> GatherSceneData()
    {
        List<ObjectData> sceneData = new List<ObjectData>();

        foreach (GameObject obj in instantiatedObjects)
        {
            ObjectData data = new ObjectData(
                obj.name,
                obj.transform.position,
                obj.transform.rotation,
                obj.transform.localScale
            );
            sceneData.Add(data);
        }

        return sceneData;
    }

    public string ConvertToJson(List<ObjectData> dataList)
    {
        return JsonUtility.ToJson(new SerializationHelper<ObjectData>(dataList));
    }

    public void SaveJsonToFile(string json, string filePath)
    {
        File.WriteAllText(filePath, json);
    }

    public void SaveScene(string filePath)
    {
        List<ObjectData> sceneData = GatherSceneData();
        string json = ConvertToJson(sceneData);
        SaveJsonToFile(json, filePath);
    }

    public void OnSaveButtonClick()
    {
        string filePath = Application.persistentDataPath + "/objectsData.json";
        Debug.Log(filePath);
        SaveScene(filePath);
    }

/*  Load the scene */
    /* public void OnLoadButtonClick()
    {
        string filePath = Application.persistentDataPath + "/objectsData.json";
        LoadObjectsFromFile(filePath);
    } */

    public void LoadObjectsFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SerializationHelper<ObjectData> helper = JsonUtility.FromJson<SerializationHelper<ObjectData>>(json);
            List<ObjectData> objectsData = helper.data;

            foreach (ObjectData data in objectsData)
            {
                // Assuming you have a method to instantiate objects based on their type
                GameObject obj = InstantiateObjectFromData(data);
                instantiatedObjects.Add(obj);
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    private GameObject InstantiateObjectFromData(ObjectData data)
    {
        GameObject objPrefabname = Resources.Load<GameObject>(data.prefabName);
        if (objPrefabname != null)
        {
            GameObject obj = Instantiate(objPrefabname, data.position, data.rotation);
            obj.transform.localScale = data.scale;
            return obj;
        }
        else
        {
            Debug.LogError("Prefab not found for type: " + data.prefabName);
            return null;
        }
    }
}
