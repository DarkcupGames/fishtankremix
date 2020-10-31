using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 ConvertToScaledScreenPosition(Vector3 pos, float resx, float resy)
    {
        return new Vector3(pos.x * resx / Screen.width, pos.y * resy / Screen.height);
    }
}

