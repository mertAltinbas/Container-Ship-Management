using ContainerManagement.Exceptions;

namespace ContainerManagement.Containers;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }
    
    // Dictionary to store product types and their required temperatures
    private static Dictionary<string, double> _productRequirements = new Dictionary<string, double>
    {
        {"Bananas", 13.3},
        { "Chocolate", 18.0 },
        { "Fish", 2.0 },
        { "Meat", -15.0 },
        { "Ice cream", -18.0 },
        { "Frozen pizza", -30.0 },
        { "Cheese", 7.0 },
        { "Sausage", 5 },
        { "Butter", 20.5},
        { "Eggs", 19}
    };
    
    public RefrigeratedContainer(int height, double tareWeight, double depth, double maxPayload, string productType, double temperature) 
        : base(height, tareWeight, depth, "R", maxPayload)
    {
        ProductType = productType;
        Temperature = temperature;
        if (_productRequirements.ContainsKey(productType))
        {
            if (temperature > _productRequirements[productType])
            {
                throw new ArgumentException($"Temperature ({temperature} Celsius) is not cold enough for {productType} which requires {_productRequirements[productType]} or more");
            }
        }
    }
    
    public override void LoadCargo(double cargoWeight)
    {
        if (cargoWeight > MaxPayload)
        {
            throw new OverfillException($"Cannot load {cargoWeight}kg into container {SerialNumber}. Maximum payload is {MaxPayload}kg.");
        }

        base.LoadCargo(cargoWeight);
    }
    
    public override string ToString()
    {
        return $"Refrigerated Container {SerialNumber}, Product: {ProductType}, Temp: {Temperature}°C, Cargo: {CurrentCargoWeight}kg/{MaxPayload}kg";
    }
}