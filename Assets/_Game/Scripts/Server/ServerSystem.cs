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
    //public static string SERVER_URL = "ws://chatgolang.herokuapp.com/ws/main";
    public static string SERVER_URL = "ws://chatgolang.herokuapp.com/ws/command";
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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        dic = new Dictionary<string, Player>();

        GameObject spawn = Resources.Load<GameObject>("player") as GameObject;
        GameObject go = Instantiate(spawn, new Vector3(0,0), Quaternion.identity);
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

    async void Start()
    {
        websocket = new WebSocket(SERVER_URL);

        websocket.OnOpen += () =>
        {
            Debug1.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug1.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug1.Log("Connection closed!");
        };

        websocket.OnMessage += ReveiMessage;

        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.02f);

        await websocket.Connect();
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

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
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

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     