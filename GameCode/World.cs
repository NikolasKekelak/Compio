using System;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.GameCode;

public enum TileType
{
    Water,
    LimeGrass,
    Sand,
    DessertSand,
    ForestGrass,
    MushroomGrass,
    Mycelia,
    DeadGrass,
    DeadLand
}

public class World
{
    public const int Width = 1000;
    public const int Height = 1000;
    public const int TileSize = 64;
    private const int SpriteTileSize = 16; // Source tile size in the tileset
    private const int TilesPerRow = 16; // 256 / 16

    private readonly TileType[,] _tiles = new TileType[Width, Height];
    private readonly Dictionary<TileType, Texture2D> _textures = new();
    private bool _texturesLoaded = false;
    private readonly WorldGenerator _generator = new();

    private void LoadTextures()
    {
        if (_texturesLoaded) return;
        
        _textures[TileType.Water] = Raylib.LoadTexture("Assets/Tiles/water.png");
        _textures[TileType.LimeGrass] = Raylib.LoadTexture("Assets/Tiles/lime_grass.png");
        _textures[TileType.Sand] = Raylib.LoadTexture("Assets/Tiles/sand.png");
        _textures[TileType.DessertSand] = Raylib.LoadTexture("Assets/Tiles/dessert_sand.png");
        _textures[TileType.ForestGrass] = Raylib.LoadTexture("Assets/Tiles/forest_grass.png");
        _textures[TileType.MushroomGrass] = Raylib.LoadTexture("Assets/Tiles/mushroom_grass.png");
        _textures[TileType.Mycelia] = Raylib.LoadTexture("Assets/Tiles/mycelia.png");
        _textures[TileType.DeadGrass] = Raylib.LoadTexture("Assets/Tiles/dead_grass.png");
        _textures[TileType.DeadLand] = Raylib.LoadTexture("Assets/Tiles/dead_land.png");

        _texturesLoaded = true;
    }

    public void Generate()
    {
        var generatedTiles = _generator.Generate(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _tiles[x, y] = generatedTiles[x, y];
            }
        }
    }

    public void Render(Vector2 cameraOffset)
    {
        LoadTextures();

        // Calculate visible tiles to avoid rendering 1,000,000 rectangles
        int startX = Math.Max(0, (int)(-cameraOffset.X / TileSize));
        int startY = Math.Max(0, (int)(-cameraOffset.Y / TileSize));
        int endX = Math.Min(Width, startX + (Raylib.GetScreenWidth() / TileSize) + 2);
        int endY = Math.Min(Height, startY + (Raylib.GetScreenHeight() / TileSize) + 2);

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                TileType type = _tiles[x, y];
                if (_textures.TryGetValue(type, out Texture2D texture))
                {
                    Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
                    Rectangle dest = new Rectangle(x * TileSize + (int)cameraOffset.X, y * TileSize + (int)cameraOffset.Y, TileSize, TileSize);
                    Raylib.DrawTexturePro(texture, source, dest, Vector2.Zero, 0, Color.White);
                }
            }
        }
    }


    public void Unload()
    {
        if (_texturesLoaded)
        {
            foreach (var texture in _textures.Values)
            {
                Raylib.UnloadTexture(texture);
            }
            _textures.Clear();
            _texturesLoaded = false;
        }
    }
}
