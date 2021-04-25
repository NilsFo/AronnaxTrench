using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class GeneratorSwitch : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject on;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (manager.GameState.GeneratorState == GameState.MaschienState.Off)
            {
                manager.GameState.GeneratorState = GameState.MaschienState.On;
            }
            else if(manager.GameState.GeneratorState == GameState.MaschienState.On)
            {
                manager.GameState.GeneratorState = GameState.MaschienState.Off;
            }
        }
  
        void FixedUpdate()
        {
            if (manager.GameState.GeneratorState == GameState.MaschienState.On)
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
