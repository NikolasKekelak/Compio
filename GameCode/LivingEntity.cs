using System.Numerics;

namespace ConsoleApp1.GameCode;

public abstract class LivingEntity : Entity
{
    public float MovementSpeed { get; set; }

    protected LivingEntity() : base()
    {
        MovementSpeed = 100f; // Default speed
    }

    protected LivingEntity(Vector2 position, Vector2 size, float movementSpeed = 100f, float rotation = 0f) 
        : base(position, size, rotation)
    {
        MovementSpeed = movementSpeed;
    }
}
