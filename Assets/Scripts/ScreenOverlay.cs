using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    public ShipUiManager manager;
    public Image overlay;
    public float GameOverFadeout = 7f;
    private float t;
    

    // Update is called once per frame
    void Update()
    {
        if(manager.GameState.PlayState == GameState.GameplayState.Gameover) {
            t += Time.deltaTime;
            overlay.color = new Color(0, 0, 0, t/GameOverFadeout);
        } else if (manager.GameState.IsSuffocation > 0.1) {
            overlay.color = new Color(0, 0, 0, manager.GameState.IsSuffocation/2);
        }
    }
}
