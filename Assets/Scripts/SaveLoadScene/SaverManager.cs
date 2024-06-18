using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SaverManager : MonoBehaviour
{
    // Lista para almacenar los objetos instanciados
    private List<GameObject> instantiatedObjects = new List<GameObject>();
    public MRUK mruk;
    public ApiManager apiManager;

    public void RegisterInstantiatedObject(GameObject obj)
    {
        instantiatedObjects.Add(obj);
    }

    public List<ObjectData> GatherObjectsData()
    {
        List<ObjectData> objectsData = new List<ObjectData>();

        foreach (GameObject obj in instantiatedObjects)
        {
            ObjectData data = new ObjectData(
                obj.name,
                obj.transform.position,
                obj.transform.rotation,
                obj.transform.localScale
            );
            objectsData.Add(data);
        }

        return objectsData;
    }

    public string ConvertToJson(List<ObjectData> dataList)
    {
        return JsonUtility.ToJson(new SerializationHelper<ObjectData>(dataList));
    }

    //Guarda datos en la db
    public void OnSaveButtonClick()
    {
        List<ObjectData> objectsData = GatherObjectsData();
        string json = ConvertToJson(objectsData);
        ProjectData.CurrentObjetoJson = json;

        SaveCurrentSceneToJson();
        
        // Llamar al POST de los datos de unityproyect
        apiManager.StartCoroutine(apiManager.PostSceneData());

        StartCoroutine(apiManager.GetDataFromApi(ProjectData.Token));
    }

    //RoomSaver - guarda la habitación en formato json en la variable estática
    public void SaveCurrentSceneToJson()
    {
        if (mruk == null)
        {
            Debug.LogError("MRUK script reference is missing.");
            return;
        }

        ProjectData.CurrentHabitacionJson = mruk.SaveSceneToJsonString(SerializationHelpers.CoordinateSystem.Unity);
    }
}
