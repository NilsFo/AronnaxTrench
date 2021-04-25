using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class FuseSix : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject on;

        void FixedUpdate()
        {
            if (manager.GameState.FuseSix == GameState.FuseState.On)
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
            if (manager.GameState.FuseSix == GameState.FuseState.Off)
            {
                manager.GameState.FuseSix = GameState.FuseState.On;
            }
            else
            {
                manager.GameState.FuseSix = GameState.FuseState.Off;
            }
        }
    }
}
