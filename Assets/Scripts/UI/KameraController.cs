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
            if (manager.GameState.Camera == GameState.CameraState.Disarm)
            {
                manager.GameState.Camera = GameState.CameraState.Armed;
            }
        }
        void FixedUpdate()
        {
            if (manager.GameState.Camera == GameState.CameraState.Armed)
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
