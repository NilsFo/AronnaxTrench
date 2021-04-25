using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class LeftSpotSwitch : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject mid;
        public GameObject on;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (manager.GameState.LeftSpotState == GameState.MaschienState.Off)
            {
                manager.GameState.LeftSpotState = GameState.MaschienState.On;
            }
            else
            {
                manager.GameState.LeftSpotState = GameState.MaschienState.Off;
            }
        }
  
        void FixedUpdate()
        {
            if (manager.GameState.LeftSpotState == GameState.MaschienState.On)
            {
                off.SetActive(false);
                on.SetActive(true);
                mid.SetActive(false);
            }
            else if (manager.GameState.LeftSpotState == GameState.MaschienState.Off)
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
