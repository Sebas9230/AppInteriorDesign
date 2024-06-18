//For serialization of the Json sending and obtaining from the server

[System.Serializable]
public class WebSocketMessage
{
    public string message;
    public PositionData position;

    [System.Serializable]
    public class PositionData
    {
        public float x;
        public float y;
        public float z;
    }
}
