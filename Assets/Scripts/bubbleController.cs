using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleController : MonoBehaviour
{
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float i = 0;
        foreach(var ps in GetComponentsInChildren<ParticleSystem>()) {
            var em = ps.emission;
            if(gameState.PumpState == GameState.MaschienState.On){
                em.rateOverTime = 10 * Mathf.Abs(gameState.pressureDelta/gameState.MaxPumpPressure) + 1 + i;
            }
            else {
                em.rateOverTime = 1 + i;
            }
            i += 0.25f;
        }
    }
}
