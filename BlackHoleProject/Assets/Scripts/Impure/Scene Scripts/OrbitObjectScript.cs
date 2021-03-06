﻿using System.Collections.Generic;
using UnityEngine;

public class OrbitObjectScript : MonoBehaviour
{
    public List<GameObject> orbitTargets = new List<GameObject>();
    public Vector3 initialVelocty = new Vector3( 0, -2000.022f, 2000.022f );
    public float timeStep = 1;
    public double mass = PhysicsConstants.MOON_MASS;
    private Rigidbody rb;
    private ScaleManagerScript scaleManager;

    private void Start()
    {
        orbitTargets.Add(GameObject.FindWithTag("BlackHole"));
        scaleManager = GameObject.Find("ScaleManager").GetComponent<ScaleManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        initialVelocty.z = (float)((double)initialVelocty.z / scaleManager.velocityScale);
        initialVelocty.y = (float)((double)initialVelocty.y / scaleManager.velocityScale);

        if (Mathf.Abs(initialVelocty.x) == Mathf.Infinity || Mathf.Abs(initialVelocty.x) == Mathf.Infinity || Mathf.Abs(initialVelocty.x) == Mathf.Infinity)
        {
            rb.velocity = Vector3.zero;
        }

        else if (float.IsNaN(initialVelocty.x) || float.IsNaN(initialVelocty.y) || float.IsNaN(initialVelocty.z))
        {
            rb.velocity = Vector3.zero;
        }

        else
        {
            rb.velocity = initialVelocty;
        }
    }


    private void FixedUpdate()
    {
        GameObject orbitTarget;
        for (int i = 0; i < orbitTargets.Count; i++)
        {
            orbitTarget = orbitTargets[i];
            Vector3 gravDir = orbitTarget.transform.position - gameObject.transform.position;
            double mag = (double)gravDir.magnitude;
            double gravForce = PhysicsCalculations.getGravitationalForceBasic(orbitTarget.GetComponent<OrbitObjectScript>().mass, mag * scaleManager.distanceScale / 1000);

            gravDir.Normalize();
            //gravForce = PhysicsCalculations.getAccelerationFromForce(mass, gravForce);

            float gravForce_f = PhysicsCalculations.ConvertDoubleToFloat(gravForce / scaleManager.velocityScale) * Time.smoothDeltaTime;
            gravDir.Scale(new Vector3(gravForce_f, gravForce_f, gravForce_f));
            rb.velocity += gravDir;

        }
        initialVelocty = rb.velocity;
    }
}
