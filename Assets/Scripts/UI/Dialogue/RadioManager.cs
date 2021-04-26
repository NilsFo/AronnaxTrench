using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public Transform radioPos;
    public Transform radioPos2;
    public GameState gameState;
    public RectTransform leftEdge;
    public RectTransform rightEdge;
    
    private bool breakerTutorial;
    private TextBubbleManager textBubbleManager;
    
    // Start is called before the first frame update
    void Start()
    {
        textBubbleManager = GetComponent<TextBubbleManager>();
        Invoke("DialogueLine1", 3);
        Invoke("DialogueLine2", 3+5);
        Invoke("DialogueLine3", 3+5+7);
    }



    

    // Update is called once per frame
    void Update()
    {
        float closestX;
        if(Mathf.Abs(radioPos.position.x - transform.position.x) < Mathf.Abs(radioPos2.position.x - transform.position.x)) {
            closestX = radioPos.position.x;
        } else {
            closestX = radioPos2.position.x;
        }
        closestX = Mathf.Min(closestX, rightEdge.position.x-420);
        closestX = Mathf.Max(closestX, leftEdge.position.x+450);
        transform.position = new Vector3(closestX, radioPos.position.y, 0);

        if(!breakerTutorial && gameState.FuseOne == GameState.FuseState.On) {
            breakerTutorial = true;
            textBubbleManager.ClearDialogueBoxes();
            Invoke("DialogueLine4", 1);
            Invoke("DialogueLine5", 1+7);
            Invoke("DialogueLine6", 25);
            Invoke("DialogueLine7", 25+7);
        }
    }

    public void DialogueLine1() {
        if(!breakerTutorial)
            textBubbleManager.Say(transform, "Radio check. Do you read, Nemo One?", 5);
    }
    public void DialogueLine2() {
        if(!breakerTutorial)
            textBubbleManager.Say(transform, "It looks like the drop knocked out some of your breakers, at least your radio is still online.", 7);
    }
    public void DialogueLine3() {
        if(!breakerTutorial)
            textBubbleManager.Say(transform, "Take a look around, find the fuse box and get the sub back online.", 7);
    }
    public void DialogueLine4() {
        textBubbleManager.Say(transform, "Alright, your pumps should be ready to go, expedition of the Arronax Trench is underway.", 7);
    }
    public void DialogueLine5() {
        textBubbleManager.Say(transform, "I hope you brought your camera. Get us some good pictures of the local wildlife if you can.", 7);
    }
    public void DialogueLine6() {
        textBubbleManager.Say(transform, "Keep an eye on your interior pressure. We don't want you crushed at the bottom of the trench.", 7);
    }
    public void DialogueLine7() {
        textBubbleManager.Say(transform, "The interior pressure should always be close to the exterior pressure.", 7);
    }
}
