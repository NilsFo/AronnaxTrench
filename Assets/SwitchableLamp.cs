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
    private Light _light;

    // Update is called once per frame
    private void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        switch(lampPos) {
            case LampPos.Front:
                _light.enabled = gameState.MidSpotState == GameState.MaschienState.On;
                break;
            case LampPos.RearLeft:
                _light.enabled = gameState.LeftSpotState == GameState.MaschienState.On;
                break;
            case LampPos.RearRight:
                _light.enabled = gameState.RightSpotState == GameState.MaschienState.On;
                break;
            

        }
    }
}
