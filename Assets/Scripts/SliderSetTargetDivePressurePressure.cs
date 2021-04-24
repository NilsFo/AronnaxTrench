using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetTargetDivePressure : MonoBehaviour
{
    public Slider slider;
  
    public GameState gameState;
    public float offset = 0.5f;

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
        gameState.TargetDivePressure = gameState.MaxDivePressure * value;
    }
}
