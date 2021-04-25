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

    // Start is called before the first frame update
    void Start()
    {
        currentBleps = new List<Image>();
        blepCache = new List<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Deleting old bleps
        foreach(Image i in currentBleps)
        {
            i.enabled = false;
        }
        currentBleps.Clear();

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

            float sonarX = localFish.x / rangeX * (rect.rect.width*0.75f);
            float sonarZ = localFish.z / rangeX * (rect.rect.height*0.75f);

            float distY = Mathf.Abs(playerCamera.transform.position.y - localFish.y);
            float sonarY = distY / rangeY;
            float a = 1 - sonarY;

            Image newBlep;
            if(i < (blepCache.Count))
            {
                newBlep = blepCache[i];
                newBlep.enabled = true;
            }
            else
            {
                newBlep = Instantiate(myBlep, this.transform);
                blepCache.Add(newBlep);
            }
            newBlep.transform.localPosition = new Vector3(sonarX, sonarZ, this.transform.position.z);

            var tempColor = newBlep.color;
            tempColor.a = a;
            newBlep.color = tempColor;
            currentBleps.Add(newBlep);
        }

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
