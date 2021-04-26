using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BulletinFish : MonoBehaviour, IPointerDownHandler
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

        myText.text = "???";
        myText.gameObject.SetActive(true);
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
        myImage.gameObject.SetActive(true);

        var tempColor = myImage.color;
        tempColor.a = 1f;
        myImage.color = tempColor;
    }

    public bool IsDiscovered()
    {
        if (id <= FishDataVault.FISH_ID_MAX)
        {
            return manager.GameState.CaughtFishIDs[id];
        }
        return false;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        float depth = manager.GameState.CurrentDepth;
        string text;
        if (id != 17)
        {
            if (IsDiscovered())
            {
                text = dataVault.GetName(id);
            }
            else
            {
                if (dataVault.GetRarity(id) == 0)
                {
                    text = "We have no information about this species. It might be deep in the trench.";
                }
                else
                {
                    if (dataVault.IsDepthAboveMinDepth(id,depth))
                    {
                        if (dataVault.IsDepthBelowMax(id, depth))
                        {
                            text = "This species should be nearby. Keep looking.";
                        }
                        else
                        {
                            text = "Sensors indicate you are close to the surface for this fish. You need to go deeper.";
                        }
                    }
                    else
                    {
                        text = "Sensors indicate you are too low for this fish. Keep rising.";
                    }
                }
            }
        }else
        {
            text = "We have no information about this species. It might be very deep in the trench.";
        }
        
        //Output the name of the GameObject that is being clicked
        FindObjectOfType<RadioManager>().RadioMessage(text, 7f);
    }

}
