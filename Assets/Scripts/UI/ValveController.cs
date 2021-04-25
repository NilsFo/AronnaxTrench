using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
    public class ValveController : MonoBehaviour, IPointerClickHandler
    {
        public AudioSource audioSource;

        public void OnPointerClick(PointerEventData eventData)
        {
            audioSource.Play();
        }
    }
}
