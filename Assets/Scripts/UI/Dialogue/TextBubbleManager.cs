using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubbleManager : MonoBehaviour
{
    public DialogueBox dialogueBoxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DialogueBox Say(Transform who, string text, float duration = 5f) {
        var box = Instantiate(dialogueBoxPrefab, transform);
        box.trackObject = who;
        if(duration == -1f) {
            box.lifetime = text.Length * box.secondsPerCharacter * 2 + 2;
        } else {
            box.lifetime = duration;
        }
        box.SetText(text);
        return box;
    }

    public void ClearDialogueBoxes() {
        foreach(var comp in  GetComponentsInChildren<DialogueBox>()) { 
            Destroy(comp.gameObject);
        }
    }
}
