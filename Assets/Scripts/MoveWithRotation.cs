using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithRotation : MonoBehaviour
{

    public float StartPosX;
    public float EndPosX;

    public GameState gameState;
    
    void FixedUpdate()
    {
        float newValue = (EndPosX - StartPosX)*(gameState.PlayerRotation/360);
        transform.position = new Vector3(StartPosX+newValue, transform.position.y, transform.position.z);
    }
}
