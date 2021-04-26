using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetSubThrust : MonoBehaviour
{

  public Slider slider;
  
  public ShipUiManager manager;
  public float offset = 0.5f;
  public int index;

  public static float myValue = 0;
  public static int changeIndex = 0;
  
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
  }
}
