using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class KameraController : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (manager.GameState.Camera == GameState.CameraState.disarm)
            {
                manager.GameState.Camera = GameState.CameraState.armed;
            }
        }
        void FixedUpdate()
        {
            if (manager.GameState.Camera == GameState.CameraState.armed)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
