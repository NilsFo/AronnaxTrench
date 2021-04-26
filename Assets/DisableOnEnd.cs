using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEnd : MonoBehaviour
{
    public GameState gameState;
    void Update()
    {
        if (gameState.GeneralState == GameState.MaschienState.Defective)
        {
            gameObject.SetActive(false);
        }
    }
}
