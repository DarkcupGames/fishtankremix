using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CloseButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject whatToClose;

    public void OnPointerDown(PointerEventData eventData)
    {
        whatToClose.SetActive(false);
    }
}
