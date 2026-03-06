using System.Numerics;

namespace ConsoleApp1.GameCode;

public class Player : LivingEntity
{
    public Player() : base()
    {
    }

    public Player(Vector2 position, Vector2 size, float movementSpeed = 100f, float rotation = 0f) 
        : base(position, size, movementSpeed, rotation)
    {
    }

    public override void Update(float deltaTime)
    {
        // Minimalistic implementation: Player-specific logic for movement could go here.
        // For now, it stays empty or with a basic comment to avoid confusion.
    }

    public override void Render()
    {
        // Minimalistic implementation: Player-specific rendering logic could go here.
    }
}
