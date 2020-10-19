using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    //public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtAmount;
    public Image image;

    public GameItem item;
    public int amount = 1;

    public void UpdateDisplay()
    {
        //txtName.text = item.displayname;
        image.sprite = item.sprite;
        txtAmount.text = amount.ToString();
    }
}
