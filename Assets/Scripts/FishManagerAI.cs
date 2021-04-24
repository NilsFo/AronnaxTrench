using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManagerAI : MonoBehaviour
{

    public GameObject fishPrefab;
    public Camera playerCamera;
    public Canvas submarineCanvas;
    public int spawnDelay = 3;
    public int spawnDelayJitter = 2;

    public float currentCountDown;

    // Start is called before the first frame update
    void Start()
    {
        InitFishSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        currentCountDown = currentCountDown - Time.deltaTime;
 
        if(currentCountDown < 0)
        {
            SpawnNextFish();
            InitFishSpawn();
        }
    }

    public void SpawnNextFish()
    {
        int startY = Random.Range(50, 350);
        GameObject fish = Instantiate(fishPrefab, new Vector3(0, startY, 0), Quaternion.identity);

        fish.transform.SetParent(submarineCanvas.transform);
        FishAI fai = fish.GetComponent<FishAI>();

        int id = Random.Range(0, 3);
        fai.id = id;
    }

    public void InitFishSpawn()
    {
        var jitter = Random.Range(spawnDelayJitter * -1, spawnDelayJitter);

        currentCountDown = spawnDelay + jitter;
        currentCountDown = Mathf.Max(currentCountDown, 1);
    }

    public int GetSubmarineDepth()
    {
        return 5;
    }
}
