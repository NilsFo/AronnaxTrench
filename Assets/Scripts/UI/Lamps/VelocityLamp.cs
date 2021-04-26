using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLamp : MonoBehaviour
{
  public ShipUiManager manager;

  public GameObject off;
  public GameObject on;
  public GameObject warn;

  private float _maxTime = Mathf.PI * 2;
  private float _timer = 0;
  private float speed = 4f;

  void FixedUpdate()
  {
    _timer += Time.deltaTime;
    if (_timer > _maxTime)
    {
      _timer = 0;
    }

    if (manager.GameState.PlayerVelocityState == GameState.MaschienState.On)
    {
      off.SetActive(false);
      on.SetActive(true);
      warn.SetActive(false);
    }
    else if (manager.GameState.PlayerVelocityState == GameState.MaschienState.Off)
    {
      off.SetActive(true);
      on.SetActive(false);
      warn.SetActive(false);
    }
    else if (manager.GameState.PlayerVelocityState == GameState.MaschienState.Defective)
    {
      if (Mathf.Sin(_timer * speed) > 0)
      {
        off.SetActive(true);
        warn.SetActive(false);
      }
      else
      {
        off.SetActive(false);
        warn.SetActive(true);
      }

      on.SetActive(false);
    }
    else
    {
      off.SetActive(false);
      on.SetActive(false);
      warn.SetActive(true);
    }
  }
}