using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambientSoundController : MonoBehaviour
{
    public GameState gameState;
    public AudioSource engine;
    public AudioSource metalHit;
    public AudioSource glassCrack;
    public AudioSource alarm;

    void Start() {
        InvokeRepeating("probMetalHitSound", 0, 5.125f);
        InvokeRepeating("probGlassCrackSound", 0, 7.25f);
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
        if(!alarm.isPlaying && 
            (gameState.PressureState == GameState.MaschienState.Defective
            || gameState.OTwoInteriorState == GameState.MaschienState.Defective
            || gameState.PlayerVelocityState == GameState.MaschienState.Defective)) {
                alarm.loop = true;
                alarm.Play();
            }
            else if(alarm.isPlaying && 
                (gameState.PressureState != GameState.MaschienState.Defective
                && gameState.OTwoInteriorState != GameState.MaschienState.Defective
                && gameState.PlayerVelocityState != GameState.MaschienState.Defective)) {
                    alarm.loop = false;
                }
    }


    private void probMetalHitSound() {
        if(gameState.HullIntegrity > 0.5f) {
            if(Random.Range(0f,1f) < 0.2f) {
                PlayMetalHitSound();
            }
        }
    }
    public void PlayMetalHitSound() {
        if(!metalHit.isPlaying) {
            metalHit.pitch = Random.Range(0.7f, 1.1f);
            metalHit.panStereo = Random.Range(-0.5f, 0.5f);
            metalHit.Play();
        }
    }
    private void probGlassCrackSound() {
        if(gameState.HullIntegrity > 0.8f) {
        
            if(Random.Range(0f,1f) < 0.2f) {
                PlayGlassCrack();
            }
        }
    }

    public void PlayGlassCrack() {
        if(!glassCrack.isPlaying) {
            glassCrack.pitch = Random.Range(0.9f, 1.1f);
            glassCrack.Play();
        }
    }
}
