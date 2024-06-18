//para que pueda enviar la posición de la cámara al servidor Websocket en Django
using UnityEngine;
using WebSocketSharp;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocket ws;
    private Vector3 lastPosition;
    private Transform cameraTransform;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8000/ws/jsonScene/");

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message from server: " + e.Data);
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

        // Busca el componente OVRCameraRig y obtén la posición de la cámara central
        var cameraRig = FindObjectOfType<OVRCameraRig>();
        if (cameraRig != null)
        {
            cameraTransform = cameraRig.centerEyeAnchor;
            lastPosition = cameraTransform.position;
        }
    }

    void Update()
    {
        if (ws != null && ws.IsAlive && cameraTransform != null)
        {
            if (cameraTransform.position != lastPosition)
            {
                var data = new WebSocketMessage
                {
                    message = "Hello from Meta Quest Pro",
                    position = new WebSocketMessage.PositionData
                    {
                        x = cameraTransform.position.x,
                        y = cameraTransform.position.y,
                        z = cameraTransform.position.z
                    }
                };
                string jsonData = JsonUtility.ToJson(data);
                
                Debug.Log("Sending data: " + jsonData); //

                // Evitar enviar datos vacíos
                if (!string.IsNullOrEmpty(jsonData))
                {
                    ws.Send(jsonData);
                }

                lastPosition = cameraTransform.position;
            }
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
