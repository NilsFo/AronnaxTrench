using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCaughtText : MonoBehaviour
{

    public GameState gameState;
    public Text text;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        bool[] caughtList =  gameState.CaughtFishIDs;
        int caughtCount = 0;
        int maxCount = caughtList.Length + 1;

        foreach (bool b in caughtList)
        {
            if (b) caughtCount = caughtCount + 1;
        }
        if (gameState.tookFotoOfMonster)
        {
            caughtCount = caughtCount + 1;
        }

        text.text = "Discovered "+ caughtCount + " out of "+ maxCount + " fish.";
    }






}
