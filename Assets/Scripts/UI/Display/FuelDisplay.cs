using UnityEngine;
using UnityEngine.UI;

namespace UI.Display
{
    public class FuelDisplay : MonoBehaviour
    {
        public ShipUiManager manager;

        public Slider slider;

        void FixedUpdate()
        {
            slider.value = manager.GameState.CurrentFuel / manager.GameState.MaxFuel;
        }
    }
}
