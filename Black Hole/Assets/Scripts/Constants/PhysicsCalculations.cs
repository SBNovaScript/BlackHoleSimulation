using System;

public static class PhysicsCalculations
{
    public static float CalculateSchwarzschildToFloat(double mass)
    {
        double schwarzschildRadius = (2 * PhysicsConstants.G * mass) / (Math.Pow(PhysicsConstants.c, 2));

        return ConvertDoubleToFloat(schwarzschildRadius);
    }

    public static double CaclulateEscapeVelocityToFloat(double mass, double radius)
    {
        double escapeVelocity = Math.Sqrt((2 * PhysicsConstants.G * mass) / radius);

        return ConvertDoubleToFloat(escapeVelocity);
    }

    public static float ConvertDoubleToFloat(double value)
    {
        return Convert.ToSingle(value);
    }
}
