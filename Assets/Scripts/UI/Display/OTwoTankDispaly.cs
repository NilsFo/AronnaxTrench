using UnityEngine;
using UnityEngine.UI;

namespace UI.Display
{
  public class OTwoTankDispaly : MonoBehaviour
  {
    public ShipUiManager manager;

    public Slider slider;

    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    private float offset = 0;

    void Start()
    {
      offset = Random.value;
    }

    void FixedUpdate()
    {
      _timer += Time.deltaTime;
      if (_timer > _maxTime)
      {
        _timer = 0;
      }
      
      if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
      {
        float ghostValue = (Mathf.Sin((_timer + offset) * speed) + 1) / 2f;
        slider.value = ghostValue;
      }
      else
      {
        slider.value = manager.GameState.CurrentOTwo / manager.GameState.MaxOTwo;
      }
    }
  }
}