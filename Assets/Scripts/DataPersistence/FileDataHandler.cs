using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath;
    private string dataFileName;
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public ProjectData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        ProjectData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //load the serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from Json back into the c# object
                loadedData = JsonUtility.FromJson<ProjectData>(dataToLoad);
            }catch (Exception e)
            {
                Debug.LogError("Error al cargar la información del archivo: " + fullPath + "\n" + e);
            }
        }
        
        return loadedData;
    }
    public void Save(ProjectData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create the directory the file will be written to if it doesn't already exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //serialize the c# project data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);
            //using blocks to ensure the connection to the file is closed once is writed the data
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            
        }catch (Exception e)
        {
            Debug.LogError("Error al guardar información en el archivo: " + fullPath + "\n" + e);
        } 
    }
}
