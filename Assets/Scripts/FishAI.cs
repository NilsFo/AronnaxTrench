using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishAI : MonoBehaviour, IPointerClickHandler
{

    public float currentCountDown;
    public int id=0;
    public int minDepth;
    public int maxDepth;
    public string fishName = "";
    public FishDataVault dataVault;

    // Start is called before the first frame update
    void Start()
    {
        InitData(id);
    }

    // Update is called once per frame
    void Update()
    {
        currentCountDown = currentCountDown + Time.deltaTime;
        Transform t = GetComponent<Transform>();
        t.position = new Vector3(t.position.x+currentCountDown, t.position.y,t.position.z);

        if(currentCountDown > 5)
        {
            Destroy(this.gameObject);
        }
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

        this.id = int.Parse(data[0]);
        this.fishName = data[1];
        this.minDepth = int.Parse(data[2]);
        this.maxDepth = int.Parse(data[3]);
    }
}
