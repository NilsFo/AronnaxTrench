using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class FuseFour : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject on;

        void FixedUpdate()
        {
            if (manager.GameState.FuseFour == GameState.FuseState.On)
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
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (manager.GameState.FuseFour == GameState.FuseState.Off)
            {
                manager.GameState.FuseFour = GameState.FuseState.On;
            }
            else
            {
                manager.GameState.FuseFour = GameState.FuseState.Off;
            }
        }
    }
}
