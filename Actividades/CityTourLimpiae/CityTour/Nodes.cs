namespace CityTour;

public class Nodes
{   
    private List<int> nodes = new List<int>();

    public Nodes(int startNode)
    {   
        AddNode(startNode);
    }
    
    public void AddNode(int node)
    {
        nodes.Add(node);
    }
    
    public List<int> GetNodes()
    {
        return nodes;
    }
    
    public int GetLastNode()
    {
        return nodes.Last();
    }
}