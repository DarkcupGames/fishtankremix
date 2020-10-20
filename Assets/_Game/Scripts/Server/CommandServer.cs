using UnityEngine;
using System.Collections;
using System;
using NativeWebSocket;

public class CommandServer : MonoBehaviour
{
    //public static string SERVER_URL_COMMAND = "ws://chatgolang.herokuapp.com/ws/command";

    public string url;
    public Action action;
    WebSocket webSocket;

    // Use this for initialization
    async void StartWebsocket(string url, WebSocketMessageEventHandler onMessage)
    {
        webSocket = new WebSocket(url);

        webSocket.OnMessage += onMessage;

        await webSocket.Connect();

        //commandsocket = new WebSocket(SERVER_URL_COMMAND);

        //commandsocket.OnMessage += ReveiCommandMessage;

        //commandsocket.OnOpen += () =>
        //{
        //    Debug1.Log("Connection comamnd open!");
        //};

        //commandsocket.OnError += (e) =>
        //{
        //    Debug1.Log("Error command! " + e);
        //};

        //commandsocket.OnClose += (e) =>
        //{
        //    Debug1.Log("Connection command closed!");
        //};

        ////commandsocket.OnError += (e) =>
        ////{
        ////    Debug.Log("Error! " + e);
        ////};

        //await websocket.Connect();

        //await commandsocket.Connect();
    }

    void Update()
    {

    }
}
