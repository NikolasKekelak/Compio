using Raylib_cs;
using System.Numerics;
using ConsoleApp1.GameCode;

Raylib.InitWindow(800, 480, "Game GUI");

GameState currentState = GameState.Menu;
MainMenu mainMenu = new MainMenu();
World world = new World();

// Player with 2 height : 1 width ratio.
// For example, width = 64, height = 128 (2 tiles big).
Vector2 playerSpawn = new Vector2(World.Width * World.TileSize / 2f, World.Height * World.TileSize / 2f);
Player player = new Player(playerSpawn, new Vector2(World.TileSize, World.TileSize * 2));

Game game = new Game();

while (!Raylib.WindowShouldClose())
{
    float deltaTime = Raylib.GetFrameTime();
    
    if (currentState == GameState.Menu)
    {
        var result = mainMenu.Update();
        if (result.Play)
        {
            world.Generate();
            currentState = GameState.Playing;
            game.Start();
        }
        else if (result.Exit)
        {
            break;
        }
    }
    else
    {
        player.Update(deltaTime);
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Lime);

    if (currentState == GameState.Menu)
    {
        mainMenu.Render();
    }
    else
    {
        // Simple camera logic - center on player
        Vector2 cameraOffset = new Vector2(Raylib.GetScreenWidth() / 2f, Raylib.GetScreenHeight() / 2f) - player.Position;
        
        world.Render(cameraOffset);
        
        // Render player at world position + offset
        Vector2 screenPlayerPos = player.Position + cameraOffset;
        player.RenderAt(screenPlayerPos);
        
        PlayerController.CursorController.Render();
    }

    Raylib.EndDrawing();
}

game.Stop();
world.Unload();
Raylib.CloseWindow();

enum GameState
{
    Menu,
    Playing
}