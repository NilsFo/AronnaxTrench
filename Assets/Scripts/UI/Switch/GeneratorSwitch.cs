using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Switch
{
    public class GeneratorSwitch : Interactible, IPointerClickHandler
    {
        public ShipUiManager manager;
  
        public GameObject off;
        public GameObject on;

        private float _maxTime = Mathf.PI * 2;
        private float _timer = 0;
        private float speed = 4f;
        
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
            _timer += Time.deltaTime;
            if (_timer > _maxTime)
            {
                _timer = 0;
            }

            if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
            {
                float diff = Mathf.Sin(_timer * speed);
                if (diff > 0f)
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
            else
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
}
