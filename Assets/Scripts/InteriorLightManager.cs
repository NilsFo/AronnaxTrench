using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteriorLightManager : MonoBehaviour
{
    public ShipUiManager manager;
    public enum IntLightStatus {
        On,
        Off,
        Flicker,
        Alarm
    }
    private IntLightStatus interiorLightStatus = IntLightStatus.Alarm;
    private float t;

    public Color lightOnColor = Color.white;
    public Color lightOffColor = Color.black;
    public Color environmentColor = Color.white;
    public Gradient flickerGradient;
    public Gradient alarmGradient;
    public float alarmInterval = 3f;

    public List<Image> interiorImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        environmentColor = manager.GameState.GetCurrentEnvironmentColor();
        if(manager.GameState.LightState == GameState.MaschienState.Off)
            InteriorLightStatus = IntLightStatus.Off;
        if(manager.GameState.LightState == GameState.MaschienState.On)
            InteriorLightStatus = IntLightStatus.On;
        if(manager.GameState.LightState == GameState.MaschienState.Warning)
            InteriorLightStatus = IntLightStatus.Alarm;
        if(manager.GameState.PlayState == GameState.GameplayState.End)
            InteriorLightStatus = IntLightStatus.Alarm;
        
        if(interiorLightStatus == IntLightStatus.Alarm) {
            t += Time.deltaTime;
            if(t > alarmInterval) {
                t -= alarmInterval;
            }
            SetLight(alarmGradient.Evaluate(t/alarmInterval));
        } else {
            if(interiorLightStatus == IntLightStatus.On) {
                SetLight(lightOnColor);
            } else if(interiorLightStatus == IntLightStatus.Off) {
                SetLight(lightOffColor);
            }
        }
    }

    public void SetLight(Color color) {
        color = MultiplyColor(color, environmentColor);
        foreach(var img in interiorImages) {
            img.color = color;
        }
    }

    private Color ScreenColor(Color c1, Color c2) {
        return new Color(
            1 - (1 - c1.r)*(1-c2.r),
            1 - (1 - c1.g)*(1-c2.g),
            1 - (1 - c1.b)*(1-c2.b)
        );
    }

    private Color MultiplyColor(Color c1, Color c2) {
        return new Color(
            c1.r * c2.r,
            c1.g * c2.g,
            c1.b * c2.b
        );
    }

    public IntLightStatus InteriorLightStatus {get => interiorLightStatus;  set {
        if(value == interiorLightStatus)
            return;
        if(value == IntLightStatus.Alarm || value == IntLightStatus.Flicker) {
            t = 0;
        } else if(value == IntLightStatus.On) {
            SetLight(lightOnColor);
        } else if(value == IntLightStatus.Off) {
            SetLight(lightOffColor);
        }
        interiorLightStatus = value;
    } }
}
