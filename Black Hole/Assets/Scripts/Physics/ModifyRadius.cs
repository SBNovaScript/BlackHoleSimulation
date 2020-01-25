using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double mass = PhysicsConstants.EARTH_MASS;
    //reference to text displaying data about the sphere
    public GameObject infoText;
    //string values to be shown in said window
    private Dictionary<string, string> mFieldNamesWithValues = new Dictionary<string, string>() 
    {
        { "Radius", "0"},
        { "Mass", "0" },
        { "Escape Velocity", "0" } 
    };

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

        // Get the latest data values from the object, and display them
        // To the provided info text box.
        ExtractDisplayableDataValues();
        UpdateInfoWindow();
    }

    private void ExtractDisplayableDataValues()
    {
        //TODO: Remove magic numbers and place conversions into their own functions. 
        mFieldNamesWithValues["Radius"] = (radius * 1000).ToString() + " mm";
        mFieldNamesWithValues["Mass"] = mass.ToString() + " kg";
        mFieldNamesWithValues["Escape Velocity"] = (escapeVelocity / 1000).ToString() + " km/s";
    }

    private void UpdateInfoWindow()
    {
        // Maps over each key + value pair using Linq.
        IEnumerable<string> outputString = 
            mFieldNamesWithValues.Select(field => field.Key + ": " + field.Value);

        infoText.GetComponent<TextMesh>().text = string.Join(Environment.NewLine, outputString);
    }

}
