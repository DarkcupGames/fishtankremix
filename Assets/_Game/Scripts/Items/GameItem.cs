using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Game Item", menuName = "GameItem")]
public class GameItem : ScriptableObject
{
    public string codename;
    public string displayname;
    public Sprite sprite;
    public float price;
    public GameObject obj;
}
