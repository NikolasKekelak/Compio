using ConsoleApp1.GameCode.Items;

namespace ConsoleApp1.GameCode.Building.Interfaces;

public interface LiquidInput {
    int insertLiquid(Liquid liquid, int amount);
}