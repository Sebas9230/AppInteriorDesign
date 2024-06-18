using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class ApiManager : MonoBehaviour
{
    private string authUrl = "https://projectvertexscape.onrender.com/api/login/";
    private string projectsUrl = "https://projectvertexscape.onrender.com/api/proyectos/";

    private string unityproyectUrl = "https://projectvertexscape.onrender.com/api/unity-proyecto/";

    public MySceneManager sceneManager;
    public SaverManager saverManager;
    //POST credentials
    public IEnumerator AuthenticateAndGetData(string email, string password)
    {
        // Crear un objeto JSON con las credenciales
        JObject json = new JObject();
        json.Add("email", email);
        json.Add("password", password);

        // Crear una solicitud web POST para autenticarse
        using (UnityWebRequest request = new UnityWebRequest(authUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error de autenticación: " + request.error);
            }
            else
            {
                Debug.Log("Authentication succesfull !");

                // Obtener el token de la respuesta JSON
                string jsonResponse = request.downloadHandler.text;
                JObject responseJson = JObject.Parse(jsonResponse);
                string accessToken = responseJson.Value<string>("token");
                ProjectData.Token = accessToken;

                //consumir api y guardar datos estáticamente en Projects de ProjectData
                yield return StartCoroutine(GetDataFromApi(accessToken));
                if (ProjectData.Projects != null && ProjectData.Projects.Count >= 0)
                {
                    sceneManager.ChangeScene(1);
                }
                else
                {
                    Debug.LogError("Error: Projects data not available");
                }
            
            }
        }
    }

    //Get projects
    public IEnumerator GetDataFromApi(string accessToken)
    {
        using (UnityWebRequest request = new UnityWebRequest(projectsUrl, "GET"))
        {
            request.SetRequestHeader("Authorization", "Token " + accessToken);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener datos de la API: " + request.error);
                yield break;
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    Debug.LogError("Error: Respuesta vacía de la API");
                    yield break;
                }

                //asignar contenido de la API:
                JArray projectsArray = JArray.Parse(jsonResponse);

                ProjectData.Projects = new List<Project>();

                foreach (JObject project in projectsArray)
                {
                    ProjectData.Projects.Add(new Project
                    {
                        Id = project.Value<int>("id"),
                        Nombre = project.Value<string>("nombre"),
                        NotasPersonales = project.Value<string>("notas_personales"),
                        UnityProyect = new UnityProject
                        {
                            Id = project["unityproyect"]?.Value<int?>("id"),
                            Objeto = project["unityproyect"]?.Value<string>("objeto"),
                            Habitacion = project["unityproyect"]?.Value<string>("habitacion")
                        }
                    });
                }

                //el diseñador es el mismo para todos los proyectos
                ProjectData.Designer = projectsArray[0].Value<string>("diseñador");
                
                Debug.Log("API data obtained: " + jsonResponse);
            }
        }
    }

    //POST the scene in unityProject
    public IEnumerator PostSceneData()
    {
        saverManager.SaveCurrentSceneToJson();

        var apiRequestData = new
        {
            id = ProjectData.Id,
            habitacion = ProjectData.CurrentHabitacionJson,
            objeto = ProjectData.CurrentObjetoJson
        };

        string apiRequestJson = JsonConvert.SerializeObject(apiRequestData);
        Debug.Log("Request JSON: " + apiRequestJson);  // Imprimir el JSON para verificarlo


        using (UnityWebRequest request = new UnityWebRequest(unityproyectUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(apiRequestJson);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al guardar la escena: " + request.error);
            }
            else
            {
                Debug.Log("Escena guardada correctamente en el servidor.");
            }
        }
    }
}
