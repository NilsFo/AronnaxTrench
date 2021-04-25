using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{

  public enum MaschienState
  {
    On, 
    Off, 
    Warning
  }
  
  //private
  [SerializeField]
  private float depth = 0;

  [SerializeField]
  private float maxDivePressure = 1000f;
  
  [SerializeField]
  private float pumpPressure = 20f;
  
  [SerializeField]
  private float targetDivePressure = 0f;  // 
  
  [SerializeField]
  private float currentDivePressure = 0f;
  
  [SerializeField]
  private float interiorPressure = 0f;
  
  [SerializeField]
  private float exteriorPressureFactor = 1f;

  [SerializeField]
  private float submarineRotation = 0;
  
  [SerializeField]
  private float submarineMaxThrust = 20f;
  
  [SerializeField] 
  private float submarineThrust = 0;

  [SerializeField]
  private float playerRotation = 0;
  
  [SerializeField]
  private float playerRotationSpeed = 0;

  [SerializeField]
  private bool[] caughtFishIDs;

  [SerializeField]
  private float noiseLevel = 0;
  
  [SerializeField] 
  private MaschienState lifeSupportState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState batteryState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState generatorState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState oTwoTankState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState oTwoInteriorState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState radioState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState engienState = MaschienState.Off;

  [SerializeField] 
  private MaschienState lightState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState spotState = MaschienState.Off;
  
  //public
  public float PlayerRotation
  {
    get => playerRotation;
    set
    {
      if (value >= 360)
      {
        playerRotation = value - 360;
      }
      else if(value < 0)
      {
        playerRotation = value + 360;
      }
      else
      {
        playerRotation = value;
      }
    }
  }

  public float PlayerRotationSpeed
  {
    get => playerRotationSpeed;
    set => playerRotationSpeed = value;
  }
  
  public float CurrentDepth
  {
    get => depth;
    set => depth = value;
  }

  public float SubmarineThrust
  {
    get => submarineThrust;
    set => submarineThrust = value;
  }

  public float SubmarineRotation
  {
    get => submarineRotation;
    set
    {
      if (value >= 360)
      {
        submarineRotation = value - 360;
      }
      else if(value < 0)
      {
        submarineRotation = value + 360;
      }
      else
      {
        submarineRotation = value;
      }
    }
  }

  public float SubmarineMaxThrust => submarineMaxThrust;

  public float MaxDivePressure => maxDivePressure;
  
  public float CurrentDivePressure => currentDivePressure;

  public float TargetDivePressure
  {
    get => targetDivePressure;
    set => targetDivePressure = value;
  }

  public float InteriorPressure
  {
    get => interiorPressure;
    set => interiorPressure = value;
  }

  public float ExteriorPressure
  {
    get => (depth - 20) * exteriorPressureFactor;
  }

  public float NoiseLevel => noiseLevel;

  public bool[] CaughtFishIDs
  {
    get => caughtFishIDs;
    set => caughtFishIDs = value;
  }

  public MaschienState LifeSupportState => lifeSupportState;

  public MaschienState BatteryState => batteryState;

  public MaschienState GeneratorState => generatorState;

  public MaschienState OTwoTankState => oTwoTankState;

  public MaschienState OTwoInteriorState => oTwoInteriorState;

  public MaschienState RadioState => radioState;

  public MaschienState EngienState => engienState;

  public MaschienState LightState
  {
    get => lightState;
    set => lightState = value;
  }

  public MaschienState SpotState
  {
    get => spotState;
    set => spotState = value;
  }
  
  void Start()
  {
    caughtFishIDs = new bool[FishDataVault.FISH_ID_MAX];
    for (int i = 0; i < FishDataVault.FISH_ID_MAX; i++)
    {
      caughtFishIDs[i] = false;
    }
  }

  void Update()
  {
    //Rotated Player 
    PlayerRotation = PlayerRotationSpeed * Time.deltaTime + PlayerRotation;
    
    //Submarine Player
    SubmarineRotation += SubmarineThrust * Time.deltaTime;

    if(currentDivePressure != targetDivePressure) {
        var diveDirection = currentDivePressure < targetDivePressure ? 1:-1;
        currentDivePressure += pumpPressure * diveDirection * Time.deltaTime;
        if(diveDirection > 0 != currentDivePressure < targetDivePressure) {
          currentDivePressure = targetDivePressure;
        }
    }

    depth += (ExteriorPressure - currentDivePressure) * 0.0001f;

    RenderSettings.fogDensity = -depth * 0.0001f + 0.005f;
  }

}
