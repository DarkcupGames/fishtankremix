using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    public ClientData data;
    public float speed = 4f;

    private void Start()
    {
        if (data == null)
            data = new ClientData();
    }

    private void Update()
    {
        if (data.id == ServerSystem.playerid)
        {
            //data.p = transform.position;
            //data.s = "somestate"; // change here
        }
        else
        {
            //Vector3 pos = data.p - transform.position;
            //transform.position += pos.normalized * speed * Time.deltaTime;
        }

        //if (data.owner == ServerSystem.playerid)
        //{
        //    data.desirePos = transform.position;
        //    return;
        //}

        //if (data.state == "stand")
        //    return;

        //Vector3 pos = data.desirePos - transform.position;
        //transform.position += pos.normalized * data.speed * Time.deltaTime;
        
    }
}
