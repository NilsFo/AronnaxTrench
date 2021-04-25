using UnityEngine;
using UnityEngine.UI;

namespace UI.Display
{
    public class OTwoTankDispaly : MonoBehaviour
    {
        public ShipUiManager manager;

        public Slider slider;

        void FixedUpdate()
        {
            slider.value = manager.GameState.CurrentOTwo / manager.GameState.MaxOTwo;
        }
    }
}
