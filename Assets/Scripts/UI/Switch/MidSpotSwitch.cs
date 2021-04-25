using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class MidSpotSwitch : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject mid;
        public GameObject on;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (manager.GameState.MidSpotState == GameState.MaschienState.Off)
            {
                manager.GameState.MidSpotState = GameState.MaschienState.On;
            }
            else
            {
                manager.GameState.MidSpotState = GameState.MaschienState.Off;
            }
        }
  
        void FixedUpdate()
        {
            if (manager.GameState.MidSpotState == GameState.MaschienState.On)
            {
                off.SetActive(false);
                on.SetActive(true);
                mid.SetActive(false);
            }
            else if (manager.GameState.MidSpotState == GameState.MaschienState.Off)
            {
                off.SetActive(true);
                on.SetActive(false);
                mid.SetActive(false);
            }
            else
            {
                off.SetActive(false);
                on.SetActive(false);
                mid.SetActive(true);
            }
        }
    }
}
