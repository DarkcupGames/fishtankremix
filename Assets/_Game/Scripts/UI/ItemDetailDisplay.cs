using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ItemDetailDisplay : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtAmount;
    public TextMeshProUGUI txtDetail;
    public Image image;
    public InventoryItemDisplay itemHolder;

    public void UpdateDisplay(InventoryItemDisplay itemHolder)
    {
        this.itemHolder = itemHolder;
        txtName.text = itemHolder.item.displayname;
        txtAmount.text = itemHolder.amount.ToString();
        txtDetail.text = itemHolder.item.description;

        image.sprite = itemHolder.item.sprite;
    }

    public void OnClick()
    {

    }
}
