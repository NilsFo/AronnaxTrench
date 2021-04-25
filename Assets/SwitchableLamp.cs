using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableLamp : MonoBehaviour
{

    
    public GameState gameState;
    public enum LampPos {
        Front,
        RearLeft,
        RearRight
    }

    public LampPos lampPos;

    // Update is called once per frame
    void Update()
    {
        switch(lampPos) {
            case LampPos.Front:
                GetComponent<Light>().enabled = gameState.MidSpotState == GameState.MaschienState.On;
                break;
            case LampPos.RearLeft:
                GetComponent<Light>().enabled = gameState.LeftSpotState == GameState.MaschienState.On;
                break;
            case LampPos.RearRight:
                GetComponent<Light>().enabled = gameState.RightSpotState == GameState.MaschienState.On;
                break;
            

        }
    }
}
