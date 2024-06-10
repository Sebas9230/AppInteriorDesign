using UnityEngine;
using System.IO;
using Meta.XR.MRUtilityKit;

public class SceneLoader : MonoBehaviour
{
    public string jsonFileName; //nombre del archivo JSON, sin extensión ni comillas

    //referencia a MRUK que maneja la escena
    public MRUK mruk;

    void Start()
    {
        LoadJsonScene();
    }

    public void LoadJsonScene()
    {
        //busca el archivo en la carpeta Resources
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile != null)
        {
            // Deserializar el contenido JSON y cargar la escena en MRUK
            mruk.LoadSceneFromJsonString(jsonFile.text);
            Debug.Log("Escena cargada exitosamente desde " + jsonFileName);
        }
        else
        {
            Debug.LogError("No se pudo encontrar el archivo JSON: " + jsonFileName);
        }
    }
}
