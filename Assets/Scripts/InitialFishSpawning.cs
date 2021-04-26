using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class InitialFishSpawning : MonoBehaviour
{

    public Transform spawnOrigin;
    public FishManagerAI fishManager;

    public int creationCount = 50;
    public int minDepth = -5;
    public int maxDepth = -300;

    // Start is called before the first frame update
    void Start()
    {
        int dist = maxDepth - minDepth;
        int stepSize = dist / creationCount;

        for(int i = 0; i < creationCount; i++)
        {
            float x = spawnOrigin.position.x;
            float y = 0 + stepSize * i + minDepth;
            float z = spawnOrigin.position.z;
            y = y * Random.Range(0.85f,1.15f);
            
            fishManager.SpawnNextFish(new Vector3(x,y,z), y, false);
        }
    }

}
