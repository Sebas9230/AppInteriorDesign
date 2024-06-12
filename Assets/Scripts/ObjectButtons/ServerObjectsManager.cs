using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class ObjectServerManager : MonoBehaviour
{
    [SerializeField] private string jsonURL;
    [SerializeField] private ItemButtonManager itemButtonManager;
    [SerializeField] private  GameObject buttonsContainer;

    private SaverManager sceneSaver;

    [Serializable]
    public struct Items
    {
        [Serializable]
        public struct Item
        {
            public string Name;
            public string Description;
            public string Category;
            public string URLBundleModel;
            public string URLImageModel;
        }
        public Item[] items;
    }

    public Items newItemsCollection = new Items();

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
        foreach (var item in newItemsCollection.items)
        {
            ItemButtonManager itemButton;
            itemButton = Instantiate(itemButtonManager, buttonsContainer.transform);
            itemButton.name = item.Name;
            itemButton.ItemName = item.Name;
            itemButton.ItemDescription = item.Description;
            itemButton.URLBundleModel = item.URLBundleModel;
            StartCoroutine(GetBundleImage(item.URLImageModel, itemButton));
            itemButton.sceneSaver = sceneSaver;
            
        }
        //desuscribo, para que los botones se creen solo una vez
        MySceneManager.instance.OnItemsMenu -= CreateButtons;
    }

    IEnumerator GetJsonData()
    {
        UnityWebRequest serverRequest = UnityWebRequest.Get(jsonURL);
        yield return serverRequest.SendWebRequest();
        if(serverRequest.result == UnityWebRequest.Result.Success)
        {
            newItemsCollection = JsonUtility.FromJson<Items>(serverRequest.downloadHandler.text);
        }else
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