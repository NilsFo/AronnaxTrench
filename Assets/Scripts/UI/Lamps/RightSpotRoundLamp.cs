using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSpotRoundLamp : MonoBehaviour
{
    public ShipUiManager manager;

    public GameObject off;
    public GameObject on;

    void FixedUpdate()
    {
        if (manager.GameState.RightSpotState == GameState.MaschienState.On)
        {
            off.SetActive(false);
            on.SetActive(true);
        }
        else
        {
            off.SetActive(true);
            on.SetActive(false);
        }
    }
}
