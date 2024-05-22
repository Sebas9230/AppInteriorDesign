using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

//Singletón - instanciado una vez,  globalmente accesible 
    public static MySceneManager instance;
    private void Awake(){
        if (instance!=null && instance!=this){
            Destroy(gameObject);
        }else
        {
            instance = this;
        }
    }
    void Start()
    {
        MainMenu();
    }

    public void MainMenu(){
        OnMainMenu?.Invoke();
        Debug.Log("Menú Principal Activado");
    }
    public void ItemsMenu(){
        OnItemsMenu?.Invoke();
        Debug.Log("Menú de Items Activado");
    }
    public void ARPosition(){
        OnARPosition?.Invoke();
        Debug.Log("Posición AR Activado");
    }        

    public void ChangeScene(int numEscena)
    {
        SceneManager.LoadScene(numEscena);
    }

    public void ShowProfile(GameObject profileCanvas){
        profileCanvas.SetActive(true);
    }
    public void HideProfile(GameObject profileCanvas){
        profileCanvas.SetActive(false);
    }
}
