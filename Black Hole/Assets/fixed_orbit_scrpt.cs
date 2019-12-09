using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixed_orbit_scrpt : MonoBehaviour
{
    public GameObject orbitTarget;
    public float radius;
    public float secondsPerOrbit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = orbitTarget.transform.position;

        float orbitPercent = Time.realtimeSinceStartup / secondsPerOrbit * 2 * Mathf.PI;
        transform.position += new Vector3(Mathf.Cos(orbitPercent) * radius, 0, Mathf.Sin(orbitPercent) * radius);
    }
}
