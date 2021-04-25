using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithRotation : MonoBehaviour
{

    public float StartPosX;
    public float EndPosX;
    
    public ShipUiManager manager;
    
    void FixedUpdate()
    {
        float newValue = (EndPosX - StartPosX)*(gameState.PlayerRotation/360f);
        transform.localPosition = new Vector3(newValue, transform.localPosition.y, transform.localPosition.z);
    }
}
