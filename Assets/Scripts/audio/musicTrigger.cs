using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTrigger : MonoBehaviour
{
    public AudioSource musicTrack;
    public bool played;
    
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !played)
        {
            musicTrack.Play();
            played = true;
        }
    }
}
