using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MoveAlongPath : MonoBehaviour
{

    public float speedX;
    public bool walking;
    public PathCreator path;
    public EndOfPathInstruction endOfPathBehaviour = EndOfPathInstruction.Stop;

    public float magniudeY = 0.75f;
    public float speedY = 0.69f;

    private float distanceTraveled = 0;
    private float distanceFloated = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            distanceTraveled = distanceTraveled + Time.deltaTime;
            if (path != null)
            {
                transform.position = path.path.GetPointAtDistance(distanceTraveled, endOfPathBehaviour);
            }

            if (isAtEnd())
            {
                distanceFloated = distanceFloated + Time.deltaTime;
                Vector3 pos = transform.position;
                float y = pos.y + Mathf.Sin(distanceFloated * speedY) * magniudeY;
                pos.y = y;
                transform.position = pos;
            }
        }
    }

    public bool isAtEnd()
    {
        return distanceTraveled >= path.path.length;
    }

}
