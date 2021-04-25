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
        float newValue = (EndPosX - StartPosX)*(gameState.PlayerRotation/360f);
        Debug.Log(newValue);
        transform.localPosition = new Vector3(newValue, transform.localPosition.y, transform.localPosition.z);
    }
}
