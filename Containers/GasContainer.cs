using System.ComponentModel;
using ContainerManagement.Exceptions;
using ContainerManagement.Interfaces;

namespace ContainerManagement.Containers;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }
    
    public GasContainer(int height, double tareWeight, double depth, double maxPayload, double pressure) 
        : base(height, tareWeight, depth, "G", maxPayload)
    {
        Pressure = pressure;
    }

    public override void UnloadCargo()
    {
        // Leave %5 of cargo empty
        CurrentCargoWeight = CurrentCargoWeight * 0.05;
    }

    public override void LoadCargo(double weight)
    {
        if (weight > MaxPayload)
        {
            NotifyHazard($"Attempted to load {weight}kg into {SerialNumber} which exceeds maximum payload of {MaxPayload}kg.");
            throw new OverfillException($"Cannot load {weight}kg into container {SerialNumber}. Maximum payload is {MaxPayload}kg.");
        }
        base.LoadCargo(weight);
    }
    
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"HAZARD NOTIFICATION [{SerialNumber}]: {message}");
    }

    public override string ToString()
    { 
        return $"Gas Container: {SerialNumber}, Pressure: {Pressure} atm, Cargo Mass Weight: {CurrentCargoWeight}kg/{MaxPayload}kg";
    }
}