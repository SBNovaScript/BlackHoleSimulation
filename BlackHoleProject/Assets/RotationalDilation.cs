using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalDilation : MonoBehaviour
{
    public bool isReference = false;

    [Header("Black Hole Settings")]
    public GameObject reference;

    void Start()
    {
        if (isReference)
        {
            StartCoroutine(RotateObjectStatic(1, Vector3.up, 1));
        }
        else
        {
            StartCoroutine(RotateObjectFromReference(1, Vector3.up));
        }
    }

    IEnumerator RotateObjectStatic(float angle, Vector3 axis, float inTime)
    {
        float rotationSpeed = angle / inTime;

        while(true)
        {
            transform.Rotate(axis, rotationSpeed);

            yield return null;
        }
    }

    IEnumerator RotateObjectFromReference(float angle, Vector3 axis)
    {
        double blackHoleMass = reference.GetComponent<CelestialObject>().mass;

        while (true)
        {
            float distance = Math.Abs(this.transform.position.z - reference.transform.position.z);
            //float distance = Vector3.Distance(this.transform.position, reference.transform.position);

            float rotationSpeed = GetRotationSpeed(blackHoleMass, distance);

            transform.Rotate(axis, rotationSpeed);

            yield return null;
        }
    }

    private static float GetRotationSpeed(double blackHoleMass, float distance)
    {
        // 0.01, 2.5
        // 0.009, 1.0
        
        double newDistance = 0.009 + ((distance - 0.01) * (1.0 - 0.009)) / (2.5 - 0.01);

        double timeDilation = PhysicsCalculations.getTimeDilationAmount(1.0, blackHoleMass, newDistance);

        float rotationSpeed = (1.0f - (float)timeDilation) + 0.2f;

        return rotationSpeed;
    }
}
