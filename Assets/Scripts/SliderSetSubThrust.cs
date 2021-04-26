using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SliderSetSubThrust : MonoBehaviour
{

  public Slider slider;
  
  public ShipUiManager manager;
  public float offset = 0.5f;
  public int index;

  public static float myValue = 0;
  public static int changeIndex = 0;
  
  private float _maxTime = Mathf.PI * 2;
  private float _timer = 0;
  private float speed = 4f;
  private float offsetTime = 0;

  void Start()
  {
    offsetTime = Random.value;
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
    changeIndex = index;
    manager.GameState.SubmarineThrust = manager.GameState.SubmarineMaxThrust * ((myValue - offset) * 2);
  }

  void Update()
  {
    float diff = Mathf.Abs(slider.value = myValue);
    if (changeIndex != index && diff >= 0)
    {
      slider.value = myValue;
    }
    
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
