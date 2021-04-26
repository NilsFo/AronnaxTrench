using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableLamp : MonoBehaviour
{

    
    public GameState gameState;
    public Gradient flickerGradient;
    public enum LampPos {
        Front,
        RearLeft,
        RearRight
    }

    public LampPos lampPos;
    private Light _light;

    private float flickerTimer = 20f;
    private float brightness;

    // Update is called once per frame
    private void Start()
    {
        _light = GetComponent<Light>();
        brightness = _light.intensity;
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
        flickerTimer -= Time.deltaTime;
        if(flickerTimer < 0) {
            flickerTimer = Random.Range(20f, 40f);
        }
        else if(flickerTimer <= 1f) {
            _light.intensity = flickerGradient.Evaluate(1-flickerTimer).a * brightness;
        }
    }
}
