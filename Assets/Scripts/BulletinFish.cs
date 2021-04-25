using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinFish : MonoBehaviour
{

    public int id=0;
    public FishDataVault dataVault;
    public ShipUiManager manager;

    public Image myImage;
    public Text myText;

    private bool unlockCache = false;

    void Start()
    {
        myImage.gameObject.SetActive(false);
        myText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!unlockCache)
        {
            if (IsDiscovered())
            {
                Unlock();
                unlockCache = true;
                myImage.gameObject.SetActive(true);
                myText.gameObject.SetActive(true);
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
        if (id < FishDataVault.FISH_ID_MAX)
        {
            return manager.GameState.CaughtFishIDs[id];
        }
        return false;
    }

}
