using System.Numerics;
using Raylib_cs;

namespace ConsoleApp1.GameCode;

public class Animation
{
    private readonly Texture2D[] _frames;
    private readonly float _frameTime;
    private float _timer;
    private int _currentFrame;
    private bool _isLooping;
    private int _loopsCompleted;

    public Animation(string[] framePaths, float fps, bool isLooping = true)
    {
        _frames = new Texture2D[framePaths.Length];
        for (int i = 0; i < framePaths.Length; i++)
        {
            _frames[i] = Raylib.LoadTexture(System.IO.Path.Combine("Assets", "Sprites", "Player", framePaths[i]));
        }
        _frameTime = 1.0f / fps;
        _isLooping = isLooping;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer >= _frameTime)
        {
            _timer = 0;
            _currentFrame++;
            if (_currentFrame >= _frames.Length)
            {
                if (_isLooping)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame = _frames.Length - 1;
                }
                _loopsCompleted++;
            }
        }
    }

    public void Draw(Vector2 position, Vector2 size, float rotation, Color color)
    {
        Texture2D texture = _frames[_currentFrame];
        Rectangle sourceRec = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle destRec = new Rectangle(position.X - size.X / 2, position.Y, size.X, size.Y);
        Vector2 origin = Vector2.Zero; // Or size / 2 if we want center rotation
        Raylib.DrawTexturePro(texture, sourceRec, destRec, origin, rotation, color);
    }

    public void Reset()
    {
        _currentFrame = 0;
        _timer = 0;
        _loopsCompleted = 0;
    }

    public int LoopsCompleted => _loopsCompleted;
    public bool IsFinished => !_isLooping && _currentFrame == _frames.Length - 1;
    public int CurrentFrame => _currentFrame;
    public int FrameCount => _frames.Length;

    public void Unload()
    {
        foreach (var texture in _frames)
        {
            Raylib.UnloadTexture(texture);
        }
    }
}
