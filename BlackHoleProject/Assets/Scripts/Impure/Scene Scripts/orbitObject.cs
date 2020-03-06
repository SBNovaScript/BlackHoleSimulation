﻿using UnityEngine;

public class orbitObject : MonoBehaviour
{
    public GameObject[] orbitTargets;
    public Vector3 initialVelocty;
    public float timeStep;
    public double mass;
    private Rigidbody rb;
    private ScaleManagerScript scaleManager;

    private void Start()
    {
        scaleManager = GameObject.Find("ScaleManager").GetComponent<ScaleManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        initialVelocty.z = (float)((double)initialVelocty.z / scaleManager.velocityScale);
        initialVelocty.y = (float)((double)initialVelocty.y / scaleManager.velocityScale);

        rb.velocity = initialVelocty;
    }


    private void FixedUpdate()
    {
        GameObject orbitTarget;
        for (int i = 0; i < orbitTargets.Length; i++)
        {
            orbitTarget = orbitTargets[i];
            Vector3 gravDir = orbitTarget.transform.position - gameObject.transform.position;
            double mag = (double) gravDir.magnitude;
            double gravForce = PhysicsCalculations.getGravitationalForceBasic(orbitTarget.GetComponent<orbitObject>().mass, mag * scaleManager.distanceScale / 1000);

            gravDir.Normalize();
            //gravForce = PhysicsCalculations.getAccelerationFromForce(mass, gravForce);

            float gravForce_f = PhysicsCalculations.ConvertDoubleToFloat(gravForce / scaleManager.velocityScale) * Time.smoothDeltaTime;
            gravDir.Scale(new Vector3(gravForce_f, gravForce_f, gravForce_f));
            rb.velocity += gravDir;

        }
        initialVelocty = rb.velocity;
    }
}