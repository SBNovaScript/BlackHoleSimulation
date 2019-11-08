using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double mass = PhysicsConstants.EARTH_MASS;

    [SerializeField]
    private float radius;

    private void Update()
    {
        radius = PhysicsCalculations.CalculateSchwarzschildToFloat(mass);

        transform.localScale = new Vector3(radius, radius, radius);
    }

}
