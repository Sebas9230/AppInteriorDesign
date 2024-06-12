using System;
using System.IO;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class RoomSaver : MonoBehaviour
{
public MRUK mruk;

    public void SaveCurrentSceneToJson(string fileName)
    {
        if (mruk == null)
        {
            Debug.LogError("MRUK script reference is missing.");
            return;
        }

        string jsonString = mruk.SaveSceneToJsonString(SerializationHelpers.CoordinateSystem.Unity); // O Unreal, según tu necesidad
        string path = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(path, jsonString);
            Debug.Log("Escena guardada correctamente en: " + path);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar la escena: " + e.Message);
        }
    }
}
