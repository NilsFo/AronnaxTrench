using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatedCam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameState gameState;
    public float rotationSpeed = 3;
    
    private float _currentRotationSpeed;
    
    void Update()
    {
        // The step size is equal to speed times frame time.
        float singleStep = _currentRotationSpeed * Time.deltaTime;
        gameState.PlayerRotation = singleStep + gameState.PlayerRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentRotationSpeed = rotationSpeed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _currentRotationSpeed = 0;
    }

    
}
