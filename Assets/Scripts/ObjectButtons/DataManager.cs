using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonManager itemButtonManager; //Script que ajusta la info de los botones

    void Start()
    {
        MySceneManager.instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButtonManager itemButton;

            itemButton = Instantiate(itemButtonManager, buttonContainer.transform);
            
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;
            itemButton.name = item.ItemName; //nombre del botón
            //Debug.Log(itemButton.name);
        }
        //para generarlos solo una vez y no cada vez que se genere el evento
        MySceneManager.instance.OnItemsMenu -= CreateButtons;
    }
}
