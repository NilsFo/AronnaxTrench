using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubRotation : MonoBehaviour
{
    public GameState gameState;

    private float lastKnownDepth = 0f;

    private void Start()
    {
        lastKnownDepth = gameState.CurrentDepth;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(0, gameState.SubmarineRotation, 0);
        transform.position = new Vector3(0,gameState.CurrentDepth, 0);

        float currentDepth = gameState.CurrentDepth;
        gameState.PlayerVelocity = (currentDepth - lastKnownDepth) * 100;
        lastKnownDepth = currentDepth;
        // print("Player velocity: " + gameState.PlayerVelocity);
    }
}
