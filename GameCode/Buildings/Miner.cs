using ConsoleApp1.GameCode.Building.Interfaces;
using ConsoleApp1.GameCode.Items;

namespace ConsoleApp1.GameCode.Buildings;

public class ElectricMiner : BuildingEntity, Mining, Output, EnergyConsumtion{
    public override void getSpriteLocation()
    {
        throw new NotImplementedException();
    }

    public override void update()
    {
        throw new NotImplementedException();
    }

    public override void render()
    {
        throw new NotImplementedException();
    }

    public Item getOre()
    {
        throw new NotImplementedException();
    }

    public Item outputItem()
    {
        throw new NotImplementedException();
    }

    public bool canCosumeEnergy()
    {
        throw new NotImplementedException();
    }

    public void consumeEnergy()
    {
        throw new NotImplementedException();
    }

    public void reserveEnergy()
    {
        throw new NotImplementedException();
    }
}
