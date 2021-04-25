using UnityEngine;
using UnityEngine.UI;

namespace UI.Display
{
  public class BallastDisplay : MonoBehaviour
  {
    public ShipUiManager manager;

    public Slider slider;

    void FixedUpdate()
    {
      slider.value = manager.GameState.CurrentDivePressure / manager.GameState.MaxDivePressure;
    }
  }
}
