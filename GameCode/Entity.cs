using System.Numerics;

namespace ConsoleApp1.GameCode;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public float Rotation { get; set; }

    protected Entity()
    {
        Position = Vector2.Zero;
        Size = Vector2.One;
        Rotation = 0f;
    }

    protected Entity(Vector2 position, Vector2 size, float rotation = 0f)
    {
        Position = position;
        Size = size;
        Rotation = rotation;
    }

    public abstract void Update(float deltaTime);
    public abstract void Render();
}