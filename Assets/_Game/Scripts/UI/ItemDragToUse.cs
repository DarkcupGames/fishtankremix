using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragToUse : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public ItemDetailDisplay itemDetail;
    public Image virtualImg;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Utils.ConvertToScaledScreenPosition(Input.mousePosition, 1920, 1080);
        virtualImg.rectTransform.anchoredPosition = pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pos = Utils.ConvertToScaledScreenPosition(Input.mousePosition, 1920, 1080);
        virtualImg.rectTransform.anchoredPosition = pos;
        virtualImg.sprite = itemDetail.itemHolder.item.sprite;
        virtualImg.gameObject.SetActive(true);
        //itemDetail.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        virtualImg.gameObject.SetActive(false);
        itemDetail.itemHolder.amount -= 1;
       
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0);
        GameObject spawned = Instantiate(itemDetail.itemHolder.item.obj, pos, Quaternion.identity);
    }
}
