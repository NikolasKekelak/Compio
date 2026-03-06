namespace ConsoleApp1.GameCode.Building.Interfaces;

public interface EnergyConsumtion
{
    Boolean canCosumeEnergy();
    void consumeEnergy();
    void reserveEnergy();
}