using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject dark;
    public GameObject light;
    
    // Start is called before the first frame update
    void Start()
    {
        dark.SetActive(true);
        light.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        dark.SetActive(false);
        light.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dark.SetActive(true);
        light.SetActive(false);
    }
}
