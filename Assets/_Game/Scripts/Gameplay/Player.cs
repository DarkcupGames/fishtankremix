using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Joystick joystick;
    float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        joystick = ServerSystem.Instance.joystick;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(joystick.Horizontal, joystick.Vertical) * speed * Time.deltaTime;
        if (joystick.Horizontal < 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (joystick.Horizontal > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
