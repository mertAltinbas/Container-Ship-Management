using ContainerManagement;
using ContainerManagement.Containers;
using ContainerManagement.Exceptions;

Console.WriteLine("Container Management System");
Console.WriteLine("===========================");

ContainerShip ship1 = new ContainerShip("Bober", 24.5, 100, 40000);
ContainerShip ship2 = new ContainerShip("Bob", 22.8, 120, 50000);

Console.WriteLine("\nCreated Ships: ");
Console.WriteLine("\nCreating containers...");

// Liquid containers
LiquidContainer milk = new LiquidContainer(250, 250, 500, 3000, false);
LiquidContainer fuel = new LiquidContainer(250, 250, 600, 3500, true);

 // Load cargo into liquid containers
Console.WriteLine("\nLoading cargo into liquid containers...");
milk.LoadCargo(2700); // 90% of capacity
try
{
    fuel.LoadCargo(2000); // Over 50% of capacity for hazardous
}
catch (OverfillException ex)
{
    Console.WriteLine($"Caught exception: {ex.Message}");
    fuel.LoadCargo(1700); // 48.5% of capacity
}

// Gas container
GasContainer helium = new GasContainer(220, 220, 400, 2000, 200);
helium.LoadCargo(1800);

// Refrigerated containers
RefrigeratedContainer bananas = new RefrigeratedContainer(250, 250, 800, 4000, "Bananas", 12.5);
RefrigeratedContainer iceCream = new RefrigeratedContainer(250, 250, 800, 3500, "Ice cream", -20.0);

bananas.LoadCargo(3500);
iceCream.LoadCargo(3000);

// Print container info
Console.WriteLine("\nContainer information:");
Console.WriteLine(milk);
Console.WriteLine(fuel);
Console.WriteLine(helium);
Console.WriteLine(bananas);
Console.WriteLine(iceCream);

// Load containers onto ship
Console.WriteLine("\nLoading containers onto Ship 1...");
ship1.LoadContainer(milk);
ship1.LoadContainer(fuel);
ship1.LoadContainer(helium);

Console.WriteLine("\nLoading containers onto Ship 2...");
ship2.LoadContainer(bananas);
ship2.LoadContainer(iceCream);
