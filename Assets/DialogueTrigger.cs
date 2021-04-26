using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string message;
    public float duration;
    private bool triggered;
    
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !triggered) {
            triggered = true;
            FindObjectOfType<RadioManager>().RadioMessage(message, duration);
        }
    }
}
