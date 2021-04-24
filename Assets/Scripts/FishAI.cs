using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishAI : MonoBehaviour, IPointerClickHandler
{

    public Image myImage;
    public FishDataVault dataVault;
    public float currentCountDown;
    public int id=0;
    public float minDepth;
    public float maxDepth;
    public int rarity;
    public string fishName = "";
    public float speedX;

    // Start is called before the first frame update
    void Start()
    {
        dataVault.ReadData();
        InitData(id);
    }

    // Update is called once per frame
    void Update()
    {
        currentCountDown = currentCountDown + Time.deltaTime;

        if(currentCountDown > 5)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        Transform t = GetComponent<Transform>();
        t.position = new Vector3(t.position.x + speedX, t.position.y, t.position.z);
    }


    public bool IsFishClickState()
    {
        // TODO: Check if camera is equipped here
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(this.gameObject);
    }

    public void InitData(int id)
    {
        string[] data = dataVault.GetAllData(id);

        //print(id);
        //print(data);
        this.id = id;
        this.fishName = dataVault.GetName(id);
        this.minDepth = dataVault.GetMinDepth(id);
        this.maxDepth = dataVault.GetMaxDepth(id);
        this.rarity = dataVault.GetRarity(id);
        this.speedX = dataVault.GetSpeedX(id);

        myImage.sprite = dataVault.GetSprite(id);
    }


}
