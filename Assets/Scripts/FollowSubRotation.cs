using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubRotation : MonoBehaviour
{
    public GameState gameState;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(0, gameState.SubmarineRotation, 0);
    }
}
