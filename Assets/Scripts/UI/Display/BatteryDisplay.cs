using UnityEngine;
using UnityEngine.UI;

namespace UI.Display
{
    public class BatteryDisplay : MonoBehaviour
    {
        public ShipUiManager manager;

        public Slider slider;

        void FixedUpdate()
        {
            slider.value = manager.GameState.CurrentBattery / manager.GameState.MaxBattery;
        }
    }
}
