using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FishManagerAI : MonoBehaviour
{
    public GameState gameState;
    public List<GameObject> myFish = new List<GameObject>();
    public List<GameObject> idleFish = new List<GameObject>();

    public FishDataVault dataVault;
    public GameObject fishPrefab;
    public Camera playerCamera;
    public Canvas submarineCanvas;
    public int spawnDelay = 3;
    public int spawnDelayJitter = 2;
    public int maxIdleFishCount = 8;

    public int spawnPositionMinY = 50;
    public int spawnPositionMaxY = 650;

    public float currentCountDown;
    public List<PathCreator> possiblePaths;

    public bool applyJitterToSpwawn = true;

    public List<GameObject> blockingCubes;

    // Start is called before the first frame update
    void Start()
    {
        ResetFishSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.IsPlayerMovingSlowly())
        {
            currentCountDown = currentCountDown - Time.deltaTime;
            if (currentCountDown < 0)
            {
                float currentDepth = gameState.CurrentDepth;
                Vector3 pos = playerCamera.transform.position;
                pos.y = pos.y + 2f * Random.Range(-1.69f, 1.337f);

                SpawnNextFish(pos, currentDepth, true);
                ResetFishSpawnTimer();
            }
        }
        else
        {
            ResetFishSpawnTimer();
        }

        // Remove this later
        if (gameState.IsPlayerMovingVeryFast())
        {
            // print("Slow down! You are very fast!");
        }
    }

    public void SpawnNextFish(Vector3 targetPosition, float depth, bool allowAutoDespawn)
    {
        int startY = Random.Range(spawnPositionMinY, spawnPositionMaxY);

        if (allowAutoDespawn)
        {
            if(idleFish.Count > maxIdleFishCount)
            {
                //print("Player is standing still, but the max idle fish cap has already been reached: "+ maxIdleFishCount);
                return;
            }
        }

        foreach(GameObject blocker in blockingCubes)
        {
            BoxCollider col = blocker.GetComponent<BoxCollider>();
            if(col.bounds.Contains(new Vector3(0, depth, 0)))
            {
                //print("Cannot spawn fish at depth: "+depth+". There's a blocker here!");
                return;
            }
        }

        List<int> fishIDs = dataVault.GetAllFishForDepth(depth);
        //print("Choose one fish from those IDs: " + fishIDs.Count+": "+ System.String.Join(", ", fishIDs.ToArray()));
        if (!(fishIDs.Count == 0))
        {
            int index = Random.Range(0,fishIDs.Count-1);
            //print("Reqesting ID index: " + index);
            int id = fishIDs[index];

            PathCreator path = possiblePaths[Random.Range(0, possiblePaths.Count - 1)];
            path=Instantiate(path);
            path.transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);

            if (applyJitterToSpwawn)
            {
                path = JitterPath(path);
            }

            CreateFish(id,startY,path, allowAutoDespawn);
        }
        else
        {
            print("No spawnable fish for this depth: "+depth);
        }
    }

    public void CreateFish(int id, int startY, PathCreator path, bool allowAutoDespawn)
    {
        bool reversed = Random.value >= 0.5;
        float startX = Random.Range(-250, -550);

        //print("Spawning fish with ID: " + id);
        GameObject fish = Instantiate(fishPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        // fish.transform.SetParent(submarineCanvas.transform);
        myFish.Add(fish);

        if (allowAutoDespawn)
        {
            idleFish.Add(fish);
        }

        FishAI fai = fish.GetComponent<FishAI>();
        fai.id = id;
        fai.myManager = this;
        fai.gameState = gameState;
        fai.startDepth = gameState.CurrentDepth;
        fai.worldX = startX;
        fai.worldY = startY;
        fai.myPath = path;
        fai.endOfPathBehaviour = EndOfPathInstruction.Loop;
        fai.myBillBoard.target = playerCamera.transform;
        fai.dstTravelled = Random.Range(0f, 100f);
        fai.applyJitterToSpwawn = applyJitterToSpwawn;
        fai.allowAutoDespawn = allowAutoDespawn;

        if (reversed)
        {
            fai.reverseDirection = true;
            fai.transform.localScale = new Vector3(fai.transform.localScale.x*-1, fai.transform.localScale.y, fai.transform.localScale.z);
        }
    }

    public PathCreator JitterPath(PathCreator path)
    {
        for(int i=0;i< path.bezierPath.NumPoints; i++)
        {
            Vector3 p = path.bezierPath.GetPoint(i);
            Vector3 np = new Vector3(p.x*Random.Range(0.9f,1.337f), p.y * Random.Range(0.69f, 4.20f), p.z * Random.Range(0.8f, 1.69f));
            path.bezierPath.MovePoint(i, np);
        }
        return path;
    }

    public void ResetFishSpawnTimer()
    {
        var jitter = Random.Range(spawnDelayJitter * -1, spawnDelayJitter);

        currentCountDown = spawnDelay + jitter;
        currentCountDown = Mathf.Max(currentCountDown, 1);
    }

    public int GetSubmarineDepth()
    {
        return 5;
    }

    public List<GameObject> getFishAtDepth(float depth)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < myFish.Count; i++)
        {
            GameObject currentFish = myFish[i];
            FishAI fai = currentFish.GetComponent<FishAI>();
            if (fai != null)
            {
                list.Add(currentFish);
            }
        }

        return list;
    }

    public void deleteAllFish()
    {
        foreach(GameObject fish in myFish)
        {
            Destroy(fish);
        }
        myFish.Clear();
        idleFish.Clear();
    }

}
