using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbitObject : MonoBehaviour
{
    public GameObject orbitTarget;
    public float radius;
    public float secondsPerOrbit;

    void Update()
    {

        transform.position = orbitTarget.transform.position;

        float orbitPercent = Time.realtimeSinceStartup / secondsPerOrbit * 2 * Mathf.PI;
        transform.position += new Vector3(Mathf.Cos(orbitPercent) * radius, 0, Mathf.Sin(orbitPercent) * radius);
    }
}
