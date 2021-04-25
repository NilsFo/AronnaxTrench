using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationSway : MonoBehaviour
{
    // Start is called before the first frame update

    public float swayAmount = 5f;
    public float swaySpeed = 1f;
    private Quaternion initRot;
    void Start()
    {
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Mathf.Sin((Time.time + transform.position.magnitude*10) * swaySpeed) * swayAmount;
        transform.rotation = initRot * Quaternion.Euler(xRot, 0, 0);
    }
}
