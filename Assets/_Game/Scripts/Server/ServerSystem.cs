using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utf8Json;
//using Newtonsoft.Json.Linq.JObject;

public class ServerSystem : MonoBehaviour
{
    //public static string SERVER_URL = "ws://fishtankserver.herokuapp.com/ws";
    public static string SERVER_URL_MAIN = "ws://chatgolang.herokuapp.com/ws/main";
    public static string SERVER_URL_COMMAND = "ws://chatgolang.herokuapp.com/ws/command";
    public static ServerSystem Instance;

    public static ClientData curPlayer;
    public static Dictionary<string, Player> dic;

    public static bool sendRequest = false;
    public static string playerid;

    public Joystick joystick;
    public GameObject player;
    public float speed = 4f;
    public CameraFollower cameraFollower;

    WebSocket websocket;
    WebSocket commandsocket;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        dic = new Dictionary<string, Player>();

        GameObject spawn = Resources.Load<GameObject>("player") as GameObject;
        GameObject go = Instantiate(spawn, new Vector3(0, 0), Quaternion.identity);
        player = go;

        curPlayer = player.GetComponent<Player>().client;
        curPlayer.id = playerid;
        curPlayer.v = new Vector3(0, 0);
        curPlayer.p = new Vector3(0, 0);
        curPlayer.s = "stand";

        player.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerid;
        dic.Add(playerid, player.GetComponent<Player>());

        cameraFollower.target = player;
    }

    void Start()
    {
        websocket = CreateWebsocketConnection(SERVER_URL_MAIN, ReveiMessage);
        commandsocket = CreateWebsocketConnection(SERVER_URL_COMMAND, ReveiCommandMessage);

        InvokeRepeating("SendWebSocketMessage", 0f, 0.2f);
    }

    public WebSocket CreateWebsocketConnection(string url, WebSocketMessageEventHandler action)
    {
        GameObject go = Instantiate(new GameObject(), transform);
        go.AddComponent(typeof(WebSocketConnecter));
        go.GetComponent<WebSocketConnecter>().StartWebsocket(url, action);
        return go.GetComponent<WebSocketConnecter>().webSocket;
    }

    public void ReveiMessage(byte[] bytes)
    {
        ClientData data = JsonSerializer.Deserialize<ClientData>(bytes);

        if (data.id == playerid)
            return;

        if (!dic.ContainsKey(data.id))
        {
            GameObject spawn = Resources.Load<GameObject>("player") as GameObject;
            GameObject go = Instantiate(spawn, data.p, Quaternion.identity);
            go.GetComponent<Player>().client = data;
            dic.Add(data.id, go.GetComponent<Player>());
        }
        else
        {
            dic[data.id].client = data;
        }
    }

    public void ReveiCommandMessage(byte[] bytes)
    {
        WebsocketCommand command = JsonSerializer.Deserialize<WebsocketCommand>(bytes);

        if (command.type == "create")
        {
            GamePlay.Instance.CreateGameObjectFromPath(command.s1, command.v1);
        }
    }

    public async void SendCommand(WebsocketCommand command)
    {
        await commandsocket.Send(JsonSerializer.Serialize(command));
    }

    async void SendWebSocketMessage()
    {
        if (!sendRequest)
            return;

        if (websocket.State == WebSocketState.Open)
        {
            await websocket.Send(JsonSerializer.Serialize<ClientData>(curPlayer));
        }
    }

    public void CreateTree()
    {
        WebsocketCommand command = new WebsocketCommand();
        command.type = "create";
        command.s1 = "tree";
        command.v1 = player.transform.position;

        SendCommand(command);
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     