using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utf8Json;
//using Newtonsoft.Json.Linq.JObject;

public class ServerSystem : MonoBehaviour
{
    public static string SERVER_URL = "ws://fishtankserver.herokuapp.com/ws";
    public static ServerSystem Instance;

    public static ClientData client;
    public static Dictionary<string, SyncPosition> dic;

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
        dic = new Dictionary<string, SyncPosition>();
        client = new ClientData();
        client.id = playerid;
        client.p = player.transform.position;
        client.s = "stand";
        // Create player
        //ClientObject data = new ClientObject();
        //data.id = playerid;
        //data.desirePos = player.transform.position;
        //data.speed = 4f;
        //data.owner = playerid;


        GameObject spawn = Resources.Load<GameObject>("player") as GameObject;
        GameObject go = Instantiate(spawn, new Vector3(0,0), Quaternion.identity);
        player = go;
        player.GetComponent<SyncPosition>().data = client;
        player.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerid;
        dic.Add(playerid, player.GetComponent<SyncPosition>());

        Vector3 pos = player.transform.position;
        Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
        Camera.main.transform.SetParent(player.transform);
        //client.objects.Add(player.GetComponent<SyncPosition>().data);

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

        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        await websocket.Connect();
    }

    public void ReveiMessage(byte[] bytes)
    {
        /*
        ClientData data = JsonSerializer.Deserialize<ClientData>(bytes);

        //var message = System.Text.Encoding.UTF8.GetString(bytes);
        //Debug.Log(message);

        //ClientData data = JsonUtility.FromJson<ClientData>(message);
        if (data.id == playerid)
            return;

        if (!dic.ContainsKey(data.id))
        {
            GameObject spawn = Resources.Load<GameObject>("player") as GameObject;
            GameObject go = Instantiate(spawn, data.p, Quaternion.identity);
            go.GetComponent<SyncPosition>().data = data;
            //sync = data;
        }

        //for (int i = 0; i < data.objects.Count; i++)
        //{
        //    ClientObject obj = data.objects[i];

        //    if (obj.owner == playerid)
        //        continue;

        //    if (!dic.ContainsKey(obj.id))
        //    {
        //        GameObject spawn = Resources.Load<GameObject>(obj.objectName) as GameObject;
        //        GameObject go = Instantiate(spawn, obj.desirePos, Quaternion.identity);
        //        var comp = go.GetComponent<SyncPosition>().data;
        //        comp.id = obj.id;
        //        comp.desirePos = obj.desirePos;
        //        comp.state = obj.state;
        //        comp.speed = obj.speed;

        //        go.transform.GetChild(0).GetComponent<TextMeshPro>().text = obj.id;

        //        dic.Add(obj.id, go.GetComponent<SyncPosition>());
        //    }
        //    else
        //    {
        //        //if (obj.owner != playerid)
        //        //{

        //        //}

        //        dic[obj.id].data.desirePos = obj.desirePos;
        //        dic[obj.id].data.state = obj.state;
        //        //Debug.Log("desirePos: " + obj.desirePos);
        //    }
        //}
        */
    }

    void Update()
    {
//#if !UNITY_WEBGL || UNITY_EDITOR
//        websocket.DispatchMessageQueue();
//#endif

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(JsonUtility.ToJson(client));
            foreach (KeyValuePair<string,SyncPosition> item in dic)
            {
                Debug.Log("dic " + item.Key);
            }
        }
            

        if (!sendRequest)
            return;

        if (dic.ContainsKey(playerid))
        {
            if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                dic[playerid].data.s = "stand";
            }
            else
            {
                //dic[playerid].data.s = "walk";
                //Vector3 pos = new Vector3(joystick.Horizontal, joystick.Vertical).normalized * 4f * Time.deltaTime;

                //player.transform.position += pos;
                //player.GetComponent<Rigidbody2D>().velocity = new Vector3(joystick.Horizontal, joystick.Vertical);
                //dic[playerid].data.desirePos = dic[playerid].transform.position;
            }
        }
    }

    async void SendWebSocketMessage()
    {
        if (!sendRequest)
            return;

        if (websocket.State == WebSocketState.Open)
        {
            //string json = ;
            //await websocket.SendText(JsonUtility.ToJson(client));
            await websocket.Send(JsonSerializer.Serialize<ClientData>(client));
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    //public void ExecuteCommand(string str)
    //{
    //    JObject json = JObject.Parse(str);
    //    if (json.HasValues())
    //    var content = json[""]
    //}
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     