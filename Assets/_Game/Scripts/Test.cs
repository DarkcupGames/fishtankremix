using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform testObj;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Input From Update: " + Input.mousePosition);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector3(pos.x, pos.y, 0);
            testObj.transform.position = pos;
        }
    }
}
