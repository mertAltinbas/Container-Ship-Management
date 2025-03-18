using ContainerManagement.Exceptions;
using ContainerManagement.Interfaces;

namespace ContainerManagement.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }
    
    public LiquidContainer(int height, double tareWeight, double depth, double maxPayload, bool isHazardous)
        : base(height, tareWeight, depth, "L", maxPayload)
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double weight)
    {
        double maxAllowedFill = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;

        if (CurrentCargoWeight > maxAllowedFill)
        {
            NotifyHazard($"Attempted to load {CurrentCargoWeight}kg into {SerialNumber} which exceeds the allowed {maxAllowedFill}kg.");
            throw new OverfillException($"Cannot load {CurrentCargoWeight}kg into container {SerialNumber}. Maximum allowed fill is {maxAllowedFill}kg.");
        }
        base.LoadCargo(weight);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"HAZARD NOTIFICATION [{SerialNumber}]: {message}");
    }

    public override string ToString()
    {
        return $"Liquid Container: {SerialNumber}, Hazardous: {IsHazardous}, Cargo Mass Weight: {CurrentCargoWeight}kg/{MaxPayload}kg";
    }
}