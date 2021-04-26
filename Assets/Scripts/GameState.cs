using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

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

  public enum GameplayState
  {
    New,
    Playing,
    End,
    Gameover
  }
  //END Enums

  //private

  //PlayState
  [Header("PlayState")] [SerializeField] private GameplayState playState;

  public bool tookFotoOfMonster = false;
  //Kamera
  [Header("Kamera")] [SerializeField] private CameraState cameraState = 0;

  //Tanks
  [Header("Tanks")] [SerializeField] private float maxFuel = 10000; //in ml ~ 8 min @ 21 ml/s

  [SerializeField] private float currentFuel = 10000; //in ml

  [SerializeField] private float maxBattery = 240000; //in RF

  [SerializeField] private float currentBattery = 240000; //in RF
  //END Tanks 

  //Generator
  [Header("Generator")] [SerializeField] private float generatorConsumptionOfFuelPerSecond = 21; //in ml

  [SerializeField] private float generatorRFOutputPerSecond = 1000; //in RF

  [SerializeField] private float totalConsumptionAlL = 1600;

  private bool isRFSaturation = true;
  //END Generator

  //Sub Position && Movement
  [Header("Submarine Movement")] [SerializeField]
  private float depth = 0;

  [SerializeField] private float maxDivePressure = 1000f;

  [SerializeField] private float maxPumpPressure = 20f;

  [SerializeField] public float pressureDelta = 0f;

  [SerializeField] private float currentDivePressure = 0f;

  [SerializeField] private float interiorPressure = 0f;

  [SerializeField] private float maxInteriorPressurePumpPressure = 20f;

  [SerializeField] private float interiorPressurePumpPressure = 0f;

  [SerializeField] private float maxPressureDifference = 250f;

  [SerializeField] private float exteriorPressureFactor = 1f;

  [SerializeField] private float submarineRotation = 0;

  [SerializeField] private float submarineMaxThrust = 20f;

  [SerializeField] private float submarineThrust = 0;

  [SerializeField] private float submarineRotationDampening = 0.2f;
  private float _submarineRotationSpeed = 0f;

  [SerializeField] private float playerVelocity = 0;
  //END Sub Position && Movement

  [Header("Player")] [SerializeField] private float playerRotation = 0;

  [SerializeField] private float playerRotationSpeed = 0;

  [Header("Fish")] [SerializeField] private bool[] caughtFishIDs;

  [Header("Enviomnet")] [SerializeField] public Gradient fogGradient;
  [SerializeField] public Gradient envGradient;

  [Header("Carbon")] [SerializeField] private float currentCarbon = 0;

  [SerializeField] private float maxCarbon = 12000;

  [SerializeField] private float carbonPerSec = 100;

  //MaschienState Dispaly && Warning
  [Header("Dispaly && Warning")] [SerializeField]
  private MaschienState lifeSupportState = MaschienState.On;

  [SerializeField] private MaschienState batteryState = MaschienState.On;

  [SerializeField] private MaschienState generatorState = MaschienState.On;

  [SerializeField] private MaschienState oTwoTankState = MaschienState.On;

  [SerializeField] private MaschienState oTwoInteriorState = MaschienState.On;

  [SerializeField] private MaschienState radioState = MaschienState.On;

  [SerializeField] private MaschienState engienState = MaschienState.Off;

  [SerializeField] private MaschienState lightState = MaschienState.On;

  [SerializeField] private MaschienState midSpotState = MaschienState.Off;

  [SerializeField] private MaschienState leftSpotState = MaschienState.Off;

  [SerializeField] private MaschienState rightSpotState = MaschienState.Off;

  [SerializeField] private MaschienState pressureState = MaschienState.Off;

  //FuseBox
  [Header("FuseBox")] [SerializeField] private FuseState mainFuse = FuseState.On; //All

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

  public float MaxOTwo
  {
    get { return maxDivePressure; }
  }

  public float CurrentOTwo
  {
    get { return maxDivePressure - currentDivePressure; }
  }

  public float MaxBattery => maxBattery;

  public float CurrentBattery => currentBattery;
  //End Public Tanks

  public CameraState Camera
  {
    get => cameraState;
    set => cameraState = value;
  }

  public MaschienState GeneralState
  {
    get
    {
      if (playState == GameplayState.End)
      {
        return MaschienState.Defective;
      }
      return MaschienState.On;
    }
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
      else if (value < 0)
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
      else if (value < 0)
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

  public MaschienState PlayerVelocityState
  {
    get
    {
      float diff = Mathf.Abs((ExteriorPressure - currentDivePressure) * Time.deltaTime);
      if (diff > 6f)
      {
        return MaschienState.Defective;
      }
      if (diff > 3f)
      {
        return MaschienState.Warning;
      }
      return MaschienState.On;
    }
  }

  public float PlayerVelocity
  {
    get => playerVelocity;
    set => playerVelocity = value;
  }

  public bool IsPlayerMovingSlowly()
  {
    return Mathf.Abs(PlayerVelocity) <= 2f;
  }

  public bool IsPlayerMovingVeryFast()
  {
    return Mathf.Abs(PlayerVelocity) >= 20f;
  }

  public float MaxInteriorPressure => maxDivePressure;

  public float ExteriorPressure
  {
    get => (depth - 20) * exteriorPressureFactor;
  }

  public float MaxExteriorPressure => maxDivePressure;

  public float MaxInteriorPressurePumpPressure => maxInteriorPressurePumpPressure;

  public float MaxPumpPressure
  {
    get => maxPumpPressure;
    set => maxPumpPressure = value;
  }

  public float InteriorPressurePumpPressure
  {
    get => interiorPressurePumpPressure;
    set
    {
      if (Mathf.Abs(value) < maxInteriorPressurePumpPressure)
      {
        interiorPressurePumpPressure = value;
      }
    }
  }

  public float HullIntegrity => (Mathf.Abs(interiorPressure - ExteriorPressure) / maxPressureDifference);

  public float IsSuffocation => (Mathf.Abs(currentCarbon / maxCarbon));

  public float IsCharged => (Mathf.Abs(currentBattery / maxBattery));

  public float IsOTwoEmpty => (Mathf.Abs(CurrentOTwo / MaxOTwo));

  public bool[] CaughtFishIDs
  {
    get => caughtFishIDs;
    set => caughtFishIDs = value;
  }

  //Visible MaschienState
  public MaschienState LifeSupportState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        if (fuseFive == FuseState.On && isRFSaturation)
        {
          return lifeSupportState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
    }
  }

  public MaschienState BatteryState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        if (isRFSaturation)
        {
          return batteryState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
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

  public MaschienState OTwoTankState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        if (isRFSaturation)
        {
          return oTwoTankState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
    }
  }

  public MaschienState OTwoInteriorState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        if (isRFSaturation)
        {
          return oTwoInteriorState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
    }
  }

  public MaschienState RadioState
  {
    get
    {
      if (mainFuse == FuseState.On)
      {
        if (isRFSaturation)
        {
          return radioState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
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
        if (isRFSaturation)
        {
          return engienState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
    }
  }

  public MaschienState LightState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseThree == FuseState.On && isRFSaturation)
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
        if (isRFSaturation)
        {
          return pressureState;
        }

        return MaschienState.Warning;
      }

      return MaschienState.Off;
    }
  }

  public MaschienState PumpState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseOne == FuseState.On && isRFSaturation)
      {
        return MaschienState.On;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState JetState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseTwo == FuseState.On && isRFSaturation)
      {
        return MaschienState.On;
      }
      else
      {
        return MaschienState.Off;
      }
    }
  }

  public MaschienState SonarState
  {
    get
    {
      if (mainFuse == FuseState.On && fuseSix == FuseState.On && isRFSaturation)
      {
        return MaschienState.On;
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
      if (mainFuse == FuseState.On && fuseFour == FuseState.On && isRFSaturation)
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
      if (mainFuse == FuseState.On && fuseFour == FuseState.On && isRFSaturation)
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
      if (mainFuse == FuseState.On && fuseFour == FuseState.On && isRFSaturation)
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

  public GameplayState PlayState
  {
    get => playState;
    set => playState = value;
  }
  //End FuseBox

  public Color GetCurrentEnvironmentColor()
  {
    return envGradient.Evaluate(-depth / 380f);
  }

  public Color GetCurrentFogColor()
  {
    return fogGradient.Evaluate(-depth / 380f);
  }

  void Start()
  {
    caughtFishIDs = new bool[FishDataVault.FISH_ID_MAX + 1];
    for (int i = 0; i <= FishDataVault.FISH_ID_MAX; i++)
    {
      caughtFishIDs[i] = false;
    }
  }

  void Update()
  {
        // Pressing ESC

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(playState != GameplayState.End)
            {
                SceneManager.LoadScene("MainMenu");
                return;
            }
        }

        if (playState == GameplayState.New)
    {
      playState = GameplayState.Playing;
    }
    else if (playState == GameplayState.Playing)
    {
      //Generate Energy
      if (GeneratorState == MaschienState.On)
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
      int currentPowerUnits = 0;
      if (mainFuse == FuseState.On)
      {
        float diffPressure = Mathf.Abs(0 - interiorPressurePumpPressure);
        if (fuseOne == FuseState.On && diffPressure > 0)
        {
          currentPowerUnits += 10;
        }

        float diffThrust = Mathf.Abs(0 - submarineThrust);
        if (fuseTwo == FuseState.On && diffPressure > 0)
        {
          currentPowerUnits += 10;
        }

        if (fuseThree == FuseState.On && lightState != MaschienState.Off)
        {
          currentPowerUnits += 2;
        }

        if (fuseFour == FuseState.On)
        {
          if (leftSpotState != MaschienState.Off)
          {
            currentPowerUnits += 3;
          }

          if (midSpotState != MaschienState.Off)
          {
            currentPowerUnits += 3;
          }

          if (rightSpotState != MaschienState.Off)
          {
            currentPowerUnits += 3;
          }
        }

        if (fuseFive == FuseState.On)
        {
          currentPowerUnits += 6;
        }

        if (fuseSix == FuseState.On)
        {
          currentPowerUnits += 6;
        }
      }

      float currentPowerInRfPerTick = (totalConsumptionAlL * (currentPowerUnits / 43f)) * Time.deltaTime;

      if (currentPowerInRfPerTick < currentBattery)
      {
        isRFSaturation = true;
        currentBattery -= currentPowerInRfPerTick;
      }
      else
      {
        isRFSaturation = false;
        currentBattery = 0;
      }
      //END Consume Energy

      //BEGIN Carbon
      if (LifeSupportState == MaschienState.On)
      {
        currentCarbon -= 5*carbonPerSec * Time.deltaTime;
        currentCarbon = Mathf.Max(currentCarbon, 0);
      }
      else
      {
        currentCarbon += carbonPerSec * Time.deltaTime;
      }
      //END Carbon

      //BEGIN LifeSupport
      if (IsSuffocation >= 1f)
      {
        // Death by suffocation
        FindObjectOfType<RadioManager>()?.RadioMessage("Your oxygen levels are too low! We are pulling you back out!", 7.0f);

        PlayState = GameplayState.Gameover;
      }
      else if (IsSuffocation >= 0.5f)
      {
        oTwoInteriorState = MaschienState.Defective;
      }
      else if (IsSuffocation >= 0.2f)
      {
        oTwoInteriorState = MaschienState.Warning;
      }
      else
      {
        oTwoInteriorState = MaschienState.On;
      }
      //END LifeSupport

      //Moving Submarine
      if (JetState != MaschienState.Off)
      {
        _submarineRotationSpeed += submarineThrust * Time.deltaTime;
      }

      _submarineRotationSpeed *= (1 - submarineRotationDampening * Time.deltaTime);
      //Submarine Player
      SubmarineRotation += _submarineRotationSpeed * Time.deltaTime;

      if (PumpState != MaschienState.Off)
      {
        //Interior Pressuer
        interiorPressure += interiorPressurePumpPressure * Time.deltaTime;
        if (interiorPressure < 0)
        {
          interiorPressure = 0;
        }
        else if (interiorPressure > maxDivePressure)
        {
          interiorPressure = maxDivePressure;
        }

        currentDivePressure += pressureDelta * Time.deltaTime;
      }

      //Pressure Magic?!
      depth += (ExteriorPressure - currentDivePressure) * Time.deltaTime * 0.01f;
      if (depth > 0)
      {
        depth = 0;
      }
      //END Moving Submarine

      //BEGIN Warning lamps
      if (HullIntegrity >= 1f)
      {
        // Death by Crushing
        FindObjectOfType<RadioManager>()?.RadioMessage("Your pressure sensors are going crazy! We are pulling you back out!", 7.0f);
        FindObjectOfType<ambientSoundController>().PlayGlassCrack();
        FindObjectOfType<ambientSoundController>().PlayMetalHitSound();
        PlayState = GameplayState.Gameover;
      }
      else if (HullIntegrity > 0.5f)
      {
        // Alarm
        pressureState = MaschienState.Defective;
      }
      else if (HullIntegrity > 0.25)
      {
        pressureState = MaschienState.Warning;
      }
      else
      {
        pressureState = MaschienState.On;
      }


      if (IsCharged <= 0.2f)
      {
        batteryState = MaschienState.Warning;
      }
      else
      {
        batteryState = MaschienState.On;
      }
      if (IsOTwoEmpty <= 0f)
      {
        oTwoTankState = MaschienState.Defective;
      }
      else if (IsOTwoEmpty <= 0.5f)
      {
        oTwoTankState = MaschienState.Warning;
      }
      else
      {
        oTwoTankState = MaschienState.On;
      }
      //END Warning Lamps
    }
    else if(PlayState == GameplayState.End)
    {
      //TODO Fuck UP State
      LightState = MaschienState.Warning;
    }
    else if (PlayState == GameplayState.Gameover)
    {
      currentDivePressure-=200*Time.deltaTime;
      depth += (ExteriorPressure - currentDivePressure) * Time.deltaTime * 0.01f;

      _submarineRotationSpeed *= (1 - submarineRotationDampening * Time.deltaTime);
      SubmarineRotation += _submarineRotationSpeed * Time.deltaTime;
      //TODO Back
    }

    //Rotated Player 
    PlayerRotation = PlayerRotationSpeed * Time.deltaTime + PlayerRotation;

    // Set fog according to gradient
    var color = GetCurrentFogColor();
    RenderSettings.fogDensity = color.a * 0.1f;
    RenderSettings.fogColor = color;
  }
}
