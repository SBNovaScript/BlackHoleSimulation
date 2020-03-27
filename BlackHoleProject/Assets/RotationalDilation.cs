using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalDilation : MonoBehaviour
{
    public bool isReference = false;

    [Header("Black Hole Settings")]
    public Transform referenceTransform;

    void Start()
    {
        if (isReference)
        {
            StartCoroutine(RotateObject(1, Vector3.up, 1));

        }
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        float rotationSpeed = angle / inTime;

        while(true)
        {
            transform.Rotate(axis, rotationSpeed);

            yield return null;
        }
    }
}
