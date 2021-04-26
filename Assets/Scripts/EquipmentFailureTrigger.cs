using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentFailureTrigger : MonoBehaviour
{
    
    public GameState gameState;
    private bool triggered;
    public enum FailureType{
        Tier1, Tier2
    }
    public FailureType failureType;
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !triggered) {
            triggered = true;
            if(failureType == FailureType.Tier1) {
                Debug.Log("Triggering failure Tier 1");
                gameState.FuseOne = GameState.FuseState.Off;
                gameState.FuseThree = GameState.FuseState.Off;
                gameState.FuseFour = GameState.FuseState.Off;
                gameState.FuseSix = GameState.FuseState.Off;

            } else if (failureType == FailureType.Tier2) {
                Debug.Log("Triggering failure Tier 2");
                gameState.FuseOne = GameState.FuseState.Off;
                gameState.FuseTwo = GameState.FuseState.Off;
                gameState.FuseThree = GameState.FuseState.Off;
                gameState.FuseFour = GameState.FuseState.Off;
                gameState.FuseFive = GameState.FuseState.Off;
                gameState.FuseSix = GameState.FuseState.Off;
                gameState.MainFuse = GameState.FuseState.Off;

            }
        }
        
    }
}
