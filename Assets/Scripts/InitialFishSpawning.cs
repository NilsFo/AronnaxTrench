using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class InitialFishSpawning : MonoBehaviour
{

    public Transform spawnOrigin;
    public FishManagerAI fishManager;

    public int creationCount = 250;
    public int maxDepth = -300;

    // Start is called before the first frame update
    void Start()
    {
        

        for(int i = 0; i < creationCount; i++)
        {
            float x = spawnOrigin.position.x;
            float y = spawnOrigin.position.y - (maxDepth / creationCount) * i;
            float z = spawnOrigin.position.z;
            
            fishManager.SpawnNextFish(new Vector3(x,y,z), y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
