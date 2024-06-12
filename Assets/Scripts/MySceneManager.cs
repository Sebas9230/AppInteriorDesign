/*************************************************************************** 
*   Singleton for Navegation in the escene and menus 
****************************************************************************/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    public static MySceneManager instance;//Singletón - instanciado una vez,  globalmente accesible 
    
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

    //Change Scene
    public void ChangeScene(int numEscena)
    {
        SceneManager.LoadScene(numEscena);
    }

    //Show Canvas
    public void ShowCanvas(GameObject canvas){
        canvas.SetActive(true);
    }
    public void HideCanvas(GameObject canvas){
        canvas.SetActive(false);
    }
}
