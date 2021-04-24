using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatedCam : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public FloatVariable rotation;
    public float rotationSpeed = 3;
    
    private float _currentRotationSpeed;
    
    void Update()
    {
        // The step size is equal to speed times frame time.
        float singleStep = _currentRotationSpeed * Time.deltaTime;
        float newValue = singleStep + rotation.Value;

        rotation.Value = newValue;
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
