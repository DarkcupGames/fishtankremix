using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : InteractableObject
{
    public string state = "normal";
    public Sprite cutted;
    public Sprite normal;

    public override void Interact()
    {
        base.Interact();

        if (state == "cutted")
            return;

        state = "cutted";
        GetComponent<SpriteRenderer>().sprite = cutted;
        GamePlay.Instance.interactImg.sprite = cutted;
        GamePlay.Instance.AddItem(item, 1);
        Invoke("Respawn", 20f);
    }

    public void Respawn()
    {
        GetComponent<SpriteRenderer>().sprite = normal;
        state = "normal";
    }

    public override void Update()
    {
        base.Update();
    }
}
