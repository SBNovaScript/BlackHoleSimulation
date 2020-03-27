using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalDilation : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RotateObject(1, Vector3.up, 1));
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
