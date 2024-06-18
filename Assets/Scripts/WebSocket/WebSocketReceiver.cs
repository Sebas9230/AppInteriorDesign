//para que pueda recibir datos del servidor Websocket en Django
using UnityEngine;
using WebSocketSharp;

public class WebSocketReceiver : MonoBehaviour
{
    private WebSocket ws;
    private Transform cameraTransform;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8000/ws/jsonScene/");

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message from server: " + e.Data);
            var data = JsonUtility.FromJson<WebSocketMessage>(e.Data);
            // Actualiza la escena con los datos recibidos
            UpdateScene(data);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket error: " + e.Message);
        };

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection established");
        };

        ws.Connect();
    }

    void UpdateScene(WebSocketMessage data)
    {
         if (cameraTransform != null)
        {
            cameraTransform.position = new Vector3(data.position.x, data.position.y, data.position.z);
        }
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }

}
