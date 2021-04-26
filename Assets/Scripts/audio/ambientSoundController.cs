using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientSoundController : MonoBehaviour
{
    public GameState gameState;
    public AudioSource engine;
    public AudioSource metalHit;
    public AudioSource glassCrack;

    void Start() {
        InvokeRepeating("MetalHitSound", 0, 5.125f);
        InvokeRepeating("GlassCrackSound", 0, 7.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!engine.isPlaying && gameState.EngienState == GameState.MaschienState.On) {
            engine.Play();
        }
        else if(engine.isPlaying && gameState.EngienState != GameState.MaschienState.On) {
            engine.Stop();
        }
    }


    private void MetalHitSound() {
        if(Mathf.Abs(gameState.ExteriorPressure - gameState.InteriorPressure) > 200) {
            if(!metalHit.isPlaying) {
                if(Random.Range(0f,1f) < 0.2f) {
                    metalHit.pitch = Random.Range(0.7f, 1.1f);
                    metalHit.panStereo = Random.Range(-0.5f, 0.5f);
                    metalHit.Play();
                }
            }
        }
    }
    private void GlassCrackSound() {
        if(Mathf.Abs(gameState.ExteriorPressure - gameState.InteriorPressure) > 500) {
            if(!glassCrack.isPlaying) {
                if(Random.Range(0f,1f) < 0.1f) {
                    glassCrack.pitch = Random.Range(0.9f, 1.1f);
                    glassCrack.Play();
                }
            }
        }
    }
}
