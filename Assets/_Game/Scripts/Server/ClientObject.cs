using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientObject
{
    public string id;
    public string objectName = "player";
    public string owner;
    public Vector3 desirePos;
    public string state = "not created";
    public float speed = 4f;
}
