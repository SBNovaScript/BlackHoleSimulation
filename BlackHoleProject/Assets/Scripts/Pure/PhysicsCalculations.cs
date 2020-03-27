using System;

public static class PhysicsCalculations
{

    public static double CalculateSchwarzschild(double mass)
    {
        return (2.0 * PhysicsConstants.G * mass) / (Math.Pow(PhysicsConstants.c, 2.0));
    }

    public static float CalculateSchwarzschildToFloat(double mass)
    {
        return ConvertDoubleToFloat(CalculateSchwarzschild(mass));
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

    //units:
    //Mass:     Kg
    //Distance: m
    public static float getGravitationalForce(double mass1, double mass2, double distance)
    {
        double force = PhysicsConstants.G * (mass1 * mass2) / (distance * distance);
        return ConvertDoubleToFloat(force);
    }

    //units:
    //Mass:     Kg
    //Distance: m
    public static double getGravitationalForceBasic(double mass, double distance)
    {
        double force = PhysicsConstants.G * mass / (distance * distance);
        return force;
    }

    //units:
    //Mass:     Kg
    //Force:    kg*m/s
    public static double getAccelerationFromForce(double mass, double force)
    {
        return force / mass;
    }

    public static double getTimeDilationAmount(double changeInTime, double mass, double distance)
    {
        return changeInTime * Math.Sqrt(Math.Abs(1.0 - ((CalculateSchwarzschild(mass) / distance))));
    }
}
