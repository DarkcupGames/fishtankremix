using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingOrderFixer : MonoBehaviour
{
    public enum Type { Static, Dynamic }
    public Type type = Type.Static;

    private float UPDATE_RATE = 0.1f;

    float count;
    SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        if (type == Type.Static)
        {
            render.sortingOrder = (int)(-transform.position.y * 100) ;
            Destroy(this);
        }
    }
    private void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = UPDATE_RATE;
            render.sortingOrder = ((int)-transform.position.y) * 100;
        }
    }
}
