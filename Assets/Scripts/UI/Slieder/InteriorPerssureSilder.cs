using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Slieder
{
  public class InteriorPerssureSilder : MonoBehaviour
  {
    public ShipUiManager manager;
    public Slider slider;
    public int index;

    public static float myValue;
    public static int changeIndex;

    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    private float offset = 0;

    void Start()
    {
      slider.value = 0.5f;
      offset = Random.value;
    }

    void OnEnable()
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
      manager.GameState.InteriorPressurePumpPressure =
        manager.GameState.MaxInteriorPressurePumpPressure * ((value - 0.5f) * 2);
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
        float ghostValue = (Mathf.Sin((_timer + offset) * speed) + 1) / 2f;
        slider.value = ghostValue;

        slider.interactable = false;
      }
      else
      {
        float diff = Mathf.Abs(slider.value - myValue);
        if (changeIndex != index && diff > 0)
        {
          slider.value = myValue;
        }
      }
    }
  }
}