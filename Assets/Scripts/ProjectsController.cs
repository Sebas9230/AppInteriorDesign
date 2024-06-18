//Load to Menu Designer and configure the Edit button to load to JsonScene
//with the correponding Json's
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using WebSocketSharp;

public class ProjectsController : MonoBehaviour
{
    public TMP_Text usuarioIngresadoTxt;
    public GameObject projectNamePrefab;
    public Transform projectsContainer;
    public  int numScene = 4;
    public RoomLoader roomLoader;
    public MySceneManager mySceneManager;
    public GameObject newProjectCanva;

    void Start()
    {
        // Verificar que los datos estén disponibles antes de acceder a ellos
        if (ProjectData.Projects == null || ProjectData.Projects.Count == 0)
        {
            Debug.LogError("Error: Datos del proyecto no disponibles en ProjectsController");
            return;
        }

        //cambiemos el número por el usuario que debería tener 0.0
        if (ProjectData.Designer == "2")
        {
            usuarioIngresadoTxt.text = "Carlitos";
        }
        
        // Crea tantos TMP_Text como proyectos haya y asigna los nombres y notas
        foreach (var project in ProjectData.Projects)
        {
            GameObject projectInstance = Instantiate(projectNamePrefab, projectsContainer);

            TMP_Text nameTxt = projectInstance.transform.Find("projectNameTxt").GetComponent<TMP_Text>();
            TMP_Text notasTxt = projectInstance.transform.Find("notasTxt").GetComponent<TMP_Text>();

            nameTxt.text = project.Nombre;
            notasTxt.text = project.NotasPersonales;

            Button editarBtn = projectInstance.transform.Find("EditProjectBtn").GetComponent<Button>();
            editarBtn.onClick.AddListener(() => OnEditProject(project));
        }
    }

    //EditButtonHandler
    private void OnEditProject(Project project)
    {
        //guardar datos actuales estáticamente para el cambio de escena
        ProjectData.Id = project.Id;
        ProjectData.CurrentHabitacionJson = project.UnityProyect.Habitacion;
        ProjectData.CurrentObjetoJson = project.UnityProyect.Objeto;

        if (!string.IsNullOrEmpty(ProjectData.CurrentHabitacionJson))
        {
            //if not empty then: jsonScene
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(numScene);
        }
        else
        {
            Debug.Log("*** Cargando Canva...");
            //else thne: show options (DefaultScene or ScannerScene)
            mySceneManager.ShowCanvas(newProjectCanva);

        }

        Debug.Log("Editar proyecto: " + project.Nombre);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Verifica si RoomLoader está disponible antes de llamar a sus métodos
        if (roomLoader != null)
        {
            roomLoader.LoadObjectsFromJsonData();
            roomLoader.LoadJsonScene();
        }
        else
        {
            Debug.LogError("RoomLoader no encontrado en la nueva escena.");
        }
    }
}
