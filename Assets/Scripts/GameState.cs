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
    Warning,
    Defective
  }
  
  public enum CameraState
  {
    Disarm,
    Armed,
  }

  public enum PlayState
  {
    New,
    Playing,
    End,
  }
  //END Enums
  
  //private
  
  //Tanks 
  [SerializeField]
  private float maxFuel = 10000; //in ml ~ 8 min @ 21 ml/s

  [SerializeField]
  private float currentFuel = 10000; //in ml

  [SerializeField]
  private float maxOTwo = 0;
  
  [SerializeField]
  private float currentOTwo = 0;
  
  [SerializeField]
  private float maxBattery = 240000; //in RF
  
  [SerializeField]
  private float currentBattery = 240000; //in RF
  //END Tanks 
  
  //Kamera
  [SerializeField]
  private CameraState cameraState = 0;
  //END Kamera
  
  //Generator
  [SerializeField]
  private float generatorConsumptionOfFuelPerSecond = 21; //in ml
  
  [SerializeField]
  private float generatorRFOutputPerSecond = 1000; //in RF

  private bool isRFSaturation = true;
  //END Generator
  
  //Sub Position && Movement
  [SerializeField]
  private float depth = 0;

  [SerializeField]
  private float maxDivePressure = 1000f;
  
  [SerializeField]
  private float maxPumpPressure = 20f;
  
  [SerializeField]
  private float pressureDelta = 0f; 
  
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
  private float submarineRotationDampening = 0.2f;
  private float _submarineRotationSpeed = 0f;

  [SerializeField]
  private float playerVelocity = 0;


    //END Sub Position && Movement

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
  
  
  
  //PlayState
  private PlayState playState;

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
  [SerializeField] private FuseState mainFuse = FuseState.On; //All

  [SerializeField] private FuseState fuseOne = FuseState.On; //Pumps
  
  [SerializeField] private FuseState fuseTwo = FuseState.On; //Jets
  
  [SerializeField] private FuseState fuseThree = FuseState.On; //Lamps
  
  [SerializeField] private FuseState fuseFour = FuseState.On; //Spots
  
  [SerializeField] private FuseState fuseFive = FuseState.On; //Life
  
  [SerializeField] private FuseState fuseSix = FuseState.On; //Sonar
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

  public float PressureDelta
  {
    get => pressureDelta;
    set => pressureDelta = value;
  }

  public float InteriorPressure
  {
    get => interiorPressure;
    set => interiorPressure = value;
  }

    public float PlayerVelocity
    {
        get => playerVelocity;
        set => playerVelocity = value;
    }

    public float MaxInteriorPressure => maxDivePressure;

  public float ExteriorPressure
  {
    get => (depth - 20) * exteriorPressureFactor;
  }

  public float MaxExteriorPressure => maxDivePressure;

  public float MaxInteriorPressurePumpPressure => maxInteriorPressurePumpPressure;

  public float MaxPumpPressure {get => maxPumpPressure; set => maxPumpPressure = value;}
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
  public MaschienState LifeSupportState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseFive == FuseState.On)
      {
        return lifeSupportState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState BatteryState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return batteryState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState GeneratorState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return generatorState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && generatorState != value)
      {
        generatorState = value;
      }
    }
  }

  public MaschienState OTwoTankState {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return oTwoTankState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState OTwoInteriorState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return oTwoInteriorState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState RadioState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return radioState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && lightState != value)
      {
        radioState = value;
      }
    }
  }

  public MaschienState EngienState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return engienState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState LightState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseThree == FuseState.On)
      {
        return lightState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && lightState != value)
      {
        lightState = value;
      }
    }
  }
  
  public MaschienState PressureState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        return midSpotState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }
  //END MaschienState

  //Spot Lights
  public MaschienState MidSpotState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseFour == FuseState.On)
      {
        return midSpotState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && midSpotState != value)
      {
        midSpotState = value;
      }
    }
  }

  public MaschienState LeftSpotState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseFour == FuseState.On)
      {
        return leftSpotState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && leftSpotState != value)
      {
        leftSpotState = value;
      }
    }
  }

  public MaschienState RightSpotState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseFour == FuseState.On)
      {
        return rightSpotState;
      }
      else
      {
        return MaschienState.Off;
      }
    }
    set
    {
      if (value != MaschienState.Warning && value != MaschienState.Defective && rightSpotState != value)
      {
        rightSpotState = value;
      }
    }
  }
//END Spot Lights

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
  //End FuseBox

  public Color GetCurrentEnvironmentColor() {
    return envGradient.Evaluate(-depth/380f);
  }
  public Color GetCurrentFogColor() {
    return fogGradient.Evaluate(-depth/380f);
  }

  void Start()
  {
    caughtFishIDs = new bool[FishDataVault.FISH_ID_MAX+1];
    for (int i = 0; i <= FishDataVault.FISH_ID_MAX; i++)
    {
      caughtFishIDs[i] = false;
    }
  }

  void Update()
  {
    //Generate Energy
    if (generatorState == MaschienState.On)
    {
      float fuelConsumption = Time.deltaTime * generatorConsumptionOfFuelPerSecond;
      if (currentFuel < fuelConsumption)
      {
        //wenig Fuel
        generatorState = MaschienState.Warning;
      }
      else
      {
        //genug Fuel 
        currentFuel -= fuelConsumption;
        currentBattery += generatorRFOutputPerSecond * Time.deltaTime;
        if (currentBattery > maxBattery)
        {
          currentBattery = maxBattery;
        }
      }
    }
    
    //Consume Energy
    
    
    
    //Rotated Player 
    PlayerRotation = PlayerRotationSpeed * Time.deltaTime + PlayerRotation;
    
    _submarineRotationSpeed += submarineThrust * Time.deltaTime;
    _submarineRotationSpeed *= (1 - submarineRotationDampening*Time.deltaTime);
    //Submarine Player
    SubmarineRotation += _submarineRotationSpeed * Time.deltaTime;
    
    //Interior Pressuer
    interiorPressure += interiorPressurePumpPressure * Time.deltaTime;

    //Pressure Magic?!
    currentDivePressure += pressureDelta * Time.deltaTime;
    depth += (ExteriorPressure - currentDivePressure) * Time.deltaTime * 0.01f;
    
    // Set fog according to gradient
    var color = GetCurrentFogColor();
    RenderSettings.fogDensity = color.a*0.1f;
    RenderSettings.fogColor = color;
  }

}
