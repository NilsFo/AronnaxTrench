using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ScrollWithPlayerRotation : MonoBehaviour
{
    public GameState gameState;
    
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, gameState.PlayerRotation, 0);
    }
}
