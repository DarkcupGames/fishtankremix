using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSystem : MonoBehaviour
{
    public static string SERVER_URL = "wss://fishtankserver.herokuapp.com/ws";
    public static ServerSystem Instance;
    WebSocket websocket;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Connect(string username) 
    {
        GameSystem.clientInfo.username = username;

    }

    // Start is called before the first frame update
    async void Start()
    {
        //websocket = new WebSocket("ws://echo.websocket.org");
        websocket = new WebSocket(SERVER_URL);
        
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(JsonUtility.ToJson(GameSystem.clientInfo));
            //// Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            //// Sending plain text
            //await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}


