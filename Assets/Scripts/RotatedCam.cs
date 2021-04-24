using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatedCam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameState gameState;
    public float rotationSpeed = 3;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameState.PlayerRotationSpeed = rotationSpeed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameState.PlayerRotationSpeed = 0;
    }

    
}
