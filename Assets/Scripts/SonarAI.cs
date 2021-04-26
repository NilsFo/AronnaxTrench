using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarAI : MonoBehaviour
{

    public Image myBlep;
    public float rangeY = 30;
    public float rangeX = 120;

    public FishManagerAI fishManager;
    public RectTransform rect;
    public GameState gameState;
    public Camera playerCamera;
    public List<Image> currentBleps;
    public List<Image> blepCache;
    private float chaosTimer = 0;
    private bool chaosInvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        currentBleps = new List<Image>();
        blepCache = new List<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (IsFinale())
        {
            if (!chaosInvoked)
            {
                InvokeRepeating("InvokeChaos", 0, 1.337f);
                chaosInvoked = true;
            }
        }
        else if (!IsFunctioning())
        {
            clearBleps();
        }

        else
        {
            clearBleps();
            UpdateNormally();
        }
    }

    private void InvokeChaos()
    {
        clearBleps();
        UpdateChaos();
    }

    private void UpdateChaos()
    {
        print("chaos");
        chaosTimer = chaosTimer + Time.deltaTime;

        for (int i = 0; i < Random.Range(3,8); i++)
        {
            Image newBlep = getNextBlep(i);

            float w = rect.rect.width;
            float x = Random.Range(w, w * -1) * 0.3f;
            float y = Random.Range(w, w * -1) * 0.3f;
            newBlep.transform.localPosition = new Vector3(x, y, this.transform.position.z);

            var tempColor = newBlep.color;
            tempColor.a = 1f;
            newBlep.color = tempColor;
            currentBleps.Add(newBlep);
        }
    }

    private void clearBleps()
    {
        // Deleting old bleps
        foreach (Image i in currentBleps)
        {
            i.enabled = false;
        }
        currentBleps.Clear();
    }

    
    private void UpdateNormally() { 
        // Looking for nearby fish
        float depth = gameState.CurrentDepth;
        List<GameObject> fish = GetNearbyFish(depth);
        //print("Nearby Fish: " + fish.Count);

        for(int i=0;i<fish.Count;i++)
        {
            GameObject currentFish = fish[i];
            Vector3 localFish = playerCamera.transform.worldToLocalMatrix.MultiplyVector(currentFish.transform.position);
            float fishX = currentFish.transform.position.x - playerCamera.transform.position.x;
            float fishZ = currentFish.transform.position.z - playerCamera.transform.position.z;

            float sonarX = localFish.x / rangeX * (rect.rect.width * 0.75f);
            float sonarZ = localFish.z / rangeX * (rect.rect.height * 0.75f);

            float distY = Mathf.Abs(playerCamera.transform.position.y - localFish.y);
            float sonarY = distY / rangeY;
            float a = 1 - sonarY;
            a = Mathf.Max(a * a, 1);

            Image newBlep = getNextBlep(i);
            newBlep.transform.localPosition = new Vector3(sonarX, sonarZ, this.transform.position.z);

            var tempColor = newBlep.color;
            tempColor.a = a;
            newBlep.color = tempColor;
            currentBleps.Add(newBlep);
        }
    }

    public bool IsFinale()
    {
        return gameState.PlayState == GameState.GameplayState.End;
    }

    public bool IsFunctioning()
    {
        return gameState.SonarState == GameState.MaschienState.On;
    }

    public Image getNextBlep(int i)
    {
        Image newBlep;
        if (i < (blepCache.Count))
        {
            newBlep = blepCache[i];
            newBlep.enabled = true;
        }
        else
        {
            newBlep = Instantiate(myBlep, this.transform);
            blepCache.Add(newBlep);
        }
        return newBlep;
    }

    public List<GameObject> GetNearbyFish(float depth)
    {
        List<GameObject> nearbyFish = new List<GameObject>();

        List<GameObject> fish = fishManager.getFishAtDepth(depth);

        foreach (GameObject f in fish)
        {
            FishAI fai = f.GetComponent<FishAI>();
            float disty = Mathf.Abs(depth - f.transform.position.y);
            if (disty <= rangeY)
            {
                Vector2 myPos = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.z);
                Vector2 fishPos = new Vector2(f.transform.position.x, f.transform.position.z);
                if (Vector2.Distance(myPos,fishPos) <= rangeX)
                { 
                    nearbyFish.Add(f);
                }
            }
        }

        return nearbyFish;
    }


}
