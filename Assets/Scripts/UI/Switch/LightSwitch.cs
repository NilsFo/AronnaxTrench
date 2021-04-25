using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitch : MonoBehaviour, IPointerClickHandler 
{
  public ShipUiManager manager;
  
  public GameObject off;
  public GameObject mid;
  public GameObject on;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (manager.GameState.LightState == GameState.MaschienState.Off)
    {
      manager.GameState.LightState = GameState.MaschienState.On;
    }
    else
    {
      manager.GameState.LightState = GameState.MaschienState.Off;
    }
  }
  
  void FixedUpdate()
  {
    if (manager.GameState.LightState == GameState.MaschienState.On)
    {
      off.SetActive(false);
      on.SetActive(true);
      mid.SetActive(false);
    }
    else if (manager.GameState.LightState == GameState.MaschienState.Off)
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
