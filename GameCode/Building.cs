using System.Numerics;
using ConsoleApp1.GameCode.Items;

namespace ConsoleApp1.GameCode;

public abstract class BuildingEntity : Entity
{
    protected BuildingEntity() : base()
    {
    }

    protected BuildingEntity(Vector2 position, Vector2 size, float rotation = 0f) 
        : base(position, size, rotation)
    {
    }
}