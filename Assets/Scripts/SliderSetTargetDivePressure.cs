using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SliderSetTargetDivePressure : Interactible
{
    public static float myValue = 0;
    
    public Slider slider;

    public ShipUiManager manager;
    public float offset = 0.5f;

    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    private float offsetTime = 0;

    void Start()
    {
        offsetTime = Random.value;
        
        myValue = (manager.GameState.MaxPumpPressure + manager.GameState.PressureDelta) /
                  (2 * manager.GameState.MaxPumpPressure);
        slider.value = myValue;
    }

    void OnEnable () 
    {
        slider.onValueChanged.AddListener(ChangeValue);
        ChangeValue(slider.value);
    }
    void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }
  
    void ChangeValue(float value)
    {
        myValue = value;
        manager.GameState.PressureDelta = manager.GameState.MaxPumpPressure * (value - 0.5f) * 2;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _maxTime)
        {
            _timer = 0;
        }

        if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
        {
            slider.onValueChanged.RemoveAllListeners();
            float ghostValue = (Mathf.Sin((_timer + offsetTime) * speed) + 1) / 2f;
            slider.value = ghostValue;

            slider.interactable = false;
        }
        else
        {
            slider.interactable = true;
            slider.value = myValue;
      
            slider.onValueChanged.AddListener(ChangeValue);
            ChangeValue(slider.value);
        }
    }
}
