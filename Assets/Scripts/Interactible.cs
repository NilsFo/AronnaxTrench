using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactible : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<PlayerCursor>().canInteract = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<PlayerCursor>().canInteract = false;
    }
}
