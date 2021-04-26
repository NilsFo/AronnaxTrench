using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace UI
{
    public class ValveController : MonoBehaviour, IPointerClickHandler
    {
        public AudioSource audioSource;

        public ShipUiManager manager;

        private float _maxTime = Mathf.PI * 2;
        private float _timer = 0;
        private float speed = 4f;
        private float offset = 0;

        private float rotation = 60f;

        void Start()
        {
            offset = Random.value;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _maxTime)
            {
                _timer = 0;
            }
      
            if (manager.GameState.GeneralState == GameState.MaschienState.Defective)
            {
                float diff = Mathf.Sin((_timer + offset) * speed);
                
                transform.localRotation = Quaternion.Euler(0f, 0f, (rotation * diff));
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0f,0f,0f);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //audioSource.Play();
        }
    }
}
