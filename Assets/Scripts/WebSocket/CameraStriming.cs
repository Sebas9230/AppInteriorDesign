//envía bytes de imagen sin procesar al servidor (lo que ve la cámara)
using UnityEngine;
using WebSocketSharp;

public class CameraStreaming : MonoBehaviour
{
    private WebSocket ws;
    public Camera secondaryCamera;
    public RenderTexture renderTexture;

    void Start()
    {
        ws = new WebSocket("wss://projectvertexscape.onrender.com/ws/jsonScene/");

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
    }

    void Update()
    {
        // Capturar la vista de la cámara en la textura
        if (secondaryCamera != null && renderTexture != null)
        {
            // Capturar la imagen de la segunda cámara
            RenderTexture.active = renderTexture;
            Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();
            RenderTexture.active = null;

            // Convertir la imagen a bytes
            byte[] bytes = texture2D.EncodeToJPG();

            // Enviar los bytes al servidor a través del WebSocket
            if (ws != null && ws.IsAlive)
            {
                ws.Send(bytes);
            }

            Destroy(texture2D);
        }
        else
        {
            Debug.LogError("No se encontró el componente Camera en centerEyeAnchor");
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
