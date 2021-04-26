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
        int count = 0;
        foreach(bool b in caughtList)
        {
            if (b) count = count + 1;
        }

        text.text = "Discovered "+count+" out of "+caughtList.Length+" fish.";
    }


}
