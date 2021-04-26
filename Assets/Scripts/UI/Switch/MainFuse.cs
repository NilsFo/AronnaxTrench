using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class MainFuse : Interactible, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject on;

        void FixedUpdate()
        {
            if (manager.GameState.MainFuse == GameState.FuseState.On)
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
            if (manager.GameState.MainFuse == GameState.FuseState.Off)
            {
                manager.GameState.MainFuse = GameState.FuseState.On;
            }
            else
            {
                manager.GameState.MainFuse = GameState.FuseState.Off;
            }
        }
    }
}
