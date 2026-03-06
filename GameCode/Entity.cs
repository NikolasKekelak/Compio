namespace ConsoleApp1.GameCode;

public abstract class Entity
{
    public int x;
    public int y;
    public int width;
    public int height;
    
    public abstract void getSpriteLocation();
    public abstract void update();
    public abstract void render();
}