using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ClientData client;
    Joystick joystick;
    float speed = 2f;

    public Sprite[] walk;
    public Sprite[] stand;

    void Start()
    {
        joystick = ServerSystem.Instance.joystick;
        if (client == null)
            client = new ClientData();
    }

    void Update()
    {
        if (client.s == "stop")
            return;

        UpdatePosition();
        UpdateAnimation();
        CheckInput();
    }

    public void UpdatePosition()
    {
        if (client.id != ServerSystem.playerid)
        {
            if (client.s == "stand")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            }
            else
            {

                Vector3 dif = client.p - transform.position;
                if (dif.magnitude < 0.1f)
                    dif = new Vector3(0, 0);

                GetComponent<Rigidbody2D>().velocity = dif.normalized * speed;
                Debug.Log("v = " + client.v);
            }

            return;
        }

        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            client.s = "stand";
            GetComponent<DAnimator>().spritesheet = stand;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        }
        else
        {
            Vector3 velocity = new Vector3(joystick.Horizontal, joystick.Vertical).normalized * speed;
            GetComponent<Rigidbody2D>().velocity = velocity;
            client.s = "walk";
        }

        client.p = transform.position;
        client.v = GetComponent<Rigidbody2D>().velocity;
    }

    public void UpdateAnimation()
    {
        if (client.s == "stand")
            GetComponent<DAnimator>().spritesheet = stand;
        if (client.s == "walk")
            GetComponent<DAnimator>().spritesheet = walk;

        if (client.v.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (client.v.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void CheckInput()
    {
        if (client.id != ServerSystem.playerid)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject attack = Resources.Load<GameObject>("Effect/attack") as GameObject;
            Debug.Log(attack);
            GameObject spawn = Instantiate(attack, transform.position, Quaternion.identity);
            spawn.transform.SetParent(transform);
        }

    }
}
