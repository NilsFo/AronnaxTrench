using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpotSwitch : MonoBehaviour, IPointerClickHandler
{
    public ShipUiManager manager;
  
    public GameObject off;
    public GameObject mid;
    public GameObject on;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager.GameState.SpotState == GameState.MaschienState.Off)
        {
            manager.GameState.SpotState = GameState.MaschienState.On;
        }
        else
        {
            manager.GameState.SpotState = GameState.MaschienState.Off;
        }
    }
  
    void FixedUpdate()
    {
        if (manager.GameState.SpotState == GameState.MaschienState.On)
        {
            off.SetActive(false);
            on.SetActive(true);
            mid.SetActive(false);
        }
        else if (manager.GameState.SpotState == GameState.MaschienState.Off)
        {
            off.SetActive(true);
            on.SetActive(false);
            mid.SetActive(false);
        }
        else
        {
            off.SetActive(false);
            on.SetActive(false);
            mid.SetActive(true);
        }
    }
}
