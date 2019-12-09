using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double mass = PhysicsConstants.EARTH_MASS;
    //reference to text displaying data about the sphere
    public GameObject infoText;
    //string values to be shown in said window
    private string[] mFieldNames = {"Radius", "Mass", "Escape Velocity"};

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
        string[] args = { (radius*1000).ToString() +" mm", mass.ToString()+" kg", (escapeVelocity/1000).ToString() +" km/s"};
        updateInfoWindow(args);
    }


    private void updateInfoWindow(string[] values)
    {
        string outputString = "";
        for (int i = 0; i < mFieldNames.Length; i++)
        {
            outputString += mFieldNames[i] + ": ";
            if (i < values.Length)
            {
                outputString += values[i];
            } else
            {
                outputString += "NO_VALUE";
            }
            outputString += "\n";
        }
        infoText.GetComponent<TextMesh>().text = outputString;
    }

}
