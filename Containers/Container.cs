namespace ContainerManagement.Containers;

public abstract class Container
{
    private static int counter = 1; // Statik olamasi tekrar tekrar 1'den baslamasini engeller
    public int Height { get; set; }
    public double TareWeight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; protected set; }
    public double MaxPayload { get; set; }
    public double CurrentCargoWeight { get; protected set; }


    public Container(int height, double tareWeight, double depth, string type, double maxPayload)
    {
        SerialNumber = $"CON-{type}-{counter++}"; // Uniq Num
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        CurrentCargoWeight = 0;
    }

    public virtual void LoadCargo(double weight)
    {
        if (weight + CurrentCargoWeight > MaxPayload)
            throw new Exception("Weight cannot be greater than max payload");

        CurrentCargoWeight += weight; 
    }

    public virtual void UnloadCargo()
    {
        CurrentCargoWeight = 0; 
    }

    public virtual double GetTotalWeight()
    {
        return CurrentCargoWeight + TareWeight;
    }

    public override string ToString()
    { 
        return $"Container {SerialNumber}, Cargo: {CurrentCargoWeight}kg/{MaxPayload}kg, Total Weight: {GetTotalWeight()}kg";
    }
}
