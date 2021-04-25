using UnityEngine;

namespace UI.Lamps
{
    public class MidSpotRoundLamp : MonoBehaviour
    {
        public ShipUiManager manager;

        public GameObject off;
        public GameObject on;

        void FixedUpdate()
        {
            if (manager.GameState.MidSpotState == GameState.MaschienState.On)
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
