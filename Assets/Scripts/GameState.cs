using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    void Start()
    {

    }


    //private
    [SerializeField]
  private  float depth = 0;
  
  [SerializeField]
  private  float maxDepthInM = 10000;
  
  [SerializeField]
  private  float submarineRotation = 0;
  
  [SerializeField]
  private  float submarineMaxThrust = 20f;
  
  [SerializeField]
  private  float submarineThrust = 0;
  
  [SerializeField]
  private float playerRotation = 0;

    private List<bool> caughtFishIDs = new List<bool>(FishDataVault.FISH_ID_MAX);

    public List<bool> getCaughtFishIDs()
    {
        return caughtFishIDs;
    }

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

  public float CurrentDepth {get; set;}


}
