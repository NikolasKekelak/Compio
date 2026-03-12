using System;

namespace ConsoleApp1.GameCode;

public class WorldGenerator
{
    private readonly Random _rand = new();
    private float _seedX;
    private float _seedY;
    private float _seedMoistureX;
    private float _seedMoistureY;

    public WorldGenerator()
    {
        _seedX = _rand.Next(0, 100000);
        _seedY = _rand.Next(0, 100000);
        _seedMoistureX = _rand.Next(0, 100000);
        _seedMoistureY = _rand.Next(0, 100000);
    }

    public TileType[,] Generate(int width, int height)
    {
        TileType[,] tiles = new TileType[width, height];
        float elevationScale = 0.02f;
        float moistureScale = 0.03f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float elevation = GetNoise(x, y, elevationScale, _seedX, _seedY);
                float moisture = GetNoise(x, y, moistureScale, _seedMoistureX, _seedMoistureY);

                tiles[x, y] = DetermineTileType(elevation, moisture);
            }
        }

        return tiles;
    }

    private float GetNoise(int x, int y, float scale, float sx, float sy)
    {
        float n = (float)(Math.Sin((x + sx) * scale) + Math.Sin((y + sy) * scale));
        n += (float)(Math.Sin((x + sx) * scale * 2.5f) * 0.5f + Math.Sin((y + sy) * scale * 2.5f) * 0.5f);
        n += (float)(Math.Sin((x + sx) * scale * 5.0f) * 0.25f + Math.Sin((y + sy) * scale * 5.0f) * 0.25f);
        
        // Normalize roughly to 0..1
        return (n + 3.5f) / 7.0f;
    }

    private TileType DetermineTileType(float elevation, float moisture)
    {
        // Low elevation is water
        if (elevation < 0.35f) return TileType.Water;
        
        // Just above water is beach (sand)
        if (elevation < 0.42f) return TileType.Sand;

        // Higher elevations depend on moisture
        if (moisture < 0.25f)
        {
            // Very Dry -> Mushroom Biome
            return TileType.MushroomGrass;
        }
        else if (moisture < 0.45f)
        {
            // Grass lands - Lime Grass
            return TileType.LimeGrass;
        }
        else if (moisture < 0.65f)
        {
            // Desert or something?
            return TileType.DessertSand;
        }
        else
        {
            // Forest Biome - mycelia and dark grass
            if (moisture > 0.85f) return TileType.Mycelia;
            if (moisture > 0.75f) return TileType.ForestGrass;
            return TileType.DeadGrass;
        }
    }
}
