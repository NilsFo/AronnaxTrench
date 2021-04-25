using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinFish : MonoBehaviour
{

    public int id=0;
    public FishDataVault dataVault;
    public GameState gameState;

    public Image myImage;
    public Text myText;

    private bool unlockCache = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!unlockCache)
        {
            if (IsDiscovered())
            {
                Unlock();
                unlockCache = true;
            }
        }
    }

    public void Unlock()
    {
        myText.text = dataVault.GetName(id);
        myImage.sprite = dataVault.GetSprite(id);

        var tempColor = myImage.color;
        tempColor.a = 1f;
        myImage.color = tempColor;
    }

    public bool IsDiscovered()
    {
        return gameState.caughtFishIDs[id];
    }

}
