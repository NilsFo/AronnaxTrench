using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Slieder
{
    public class InteriorPerssureSilder : MonoBehaviour
    {
        public ShipUiManager manager;
        public Slider slider;
        public int index;

        public static float myValue;
        public static int changeIndex;

        void Start()
        {
            slider.value = 0.5f;
        }

        void OnEnable () 
        {
            slider.onValueChanged.AddListener(ChangeValue);
            ChangeValue(slider.value);
        }
        void OnDisable()
        {
            slider.onValueChanged.RemoveAllListeners();
        }
  
        void ChangeValue(float value)
        {
            myValue = value;
            changeIndex = index;
            manager.GameState.InteriorPressurePumpPressure = manager.GameState.MaxInteriorPressurePumpPressure * ((value - 0.5f) * 2) ;
        }

        void Update()
        {
            float diff = Mathf.Abs(slider.value - myValue);
            if (changeIndex != index && diff > 0)
            {
                slider.value = myValue;
            }
        }
    }
}
