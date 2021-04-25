using UnityEngine;

namespace UI.Lamps
{
    public class LeftSpotRoundLamp : MonoBehaviour
    {
        public ShipUiManager manager;

        public GameObject off;
        public GameObject on;

        void FixedUpdate()
        {
            if (manager.GameState.LeftSpotState == GameState.MaschienState.On)
            {
                off.SetActive(false);
                on.SetActive(true);
            }
            else
            {
                off.SetActive(true);
                on.SetActive(false);
            }
        }
    }
}
