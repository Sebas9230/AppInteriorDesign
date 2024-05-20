using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButton itemButton;

    void Start()
    {
        MenuManager.instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButton itemButtonManager;

            itemButtonManager = Instantiate(itemButton, buttonContainer.transform);
            itemButtonManager.ItemName = item.ItemName;
            itemButtonManager.ItemDescription = item.ItemDescription;
            itemButtonManager.ItemImage = item.ItemImage;
            itemButtonManager.Item3DModel = item.Item3DModel;
            itemButtonManager.name = item.ItemName;
        }

        MenuManager.instance.OnItemsMenu -= CreateButtons;
    }
}
