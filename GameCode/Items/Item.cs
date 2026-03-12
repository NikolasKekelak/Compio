using System.Runtime.Intrinsics.Arm;
using Raylib_cs;

namespace ConsoleApp1.GameCode.Items;

public enum ItemKind {
    Resource,
    Fuel,
    Component,
    Consumable,
    Ingot
}

//Lightweight class for managing minimalistic memory requirements
public class ItemDescriptor {
    public string Name { get; }
    public ItemKind Kind { get; }
    public Texture2D Sprite { get; }
    public String Path { get; }
    public int Energy { get; }
    
    public ItemDescriptor(string name,ItemKind kind,string path, int energy) {
        Name = name;
        Kind = kind;
        Path = path;
        //sprite sa fetchne
        Energy = energy;
    }

    public Boolean isBurnable() {
        return Energy > 0;
    }
}

public class Item {
    public ushort Id { get; }
    public ushort Amount { get; }
}


public class InvalidDescribtorID : Exception {}

//Lightweight class for managing minimalistic memory requirements
public class ItemLightweight {
    private ItemDescriptor[] itemDescriptors = new ItemDescriptor[256];

    public ItemLightweight() {
        
    }
    
    //To be added
    private void Check(ushort id) {
        if (id >= itemDescriptors.Length || itemDescriptors[id] == null) 
            throw new InvalidDescribtorID ();
    }

    public Boolean isValidId(ushort Id) {
        return itemDescriptors[Id] != null;
    }

    public string getName(ushort Id) {
        Check(Id);
        return itemDescriptors[Id].Name;
    }
    public Texture2D getSprite(ushort Id) {
        Check(Id);
        return itemDescriptors[Id].Sprite;
    }
    public ItemDescriptor getDescriptor(ushort Id) {
        Check(Id);
        return itemDescriptors[Id];
    }
    
    public ItemKind getKind(ushort Id) {
        return itemDescriptors[Id].Kind;
    }
    
    public Boolean isBurnable(ushort Id) {
        Check(Id);
        return itemDescriptors[Id].isBurnable();
    }

    public int getEnergy(ushort Id) {
        Check(Id);
        return itemDescriptors[Id].Energy;
    }
}

