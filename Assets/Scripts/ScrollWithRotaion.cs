using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class ScrollWithRotaion : MonoBehaviour
{
    public FloatVariable rotation;
    
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, rotation.Value, 0);
    }
}
