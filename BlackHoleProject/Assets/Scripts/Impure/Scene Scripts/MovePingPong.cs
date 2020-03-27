using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePingPong : MonoBehaviour
{

    public GameObject properTimeFrame;
    public GameObject blackHole;
    public bool isBlackHole = false;

    private OrbitObject orbitObject;


    private void Start() {
        if (isBlackHole)
        {
            orbitObject = blackHole.GetComponent<OrbitObject>();
        }
    }

    void Update()
    {
        if (isBlackHole)
        {
            float distance = Vector3.Distance(blackHole.transform.position, properTimeFrame.transform.position);
            float newDistance = 0.005f + (distance - 0) * (0.1f - 0.005f) / (20f - 0);

            float timeDilation = 1.0f - (float)PhysicsCalculations.getTimeDilationAmount(1.0, orbitObject.mass, distance);

            //Debug.Log(timeDilation);
        }
        //    transform.position = new Vector3(Mathf.PingPong(Time.time * timeDilation, 0.5f), transform.position.y, transform.position.z);
        //} else
        //{
        //    transform.position = new Vector3(Mathf.PingPong(Time.time, 1), transform.position.y, transform.position.z);
        //}
        
    }
}
