using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManagerAI : MonoBehaviour
{
    public GameState gameState;
    public List<GameObject> myFish;

    public FishDataVault dataVault;
    public GameObject fishPrefab;
    public Camera playerCamera;
    public Canvas submarineCanvas;
    public int spawnDelay = 3;
    public int spawnDelayJitter = 2;

    public int spawnPositionMinY = 50;
    public int spawnPositionMaxY = 650;

    public float currentCountDown;

    // Start is called before the first frame update
    void Start()
    {
        myFish = new List<GameObject>();

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
        int startY = Random.Range(spawnPositionMinY, spawnPositionMaxY);

        List<int> fishIDs = dataVault.GetAllFishForDepth(gameState.CurrentDepth);
        //print("Choose one fish from those IDs: " + fishIDs.Count+": "+ System.String.Join(", ", fishIDs.ToArray()));
        if (!(fishIDs.Count == 0))
        {
            int index = Random.Range(0,fishIDs.Count-1);
            //print("Reqesting ID index: " + index);
            int id = fishIDs[index];
            CreateFish(id,startY);
        }
    }

    public void CreateFish(int id, int startY)
    {
        bool reversed = Random.value >= 0.5;
        float startX = Random.Range(-250, -550);

        //print("Spawning fish with ID: " + id);
        GameObject fish = Instantiate(fishPrefab, new Vector3(startX, startY, 0), Quaternion.identity);
        fish.transform.SetParent(submarineCanvas.transform);
        myFish.Add(fish.gameObject);

        FishAI fai = fish.GetComponent<FishAI>();
        fai.id = id;
        fai.myManager = this;
        fai.gameState = gameState;
        fai.startDepth = gameState.CurrentDepth;
        fai.worldX = startX;
        fai.worldY = startY;

        if (reversed)
        {
            fai.reverseDirection = true;
            fai.transform.localScale = new Vector3(fai.transform.localScale.x*-1, fai.transform.localScale.y, fai.transform.localScale.z);
        }
        
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
