using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteriorPerssureSilder : MonoBehaviour
{
    public ShipUiManager manager;
    public Slider slider;
    
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
        manager.GameState.InteriorPressurePumpPressure = manager.GameState.MaxInteriorPressurePumpPressure * value;
    }
}
