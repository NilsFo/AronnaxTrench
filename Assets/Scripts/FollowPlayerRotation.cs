using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerRotation: MonoBehaviour
{

    public GameState gameState;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(0, gameState.PlayerRotation, 0);
    }
    
    
}
