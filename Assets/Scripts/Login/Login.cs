using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField txtName;
    public void OnLogin()
    {
        GameSystem.clientInfo.username = txtName.text;
    }
}
