using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomLoader : MonoBehaviour
{
    public MRUK mruk;
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private void Start()
    {
        LoadJsonScene();
        LoadObjectsFromJsonData();
    }

    // Load the scene from content stored in ProjectData.CurrentHabitacionJson
    public void LoadJsonScene()
    {
        if (string.IsNullOrEmpty(ProjectData.CurrentHabitacionJson))
        {
            Debug.LogError("El contenido JSON de CurrentHabitacionJson está vacío");
            return;
        }

        // Comprobar si es una URL
        if (Uri.IsWellFormedUriString(ProjectData.CurrentHabitacionJson, UriKind.Absolute))
        {
            StartCoroutine(DownloadAndProcessJson(ProjectData.CurrentHabitacionJson, ProcessSceneJson));
        }
        else
        {
            // Procesar el JSON directamente si no es una URL
            ProcessSceneJson(ProjectData.CurrentHabitacionJson);
        }
    }

    // Load the objects from content stored in ProjectData.CurrentObjetoJson
    public void LoadObjectsFromJsonData()
    {
        if (!string.IsNullOrEmpty(ProjectData.CurrentObjetoJson))
        {
            // Comprobar si es una URL
            if (Uri.IsWellFormedUriString(ProjectData.CurrentObjetoJson, UriKind.Absolute))
            {
                StartCoroutine(DownloadAndProcessJson(ProjectData.CurrentObjetoJson, ProcessObjectsJson));
            }
            else
            {
                // Procesar el JSON directamente si no es una URL
                ProcessObjectsJson(ProjectData.CurrentObjetoJson);
            }
        }
        else
        {
            Debug.LogError("No object data found in CurrentObjetoJson.");
        }
    }

    private IEnumerator DownloadAndProcessJson(string url, Action<string> processJsonCallback)
    {
        Debug.Log("Downloading JSON from URL: " + url);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading JSON: " + request.error);
            }
            else
            {
                string jsonText = request.downloadHandler.text;
                processJsonCallback(jsonText);
            }
        }
    }

    // Procesar JSON de la escena
    private void ProcessSceneJson(string jsonText)
    {
        Debug.Log("Processing scene JSON");
        if (!string.IsNullOrEmpty(jsonText))
        {
            mruk.LoadSceneFromJsonString(jsonText);
            Debug.Log("Escena cargada exitosamente desde el JSON proporcionado.");
        }
        else
        {
            Debug.LogError("El contenido JSON está vacío o no se pudo leer correctamente.");
        }
    }

    // Procesar JSON de los objetos
    private void ProcessObjectsJson(string jsonText)
    {
        Debug.Log("Processing objects JSON");
        try
        {
            SerializationHelper<ObjectData> helper = JsonUtility.FromJson<SerializationHelper<ObjectData>>(jsonText);
            List<ObjectData> objectsData = helper.data;

            foreach (ObjectData data in objectsData)
            {
                GameObject obj = InstantiateObjectFromData(data);
                instantiatedObjects.Add(obj);
            }
        }
        catch (ArgumentException e)
        {
            Debug.LogError("JSON parse error: " + e.Message);
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