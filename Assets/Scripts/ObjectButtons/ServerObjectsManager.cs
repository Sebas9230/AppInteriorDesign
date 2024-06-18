using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ObjectServerManager : MonoBehaviour
{
    [SerializeField] private string jsonURL = "https://projectvertexscape.onrender.com/api/categorias-objetos/";
    [SerializeField] private ItemButtonManager itemButtonManager;
    [SerializeField] private  GameObject buttonsContainer;

    private SaverManager sceneSaver;

    [Serializable]
    public struct Categoria
    {
        public string categoria;
        public string url;
        public Object[] Objetos;
    }

    [Serializable]
    public struct Object
    {
        public string nombre;
        public string objeto3d;
        public string img;
    }

    // Clase contenedora para envolver la lista
    [Serializable]
    public struct CategoriasWrapper
    {
        public List<Categoria> categorias;
    }

    public List<Categoria> categoriasCollection = new List<Categoria>();


    void Start()
    {
        sceneSaver = FindObjectOfType<SaverManager>();
        if (sceneSaver == null)
        {
            Debug.LogError("No SaverManager found in the scene!");
        }
        
        StartCoroutine(GetJsonData());
        //Creo y suscribo la función que creará los botnotes
        MySceneManager.instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        if (categoriasCollection == null || categoriasCollection.Count == 0)
        {
            Debug.LogError("Categorias collection is null or empty!");
            return;
        }

        Debug.Log("Creating buttons...");
        foreach (var categoria in categoriasCollection)
        {
            foreach (var objeto in categoria.Objetos)
            {
                ItemButtonManager itemButton = Instantiate(itemButtonManager, buttonsContainer.transform);
                itemButton.name = objeto.nombre;
                itemButton.ItemName = objeto.nombre;
                itemButton.ItemDescription = categoria.categoria; // Usamos la categoría como descripción
                itemButton.URLBundleModel = objeto.objeto3d;
                StartCoroutine(GetBundleImage(objeto.img, itemButton));
                itemButton.sceneSaver = sceneSaver;

                Debug.Log($"Button created for object: {objeto.nombre}");
            }
        }
        MySceneManager.instance.OnItemsMenu -= CreateButtons;
    }

    IEnumerator GetJsonData()
    {
        UnityWebRequest serverRequest = UnityWebRequest.Get(jsonURL);
        yield return serverRequest.SendWebRequest();
        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = serverRequest.downloadHandler.text;
            Debug.Log("Received JSON: " + jsonResponse);

            try
                {
                    // Envolver el JSON en un objeto que tenga una propiedad 'categorias'
                    string wrappedJson = "{\"categorias\":" + jsonResponse + "}";
                    CategoriasWrapper categoriasWrapper = JsonUtility.FromJson<CategoriasWrapper>(wrappedJson);
                    categoriasCollection = categoriasWrapper.categorias;
                    Debug.Log($"Number of categories: {categoriasCollection.Count}");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error deserializing JSON: " + ex.Message);
                }
        }
        else
        {
            Debug.Log("Error while obtaining data from json in server");
        }
    }

    IEnumerator GetBundleImage(string urlImage, ItemButtonManager button)
    {
        UnityWebRequest serverRequest = UnityWebRequest.Get(urlImage);
        serverRequest.downloadHandler = new DownloadHandlerTexture();
        yield return serverRequest.SendWebRequest();
        if(serverRequest.result == UnityWebRequest.Result.Success)
        {
            button.ImageBundle.texture = ((DownloadHandlerTexture)serverRequest.downloadHandler).texture;
        }else
        {
            Debug.Log("Error while asigning the images");
        }
    }
}