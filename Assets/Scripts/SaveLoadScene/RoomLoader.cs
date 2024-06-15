using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.Networking;
using System.Collections;

public class RoomLoader : MonoBehaviour
{
    public MRUK mruk;

    private void Start()
    {
        LoadJsonScene();
    }

    //Load the scene from json url in api
    public void LoadJsonScene()
    {
        string url = ProjectData.CurrentProjectJsonUrl;
        Debug.Log("La ural guardada es: " + url);
        
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL del archivo JSON no está definida.");
            return;
        }

        //StartCoroutine(LoadJsonFromUrl(url)); //para render
        StartCoroutine(LoadJsonFromUrl("http://127.0.0.1:8000/media/projects/roomTest_TRCPc4L.json"));//probar localmente

    }

    //petición url para acceder al archivo json
    private IEnumerator LoadJsonFromUrl(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al cargar el archivo JSON: " + request.error);
                yield break;
            }

            string jsonText = request.downloadHandler.text;

            if (!string.IsNullOrEmpty(jsonText))
            {
                mruk.LoadSceneFromJsonString(jsonText);
                Debug.Log("Escena cargada exitosamente desde " + url);
            }
            else
            {
                Debug.LogError("El archivo JSON está vacío o no se pudo leer correctamente.");
            }
        }
    }
}
