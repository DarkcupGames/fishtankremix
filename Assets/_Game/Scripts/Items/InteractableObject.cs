using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    public static float INTERACT_DISTANCE = 0.5f;
    public GameItem item;

    public void Update()
    {
        float d = Vector3.Distance(transform.position, ServerSystem.Instance.player.transform.position);
        if (d < INTERACT_DISTANCE && GamePlay.Instance.interact == null) 
        {
            GamePlay.Instance.interact = this;
            GamePlay.Instance.interactImg.sprite = item.sprite;
        }

        else if (d > INTERACT_DISTANCE && GamePlay.Instance.interact == this)
        {
            GamePlay.Instance.interact = null;
            GamePlay.Instance.interactImg.sprite = null;
        }
    }
}
