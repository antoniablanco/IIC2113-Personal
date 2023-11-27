using CityTour.Exceptions;

namespace CityTour;

public class City
{
    private Dictionary<(int, char), (int, double)> _roads;
    
    public City(string path)
    {
        _roads = new Dictionary<(int, char), (int, double)>();
        string[] graph = File.ReadAllLines(path);
        foreach (string arc in graph)
        {   
            string[] infoOfArc = arc.Split(',');
            AddInfoOfArcToRoads(infoOfArc);
        }
    }
    
    // Formato: "nodo1,ruta,nodo2,costo"
    private void AddInfoOfArcToRoads(string[] infoOfArc)
    {
        int startNode = Convert.ToInt32(infoOfArc[0]);
        char route = Convert.ToChar(infoOfArc[1]);
        int finalNode = Convert.ToInt32(infoOfArc[2]); 
        double costs = Convert.ToDouble(infoOfArc[3]); 
            
        AddRoadToNodes(startNode, finalNode, route, costs);
    }
    
    private void AddRoadToNodes(int nodeOne, int nodeTwo, char route, double cost)
    {
        AddRoadFromNodeOneToNodeTwo(nodeOne, nodeTwo, route, cost);
        AddRoadFromNodeOneToNodeTwo(nodeTwo, nodeOne, route, cost);
    }
    
    private void AddRoadFromNodeOneToNodeTwo(int nodeOne, int nodeTwo, char route, double cost)
    {
        _roads[(nodeOne, route)] = (nodeTwo, cost);
    }
    
    public (bool, double, List<int>) GetTripInfo(int startingNode, string route)
    {
        try
        {   
            ValidRoute validRoute = new ValidRoute(_roads, startingNode);
            return validRoute.ValidRouteInfo(route);
        }
        catch (RouteNotValidException e)
        {   
            return (false, double.PositiveInfinity, new List<int>());
        }
    }
}