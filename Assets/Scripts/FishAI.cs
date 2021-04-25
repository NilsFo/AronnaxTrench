using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PathCreation;

public class FishAI : MonoBehaviour, IPointerClickHandler
{

    public static readonly bool SELF_CATCH_ENABLED = false;

    public float movementSeedX;
    public float movementSeedY;
    public FishManagerAI myManager;
    public GameState gameState;
    public BillBoard myBillBoard;

    public float StartPosX = -250;
    public float EndPosX = -3753;

    public Image myImage;
    public SpriteRenderer sprite;
    public FishDataVault dataVault;
    public float aliveTime;
    public int id=0;
    public float minDepth;
    public float maxDepth;
    public int rarity;
    public string fishName = "";
    public bool reverseDirection = false;
    public bool applyJitterToSpwawn = false;

    public float aliveTimeMax = 5;
    public float speedX;
    public float magnitudeY;
    public float jitterY;
    public float magnitudeX;

    private Transform t;
    private float startY = 0;
    private float despawnTimer = 3;
    public float startDepth;

    public float worldX;
    public float worldY;
    public float SelfCatchTimer = 3;

    public PathCreator myPath;
    public float dstTravelled;
    public EndOfPathInstruction endOfPathBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        dataVault.ReadData();

        if (id != -1)
        {
            InitData(id);
        }

        movementSeedX = Random.Range(0, 100);
        movementSeedY = Random.Range(0, 100);
        despawnTimer = 3;
        startY = t.position.y;
        StartPosX = -250;
        EndPosX = -3753;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime = aliveTime + Time.deltaTime;

        /*
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
        }*/
    }

    void FixedUpdate()
    {
        // Self Catching
        if (SELF_CATCH_ENABLED)
        {
            SelfCatchTimer = SelfCatchTimer - Time.deltaTime;
            if (SelfCatchTimer < 0)
            {
                Catch();
                RemoveFish();
            }
        }

        // Movement along a path
        float speed = speedX;

        //speed = 0.1f;
        //magnitudeX = 0.1f;
        //magnitudeY = 5;
        //jitterY = 150;
        //TODO why won't this be read??

        float mx = Mathf.Abs(Mathf.Sin(aliveTime)) * magnitudeX;
        float my = Mathf.Sin(aliveTime * magnitudeY) * jitterY;

        dstTravelled += (speed + mx) * Time.deltaTime;

        float transformDistance = dstTravelled;
        if (reverseDirection)
        {
            transformDistance = transformDistance * -1;
        }

        if (myPath != null)
        {
            transform.position = myPath.path.GetPointAtDistance(transformDistance, endOfPathBehaviour);
            //transform.rotation = myPath.path.GetRotationAtDistance(transformDistance, endOfPathBehaviour);
        }
    }


    public bool IsFishClickState()
    {
        // TODO: Check if camera is equipped here
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Player click: "+fishName);
        Catch();
    }

    public void Catch()
    { 
        bool[] caughtList = gameState.CaughtFishIDs;
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
        if (applyJitterToSpwawn)
        {
            this.speedX = this.speedX + Random.Range(this.speedX * -0.2f, this.speedX * 0.2f);
            this.minDepth = this.minDepth + Random.Range(this.minDepth * -0.2f, this.minDepth * 0.2f);
            this.maxDepth = this.maxDepth + Random.Range(this.maxDepth * -0.2f, this.maxDepth * 0.2f);
            this.aliveTimeMax = this.aliveTimeMax + Random.Range(this.aliveTimeMax * -0.2f, this.aliveTimeMax * 0.2f);
            this.magnitudeY = this.magnitudeY + Random.Range(this.magnitudeY * -0.2f, this.magnitudeY * 0.2f);
            this.magnitudeX = this.magnitudeX + Random.Range(this.magnitudeX * -0.2f, this.magnitudeX * 0.2f);
            this.jitterY = this.jitterY + Random.Range(this.jitterY * -0.2f, this.jitterY * 0.2f);
        }
        // print("Fish created: "+fishName+". SpeedX: "+ speedX);

        //Applying Gradient Shift
        //TODO

        // Applying Sprite
        if (myImage != null)
        {
            myImage.sprite = dataVault.GetSprite(id);
        }
        sprite.sprite = dataVault.GetSprite(id);
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
        return gameState.CurrentDepth;
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
