using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class FollowRotation: MonoBehaviour
{

    public FloatVariable rotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, rotation.Value, 0);
    }
    
    
}
