using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class KameraController : MonoBehaviour, IPointerClickHandler
    {
        public ShipUiManager manager;
        private UnityEngine.UI.Image camImage;

        void Start() {
            camImage = GetComponentInChildren<UnityEngine.UI.Image>();
        }
    
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
                camImage.enabled = false;
            }
            else
            {
                camImage.enabled = true;
            }
        }
    }
}
