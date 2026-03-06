using ConsoleApp1.GameCode.Building.Interfaces;
using ConsoleApp1.GameCode.Items;
using ConsoleApp1.GameCode.Items.Interfaces;

namespace ConsoleApp1.GameCode.Buildings;

public class CoalFurnace : BuildingEntity, Production, Input,Output, Burning {
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

    public bool canCraft()
    {
        throw new NotImplementedException();
    }

    public void Craft()
    {
        throw new NotImplementedException();
    }

    public void inputItem(Item item)
    {
        throw new NotImplementedException();
    }

    public Item outputItem()
    {
        throw new NotImplementedException();
    }

    public int burn(Burnable burnable)
    {
        throw new NotImplementedException();
    }
}

public class ElectricFurnace : Production, Input, Output, EnergyConsumtion {
    public bool canCraft()
    {
        throw new NotImplementedException();
    }

    public void Craft()
    {
        throw new NotImplementedException();
    }

    public void inputItem(Item item)
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