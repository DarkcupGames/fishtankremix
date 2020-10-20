using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    public TextMeshProUGUI txtAmount;
    public Image image;

    public GameItem item;
    public int amount = 1;

    public void UpdateDisplay()
    {
        image.sprite = item.sprite;
        txtAmount.text = amount.ToString();
    }

    public void OnClick()
    {
        GamePlay.Instance.ShowItemDetail(this);
    }
}

