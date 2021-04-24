using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
  //private
  [SerializeField]
  private float depth = 0;
  
  [SerializeField]
  private float maxDepthInM = 10000;
  
  [SerializeField]
  private float maxDivePressure = 1000f;
  
  [SerializeField]
  private float pumpPressure = 20f;
  
  [SerializeField]
  private float targetDivePressure = 0f;
  
  [SerializeField]
  private float currentDivePressure = 0f;
  
  [SerializeField]
  private float interiorPressure = 0f;
  
  [SerializeField]
  private float exteriorPressure = 0f;
  
  
  [SerializeField]
  private  float submarineRotation = 0;
  
  [SerializeField]
  private  float submarineMaxThrust = 20f;
  
  [SerializeField]
  private  float submarineThrust = 0;
  
  
  [SerializeField]
  private float playerRotation = 0;
  
  [SerializeField]
  private float playerRotationSpeed = 0;
  
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
    get => exteriorPressure;
    set => exteriorPressure = value;
  }

  void Update()
  {
    //Rotated Player 
    PlayerRotation = PlayerRotationSpeed * Time.deltaTime + PlayerRotation;
    
    //Submarine Player
    SubmarineRotation = SubmarineThrust * Time.deltaTime;
  }
}
