using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Finale : MonoBehaviour
{
    public Gradient FadeoutColorGradient;
    public Image overlayImage;
    public Text thanksText;
    public Text fishText;
    public string nextSceneName = "MainMenu";

    public float finaleStartDepth = -370;
    public GameState gameState;
    public FishManagerAI fishManager;
    public Camera playerCamera;
    public Transform playerBoat;
    public MoveAlongPath monsterMover;

    public float secondsSinceEnd = 0f;
    private bool fadingOut = false;

    private float fadeoutDelayCurrent = 0f;
    public float fadeoutDelayTarget = 6.66f;

    private float fadeoutCurrent = 0f;
    public float fadeoutTarget = 8f;
    public float textDelay = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print("state: "+gameState.playState);

        // Checking and changing states
        if (!(gameState.PlayState == GameState.GameplayState.End))
        {
            if (gameState.CurrentDepth <= finaleStartDepth)
            {
                InitEnding();
            } 
        }

        if (gameState.tookFotoOfMonster && !fadingOut)
        {
                StartFadeOut();
        }
        

        if (gameState.PlayState == GameState.GameplayState.End)
        {
            secondsSinceEnd = secondsSinceEnd + Time.deltaTime;
        }

        // Fading out....
        if (fadingOut)
        {
            if (fadeoutDelayCurrent < fadeoutDelayTarget)
            {
                fadeoutDelayCurrent = fadeoutDelayCurrent + Time.deltaTime;
                print("Waiting for fadeout...: "+ fadeoutDelayCurrent);
            }
            else
            {
                fadeoutCurrent = fadeoutCurrent + Time.deltaTime;
                float b = fadeoutCurrent / fadeoutTarget;
                overlayImage.color = FadeoutColorGradient.Evaluate(b);

                if(fadeoutCurrent > textDelay)
                {
                    thanksText.enabled = true;
                    fishText.enabled = true;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (fadeoutCurrent > textDelay)
            {
                print("Loading next scene");
                SceneManager.LoadSceneAsync(nextSceneName);
            }
        }
    }

    public void InitEnding()
    {
        print("Initiating the end.");
        gameState.PlayState = GameState.GameplayState.End;
        monsterMover.walking = true;

        // TODO spawn tentacles here ??

        // TODO disable player controls & lock player in place here here

        // TODO disable all non critical sounds here
        // TODO make UI elements go crazy here
        // TODO play music and chanting over 
    }

    public void StartFadeOut()
    {
        print("Starting to fade out.");
        fadingOut = true;
        overlayImage.color = new Color(1, 1, 1, 1);
    }

}
