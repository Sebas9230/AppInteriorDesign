using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuItems;
    [SerializeField] private GameObject itemsMenuOpened;
    [SerializeField] private GameObject arPositionMenu;
    [SerializeField] private Button BtnShowItems;
    [SerializeField] private Button BtnItemsOpened;
    [SerializeField] private Button BtnAprove;
    [SerializeField] private Button BtnBack;
    // Start is called before the first frame update
    void Start()
    {
        BtnShowItems.onClick.AddListener(OnBtnshowItemsClicked);
        BtnItemsOpened.onClick.AddListener(OnBtnItemsOpenedClicked);
        BtnAprove.onClick.AddListener(OnBtnsARPositionClicked);
        BtnBack.onClick.AddListener(OnBtnsARPositionClicked);
    }

    private void OnBtnshowItemsClicked()
    {
        mainMenuItems.SetActive(false);
        itemsMenuOpened.SetActive(true);
    }
    private void OnBtnItemsOpenedClicked()
    {
        mainMenuItems.SetActive(true);
        itemsMenuOpened.SetActive(false);
    }
    private void OnBtnsARPositionClicked()
    {
        itemsMenuOpened.SetActive(true);
        arPositionMenu.SetActive(false);
    }

    public void GoToARPositionMenu(){
        arPositionMenu.SetActive(true);
        itemsMenuOpened.SetActive(false);
    }

    // Start is called before the first frame update
    /* void Start()
    {
        MySceneManager.instance.OnMainMenu += ActivateMainMenu;
        MySceneManager.instance.OnItemsMenu += ActivateItemsMenu;
        MySceneManager.instance.OnARPosition += ActivateARPosition;
    }

    private void ActivateMainMenu(){
        mainMenuItems.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);//3 segundos

        itemsMenuOpened.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.5f);
        itemsMenuOpened.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        itemsMenuOpened.transform.GetChild(3).transform.DOMoveY(180, 0.3f);

        arPositionMenu.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        arPositionMenu.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
    }
    private void ActivateItemsMenu(){
        mainMenuItems.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);

        itemsMenuOpened.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.5f);
        itemsMenuOpened.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0.3f);
        itemsMenuOpened.transform.GetChild(3).transform.DOMoveY(230, 0.3f);
    }
    private void ActivateARPosition(){
        mainMenuItems.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);

        itemsMenuOpened.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.5f);
        itemsMenuOpened.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        itemsMenuOpened.transform.GetChild(3).transform.DOMoveY(100, 0.3f);

        arPositionMenu.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);
        arPositionMenu.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0.3f);
    } */
} 

