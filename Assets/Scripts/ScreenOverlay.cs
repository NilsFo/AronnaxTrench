using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenOverlay : MonoBehaviour
{
    public ShipUiManager manager;
    public Image overlay;
    public float GameOverFadeout = 7f;
    private float t;
    
    public Text thanksText;
    public Text fishText;
    

    // Update is called once per frame
    void Update()
    {
        if(manager.GameState.PlayState == GameState.GameplayState.Gameover) {
            t += Time.deltaTime;
            overlay.color = new Color(0, 0, 0, t/GameOverFadeout);
            if(t > GameOverFadeout) {
                thanksText.enabled = true;
                if(manager.GameState.IsSuffocation >= 1)
                    thanksText.text = "Mission Aborted\nInsufficient Oxygen Levels";
                else
                    thanksText.text = "Mission Aborted\nDangerous Pressure Difference";
                fishText.enabled = true;
            }
            if(t > GameOverFadeout * 2) {
                SceneManager.LoadScene("MainMenu");
            }
        } else if (manager.GameState.IsSuffocation > 0.1) {
            overlay.color = new Color(0, 0, 0, manager.GameState.IsSuffocation*0.8f);
        }
    }
}
