using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{

  //Enums
  public enum FuseState
  {
    On,
    Off
  }
  
  public enum MaschienState
  {
    On, 
    Off, 
    Warning
  }
  
  public enum CameraState
  {
    disarm,
    armed,
  }
  //END Enums
  
  //private
  
  //Tanks 
  [SerializeField]
  private float maxFuel = 0;

  [SerializeField]
  private float currentFuel = 0;

  [SerializeField]
  private float maxOTwo = 0;
  
  [SerializeField]
  private float currentOTwo = 0;
  
  [SerializeField]
  private float maxBattery = 0;
  
  [SerializeField]
  private float currentBattery = 0;
  //END Tanks
  
  //Kamera
  [SerializeField]
  private CameraState cameraState = 0;
  //END Kamera
  
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
  private float maxInteriorPressurePumpPressure = 20f;
  
  [SerializeField]
  private float interiorPressurePumpPressure = 0f;
  
  [SerializeField]
  private float maxPressureDifference = 250f;
  
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
  public Gradient fogGradient;
  [SerializeField]
  public Gradient envGradient;
  
  //MaschienState Dispaly && Warning
  
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
  private MaschienState midSpotState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState leftSpotState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState rightSpotState = MaschienState.Off;
  
  [SerializeField] 
  private MaschienState pressureState = MaschienState.Off;

  //FuseBox
  [SerializeField] private FuseState mainFuse = FuseState.On;
  
  [SerializeField] private FuseState fuseOne = FuseState.On;
  
  [SerializeField] private FuseState fuseTwo = FuseState.On;
  
  [SerializeField] private FuseState fuseThree = FuseState.On;
  
  [SerializeField] private FuseState fuseFour = FuseState.On;
  
  [SerializeField] private FuseState fuseFive = FuseState.On;
  
  [SerializeField] private FuseState fuseSix = FuseState.On;
  //End private
  
  //Public
  //Public Tanks
  public float MaxFuel => maxFuel;

  public float CurrentFuel => currentFuel;

  public float MaxOTwo => maxOTwo;

  public float CurrentOTwo => currentOTwo;

  public float MaxBattery => maxBattery;

  public float CurrentBattery => currentBattery;
  //End Public Tanks

  public CameraState Camera
  {
    get => cameraState;
    set => cameraState = value;
  }

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

  public float MaxInteriorPressure => maxDivePressure;

  public float ExteriorPressure
  {
    get => (depth - 20) * exteriorPressureFactor;
  }

  public float MaxExteriorPressure => maxDivePressure;

  public float MaxInteriorPressurePumpPressure => maxInteriorPressurePumpPressure;

  public float InteriorPressurePumpPressure
  {
    get => interiorPressurePumpPressure;
    set => interiorPressurePumpPressure = value;
  }

  public float HullIntegrity => (Mathf.Abs(interiorPressure - ExteriorPressure) / maxPressureDifference);

  public float NoiseLevel => noiseLevel;

  public bool[] CaughtFishIDs
  {
    get => caughtFishIDs;
    set => caughtFishIDs = value;
  }

  //MaschienState
  
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

  //Spot Lights
  public MaschienState MidSpotState
  {
    get => midSpotState;
    set => midSpotState = value;
  }

  public MaschienState LeftSpotState
  {
    get => leftSpotState;
    set => leftSpotState = value;
  }

  public MaschienState RightSpotState
  {
    get => rightSpotState;
    set => rightSpotState = value;
  }

  public MaschienState PressureState
  {
    get => pressureState;
    set => pressureState = value;
  }

  //FuseBox
  public FuseState MainFuse
  {
    get => mainFuse;
    set => mainFuse = value;
  }

  public FuseState FuseOne
  {
    get => fuseOne;
    set => fuseOne = value;
  }

  public FuseState FuseTwo
  {
    get => fuseTwo;
    set => fuseTwo = value;
  }

  public FuseState FuseThree
  {
    get => fuseThree;
    set => fuseThree = value;
  }

  public FuseState FuseFour
  {
    get => fuseFour;
    set => fuseFour = value;
  }

  public FuseState FuseFive
  {
    get => fuseFive;
    set => fuseFive = value;
  }

  public FuseState FuseSix
  {
    get => fuseSix;
    set => fuseSix = value;
  }

  public Color GetCurrentEnvironmentColor() {
    return envGradient.Evaluate(-depth/380f);
  }
  public Color GetCurrentFogColor() {
    return fogGradient.Evaluate(-depth/380f);
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
    
    //Interior Pressuer
    interiorPressure += interiorPressurePumpPressure * Time.deltaTime;

    if(currentDivePressure != targetDivePressure) {
        var diveDirection = currentDivePressure < targetDivePressure ? 1:-1;
        currentDivePressure += pumpPressure * diveDirection * Time.deltaTime;
        if(diveDirection > 0 != currentDivePressure < targetDivePressure) {
          currentDivePressure = targetDivePressure;
        }
    }

    depth += (ExteriorPressure - currentDivePressure) * Time.deltaTime * 0.01f;


    // Set fog according to gradient
    var color = GetCurrentFogColor();
    RenderSettings.fogDensity = color.a*0.1f;
    RenderSettings.fogColor = color;
  }

}
