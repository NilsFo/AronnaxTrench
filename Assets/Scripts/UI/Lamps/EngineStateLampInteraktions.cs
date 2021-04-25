using UnityEngine;

namespace UI.Lamps
{
    public class EngineStateLampInteraktions : MonoBehaviour
    {
        public ShipUiManager manager;

        public GameObject off;
        public GameObject on;
        public GameObject warn;

        void FixedUpdate()
        {
            if (manager.GameState.EngienState == GameState.MaschienState.On)
            {
                off.SetActive(false);
                on.SetActive(true);
                warn.SetActive(false);
            }
            else if (manager.GameState.EngienState == GameState.MaschienState.Off)
            {
                off.SetActive(true);
                on.SetActive(false);
                warn.SetActive(false);
            }
            else
            {
                off.SetActive(false);
                on.SetActive(false);
                warn.SetActive(true);
            }
        }
    }
}
