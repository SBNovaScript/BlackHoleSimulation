using System;

public static class PhysicsCalculations
{
    public static float CalculateSchwarzschildToFloat(double mass)
    {
        double schwarzschildRadius = (2 * PhysicsConstants.G * mass) / (Math.Pow(PhysicsConstants.c, 2));

        // Converts double to float
        return Convert.ToSingle(schwarzschildRadius);
    }
}
