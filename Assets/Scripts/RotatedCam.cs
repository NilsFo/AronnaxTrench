using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotatedCam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameState gameState;
    public Sprite arrow;
    public Sprite arrowActive;
    public float rotationSpeed = 3;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameState.PlayerRotationSpeed = rotationSpeed;
        GetComponentInChildren<Image>().sprite = arrowActive;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameState.PlayerRotationSpeed = 0;
        GetComponentInChildren<Image>().sprite = arrow;
    }

    
}
