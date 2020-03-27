using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public double Mass = PhysicsConstants.EARTH_MASS;

    // reference to text displaying data about the sphere
    public GameObject InfoText;
    // string values to be shown in said window
    private readonly Dictionary<string, string> mFieldNamesWithValues = new Dictionary<string, string>()
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
        radius = PhysicsCalculations.CalculateSchwarzschildToFloat(Mass);

        // Escape velocity never changes with the black hole radius,
        // Since it will always be the speed of light.
        escapeVelocity = PhysicsCalculations.CaclulateEscapeVelocityToFloat(Mass, radius);

        transform.localScale = new Vector3(radius, radius, radius);

        // Get the latest data values from the object, and display them
        // To the provided info text box.
        ExtractDisplayableDataValues();
        UpdateInfoWindow();
    }

    private void ExtractDisplayableDataValues()
    {
        // TODO: Remove magic numbers and place conversions into their own functions.
        mFieldNamesWithValues["Radius"] = (radius * 1000).ToString() + " mm";
        mFieldNamesWithValues["Mass"] = Mass.ToString() + " kg";
        mFieldNamesWithValues["Escape Velocity"] = (escapeVelocity / 1000).ToString() + " km/s";
    }

    private void UpdateInfoWindow()
    {
        // Maps over each key + value pair using Linq.
        IEnumerable<string> outputString =
            mFieldNamesWithValues.Select(field => field.Key + ": " + field.Value);

        InfoText.GetComponent<TextMesh>().text = string.Join(Environment.NewLine, outputString);
    }

}
