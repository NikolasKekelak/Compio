using Raylib_cs;

namespace ConsoleApp1.GameCode.Research;

public class ResearchTree
{
    private List<TierNode> Tiers = new List<TierNode>();
}

public class TierNode()
{
    private Boolean Unlocked = false;
    private ResearchNode RootNode = null;
    private string IconPath = "";
    private string Name = "";
    private string Description = "";
}

public abstract class ResearchNode
{
    private Boolean Unlocked = false;
    private List<ResearchNode> Prerequisites = new List<ResearchNode>();
    private Texture2D? Icon = null;
    private string Name = "";
    private string Description = "";

    public abstract void Unlock();
}