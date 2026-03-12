using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.GameCode;

public class MainMenu
{
    private readonly Color _buttonColor = Color.LightGray;
    private readonly Color _hoverColor = Color.Gray;
    private readonly Color _textColor = Color.Black;

    private Rectangle _playButton;
    private Rectangle _exitButton;

    public MainMenu()
    {
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        _playButton = new Rectangle(screenWidth / 2f - 100, screenHeight / 2f - 60, 200, 50);
        _exitButton = new Rectangle(screenWidth / 2f - 100, screenHeight / 2f + 10, 200, 50);
    }

    public (bool Play, bool Exit) Update()
    {
        bool playPressed = false;
        bool exitPressed = false;

        Vector2 mousePoint = Raylib.GetMousePosition();

        if (Raylib.CheckCollisionPointRec(mousePoint, _playButton))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                playPressed = true;
            }
        }

        if (Raylib.CheckCollisionPointRec(mousePoint, _exitButton))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                exitPressed = true;
            }
        }

        return (playPressed, exitPressed);
    }

    public void Render()
    {
        Vector2 mousePoint = Raylib.GetMousePosition();

        // Title
        Raylib.DrawText("TETRIS FACTORIO", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("TETRIS FACTORIO", 40) / 2, 100, 40, Color.Gold);

        // Play Button
        Color playColor = Raylib.CheckCollisionPointRec(mousePoint, _playButton) ? _hoverColor : _buttonColor;
        Raylib.DrawRectangleRec(_playButton, playColor);
        Raylib.DrawRectangleLinesEx(_playButton, 2, Color.Black);
        Raylib.DrawText("PLAY", (int)(_playButton.X + _playButton.Width / 2 - Raylib.MeasureText("PLAY", 20) / 2), (int)(_playButton.Y + 15), 20, _textColor);

        // Exit Button
        Color exitColor = Raylib.CheckCollisionPointRec(mousePoint, _exitButton) ? _hoverColor : _buttonColor;
        Raylib.DrawRectangleRec(_exitButton, exitColor);
        Raylib.DrawRectangleLinesEx(_exitButton, 2, Color.Black);
        Raylib.DrawText("EXIT", (int)(_exitButton.X + _exitButton.Width / 2 - Raylib.MeasureText("EXIT", 20) / 2), (int)(_exitButton.Y + 15), 20, _textColor);
    }
}
