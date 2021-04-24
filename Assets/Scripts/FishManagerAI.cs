using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class FishManagerAI : MonoBehaviour
{

    public FishDataVault dataVault;
    public GameObject fishPrefab;
    public Camera playerCamera;
    public Canvas submarineCanvas;
    public int spawnDelay = 3;
    public int spawnDelayJitter = 2;
    public FloatVariable currentDepth;

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

        List<int> fishIDs = dataVault.GetAllFishForDepth(currentDepth.Value);
        //print("Choose one fish from those IDs: " + fishIDs.Count+": "+ System.String.Join(", ", fishIDs.ToArray()));
        if (!(fishIDs.Count == 0))
        {
            int index = Random.Range(0,fishIDs.Count-1);
            //print("Reqesting ID index: " + index);
            int id = fishIDs[index];
            CreateFish(id,startY);
        }
    }

    public void CreateFish(int id, int yPosition)
    {
        //print("Spawning fish with ID: " + id);
        GameObject fish = Instantiate(fishPrefab, new Vector3(0, yPosition, 0), Quaternion.identity);
        fish.transform.SetParent(submarineCanvas.transform);
        FishAI fai = fish.GetComponent<FishAI>();
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
