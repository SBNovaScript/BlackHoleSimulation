using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double mass;

    private void Update()
    {
        float radius = CalculateSchwarzschild(mass);

        transform.localScale = new Vector3(radius, radius, radius);
    }

    private float CalculateSchwarzschild(double mass)
    {
        return Convert.ToSingle((2 * PhysicsConstants.G * mass) / (Math.Pow(PhysicsConstants.c, 2)));
    }

}
