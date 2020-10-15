using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    public ClientObject data;

    private void Start()
    {
        if (data == null)
            data = new ClientObject();
    }

    private void Update()
    {
        if (data.owner == ServerSystem.playerid)
        {
            data.desirePos = transform.position;
            return;
        }

        if (data.state == "stand")
            return;

        Vector3 pos = data.desirePos - transform.position;
        transform.position += pos.normalized * data.speed * Time.deltaTime;
        
    }
}
