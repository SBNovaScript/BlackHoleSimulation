using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double mass = PhysicsConstants.EARTH_MASS;

    // In Meters
    [SerializeField]
    private float radius;

    // In Meters / Second
    [SerializeField]
    private double escapeVelocity;

    private void Update()
    {
        radius = PhysicsCalculations.CalculateSchwarzschildToFloat(mass);

        // Escape velocity never changes with the black hole radius,
        // Since it will always be the speed of light.
        escapeVelocity = PhysicsCalculations.CaclulateEscapeVelocityToFloat(mass, radius);

        transform.localScale = new Vector3(radius, radius, radius);
    }

}
