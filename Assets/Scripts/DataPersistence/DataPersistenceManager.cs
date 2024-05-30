/*************************************************************************** 
* Singleton for Load and Save of the data of a project/scene
***************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{ 
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private ProjectData projectData; //track the data's project, use the principal class
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set;}

    //for the singleton
    private void Awake()
    {
        if(instance != null){
            Debug.LogError("Se encontró más the un Data Persistence Manager en la escena");
            Destroy(gameObject);
        }else{
            instance = this;
        }
    }
    void Start()
    {
        this.dataHandler = new FileDataHandler(Environment.CurrentDirectory, fileName);
        Debug.Log("Start method in DataPersistenceManager.cs ... ");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadProject();
    }

    //logic for saving and loading a project
    public void NewProject()
    {
        this.projectData = new ProjectData();
    }
    public void LoadProject()
    {
        //load the data from a file using the data handler
        this.projectData = dataHandler.Load();
        //if no data, initialize a new project
        if (this.projectData == null){
            Debug.Log("No se ha encontrada ningún proyecto. Inicializando uno nuevo");
            NewProject();
        }

        //push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(projectData);
        }
    }
    public void SaveProject()
    {
        //pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref projectData);
        }
        //save that data to a file using the data handler
        dataHandler.Save(projectData);

        Debug.Log("*** Data saved at: " + Environment.CurrentDirectory+" ***");
    }

    //Find persistence objects/data
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects); //initialize the list
    }

    void OnApplicationQuit()
    {
        SaveProject();
    }
}
