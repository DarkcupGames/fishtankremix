using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientData
{
    public string id;
    public Vector3 p; // position
    public Vector3 v; // velocity
    public string s; // state

    //public List<ClientObject> objects;
    //public Dictionary<string, string> commands;

    //public ClientData()
    //{
    //    objects = new List<ClientObject>();
    //    commands = new Dictionary<string, string>();
    //}

    //private void Update()
    //{
    //    if (data.id == ServerSystem.playerid)
    //        return;

    //    if (data.state == "stand")
    //        return;

    //    Vector3 pos = data.desirePos - transform.position;
    //    transform.position += pos.normalized * data.speed * Time.deltaTime;
    //}
}
