using UnityEngine;
using System.Collections;
using System;
using NativeWebSocket;

public class WebSocketConnecter : MonoBehaviour
{
    public WebSocket webSocket;
    public string url;
    public Action action;

    public async void StartWebsocket(string url, WebSocketMessageEventHandler onMessage)
    {
        webSocket = new WebSocket(url);

        webSocket.OnMessage += onMessage;

        webSocket.OnOpen += () =>
        {
            Debug1.Log("Connection open: " + url);
        };

        webSocket.OnError += (e) =>
        {
            Debug1.Log("Error on "+ url +  ": " + e);
        };

        webSocket.OnClose += (e) =>
        {
            Debug1.Log("Connection closed : " + url);
        };

        await webSocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        webSocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await webSocket.Close();
    }
}
