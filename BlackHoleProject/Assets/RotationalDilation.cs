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
            //StartCoroutine(RotateObjectStatic(1, Vector3.up, 1));
        }
        else
        {
            //StartCoroutine(RotateObjectFromReference(1, Vector3.up));
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
            float distance = Vector3.Distance(this.transform.position, reference.transform.position);
            //float newDistance = 0.005f + (distance - 0) * (0.1f - 0.005f) / (20f - 0);

            float newDistance = distance / 10;

            double timeDilation = PhysicsCalculations.getTimeDilationAmount(1.0, blackHoleMass, 0.01);

            float rotationSpeed = (float)timeDilation;

            Debug.Log(rotationSpeed);

            transform.Rotate(axis, rotationSpeed);

            yield return null;
        }
    }
}
