using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1.GameCode;

public static class BuildingCollector
{
    private static readonly Dictionary<string, Type> _buildingMappings = new();

    static BuildingCollector()
    {
        InitializeMappings();
    }

    private static void InitializeMappings()
    {
        _buildingMappings.Clear();

        // Get the current assembly to scan for building classes
        Assembly assembly = Assembly.GetExecutingAssembly();

        // Find all non-abstract types that inherit from BuildingEntity
        var buildingTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BuildingEntity)));

        foreach (var type in buildingTypes)
        {
            _buildingMappings[type.Name] = type;
        }
    }

    public static Type? GetBuildingType(string name)
    {
        if (_buildingMappings.TryGetValue(name, out var type))
            return type;
        
        return null;
    }

    public static IEnumerable<string> GetAvailableBuildings() {
        return _buildingMappings.Keys;
    }
}

public static class BuildingEntityFactory
{
    public static BuildingEntity Create(string buildingName)
    {
        Type? type = BuildingCollector.GetBuildingType(buildingName);

        if (type == null)
        {
            throw new ArgumentException($"Building with name '{buildingName}' does not correspond to any known building type.");
        }

        try
        {
            // Use reflection to create an instance of the building
            // Assuming the building has a parameterless constructor
            BuildingEntity? instance = Activator.CreateInstance(type) as BuildingEntity;

            if (instance == null)
                throw new Exception($"Failed to create instance of '{buildingName}'.");
            

            return instance;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating building '{buildingName}': {ex.Message}", ex);
        }
    }
}
