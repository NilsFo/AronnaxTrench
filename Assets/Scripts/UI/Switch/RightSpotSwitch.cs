using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightSpotSwitch : Interactible, IPointerClickHandler
{
    public ShipUiManager manager;
  
    public GameObject off;
    public GameObject mid;
    public GameObject on;
    
    public static bool isOn = true;
    
    private float _maxTime = Mathf.PI * 2;
    private float _timer = 0;
    private float speed = 4f;
    
    void Start()
    {
        if (manager.GameState.MidSpotState == GameState.MaschienState.On)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;
        if (!isOn)
        {
            FindObjectOfType<ambientSoundController>()?.PlayClick(0.9f);
        }
        else
        {
            FindObjectOfType<ambientSoundController>()?.PlayClick(1.1f);
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
            if (diff > 0.33f)
            {
                off.SetActive(true);
                on.SetActive(false);
                mid.SetActive(false);
            }
            else if (diff < -0.33f)
            {
                off.SetActive(false);
                on.SetActive(true);
                mid.SetActive(false);
            }
            else
            {
                off.SetActive(false);
                on.SetActive(false);
                mid.SetActive(true);
            }
        }
        else
        {
            if (isOn)
            {
                off.SetActive(false);
                on.SetActive(true);
                mid.SetActive(false);
                manager.GameState.RightSpotState = GameState.MaschienState.On;
            }
            else
            {
                off.SetActive(true);
                on.SetActive(false);
                mid.SetActive(false);
                manager.GameState.RightSpotState = GameState.MaschienState.Off;
            }
        }
    }
}
