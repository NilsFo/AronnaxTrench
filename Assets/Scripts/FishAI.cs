using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishAI : MonoBehaviour, IPointerClickHandler
{
    public float movementSeedX;
    public float movementSeedY;
    public FishManagerAI myManager;
    public GameState gameState;

    public Image myImage;
    public FishDataVault dataVault;
    public float aliveTime;
    public int id=0;
    public float minDepth;
    public float maxDepth;
    public int rarity;
    public string fishName = "";

    public float aliveTimeMax = 5;
    public float speedX;
    public float magnitudeY;
    public float jitterY;
    public float magnitudeX;

    private Transform t;
    private float startY = 0;
    private float despawnTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        dataVault.ReadData();
        InitData(id);

        movementSeedX = Random.Range(0, 100);
        movementSeedY = Random.Range(0, 100);
        despawnTimer = 3;
        startY = t.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime = aliveTime + Time.deltaTime;

        if (AllowDespawnTimer())
        {
            despawnTimer = despawnTimer - Time.deltaTime;
            if(despawnTimer < 0)
            {
                RemoveFish();
            }
        }
        else
        {
            despawnTimer = 3;
        }
    }

    void FixedUpdate()
    {
        float x = t.position.x;
        float y = startY;

        float mx = speedX * (Mathf.Sin(aliveTime) + 1) * magnitudeX;
        mx = Mathf.Max(mx, 0.0001f);

        float my = jitterY * Mathf.Sin(magnitudeY + aliveTime);

        t.position = new Vector3(x + mx, y + my, t.position.z);
    }


    public bool IsFishClickState()
    {
        // TODO: Check if camera is equipped here
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Player click: "+fishName);
        List<bool> caughtList = gameState.getCaughtFishIDs();
        bool alreadyCaught = caughtList[id];
        if (!alreadyCaught)
        {
            caughtList[id] = true;
            print("New Catch!!");
        }
    }

    public void InitData(int id)
    {
        string[] data = dataVault.GetAllData(id);

        // Reading data from vault
        this.id = id;
        this.fishName = dataVault.GetName(id);
        this.minDepth = dataVault.GetMinDepth(id);
        this.maxDepth = dataVault.GetMaxDepth(id);
        this.rarity = dataVault.GetRarity(id);
        this.speedX = dataVault.GetSpeedX(id);
        this.jitterY = dataVault.GetJitterY(id);
        this.aliveTimeMax = dataVault.GetAliveTime(id);
        this.magnitudeY= dataVault.GetMagnitudeY(id);
        this.magnitudeX = dataVault.GetMagnitudeX(id);

        // Adding Jitter to data
        this.speedX = this.speedX + Random.Range(this.speedX * -0.2f, this.speedX * 0.2f);
        this.minDepth = this.minDepth + Random.Range(this.minDepth * -0.2f, this.minDepth * 0.2f);
        this.maxDepth = this.maxDepth + Random.Range(this.maxDepth * -0.2f, this.maxDepth * 0.2f);
        this.aliveTimeMax = this.aliveTimeMax + Random.Range(this.aliveTimeMax * -0.2f, this.aliveTimeMax * 0.2f);
        this.magnitudeY = this.magnitudeY + Random.Range(this.magnitudeY * -0.2f, this.magnitudeY * 0.2f);
        this.magnitudeX = this.magnitudeX + Random.Range(this.magnitudeX * -0.2f, this.magnitudeX * 0.2f);
        this.jitterY = this.jitterY + Random.Range(this.jitterY * -0.2f, this.jitterY * 0.2f);

        //Applying Gradient Shift
        //TODO

        // Applying Sprite
        myImage.sprite = dataVault.GetSprite(id);
    }

    public bool IsInPlayerView()
    {
        // TODO
        return true;
    }

    public bool IsInComfortableDepth()
    {
        float depth = GetCurrentDepth();
        return depth >= minDepth && depth <= maxDepth;
    }

    public float GetCurrentDepth()
    {
        // TODO implement
        return 0.1f;
    }

    public bool AllowDespawnTimer()
    {
        if(!IsInComfortableDepth() || aliveTime > aliveTimeMax)
        {
            return !IsInPlayerView();
        }
        return false;
            
    }

    public void RemoveFish()
    {
        myManager.myFish.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

}
