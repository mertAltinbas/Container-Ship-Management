using System.Threading.Channels;
using ContainerManagement.Containers;

namespace ContainerManagement;

public class ContainerShip
{
    public string Name { get; set; }
    public double MaxSpeed { get; set; }
    public int MaxContainerCount { get; set; }
    public double MaxWeight { get; set; } // in tons
    private List<Container> _containers;

    public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
        _containers = new List<Container>();
    }

    public bool LoadContainer(Container container)
    {
        if (_containers.Count >= MaxContainerCount)
        {
            Console.WriteLine($"Cannot load container {container.SerialNumber} onto {Name}. Ship is at maximum container capacity");
            return false;
        }
        
        double currentWeight = GetTotalWeight();
        double newWeight = currentWeight + (GetTotalWeight() / 1000); // Convert kg to tons

        if (newWeight > MaxWeight)
        {
            Console.WriteLine($"Cannot load container {container.SerialNumber} onto {Name}. Ship is at maximum container capacity");
            return false;
        }
        
        _containers.Add(container);
        Console.WriteLine($"Container {container.SerialNumber} loaded onto {Name}");
        
        return true;
    }

    public bool LoadContainers(List<Container> containers)
    {
        bool allLoaded = true;
        foreach (Container container in containers)
        {
            if(!LoadContainer(container))
                allLoaded = false;
        }
        return allLoaded;
    }

    public bool RemoveContainer(Container container)
    {
        Container containerToRemove = _containers.Find(c => c.SerialNumber == container.SerialNumber);

        if (containerToRemove != null)
        {
            _containers.Remove(containerToRemove);
            Console.WriteLine($"Container {container.SerialNumber} removed from {Name}");
            return true;
        }

        Console.WriteLine($"Container {container.SerialNumber} not found onto {Name}");
        return false;
    }
    
    public bool ReplaceContainer(string serialNumber, Container newContainer)
    {
        int index = _containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index >= 0)
        {
            Container oldContainer = _containers[index];
            double currentWeight = GetTotalWeight();
            double newWeight = currentWeight - (oldContainer.GetTotalWeight() / 1000) + (newContainer.GetTotalWeight() / 1000);

            if (newWeight > MaxWeight)
            {
                Console.WriteLine($"Cannot replace container {serialNumber} with {newContainer.SerialNumber}. Would exceed maximum weight capacity.");
                return false;
            }

            _containers[index] = newContainer;
            Console.WriteLine($"Container {serialNumber} replaced with {newContainer.SerialNumber} on {Name}.");
            return true;
        }

        Console.WriteLine($"Container {serialNumber} not found on {Name}.");
        return false;
    }
    
    public void PrintContainerInfo(string serialNumber)
    {
        Container container = _containers.Find(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            Console.WriteLine(container);
        }
        else
        {
            Console.WriteLine($"Container {serialNumber} not found on {Name}.");
        }
    }
    
    public double GetTotalWeight()
    {
        double totalWeight = 0;
        foreach (Container container in _containers)
        {
            totalWeight += container.GetTotalWeight();
        }
        return totalWeight / 1000; // Convert kg to tons
    }
}