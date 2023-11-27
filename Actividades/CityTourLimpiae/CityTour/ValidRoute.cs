using CityTour.Exceptions;

namespace CityTour;

public class ValidRoute
{   
    private Dictionary<(int, char), (int, double)> _roads;

    private Nodes _nodes;
    
    public ValidRoute(Dictionary<(int, char), (int, double)> roads, int startNode)
    {
        _roads = roads;
        _nodes = new Nodes(startNode);
    }
    
    public (bool, double, List<int>) ValidRouteInfo(string route)
    {
        bool isRouteValid = true;
        double cost = GetCostOfCompleteThRoute(route);
        List<int> nodesInRoute = _nodes.GetNodes();

        return (isRouteValid, cost, nodesInRoute);
    }
    

    private double GetCostOfCompleteThRoute(string route)
    {
        double costOfTheRoute = 0;
        foreach (char road in route)
        {
            costOfTheRoute += GetRoadCost(_nodes.GetLastNode(), road);
        }

        return costOfTheRoute;
    }

    
    private double GetRoadCost(int currentNode, char road)
    {   
        if (DoesTheRouteExist(_nodes.GetLastNode(), road))
        {   
            (int, double) info = _roads[(currentNode, road)];
            int nextNode = info.Item1;
            _nodes.AddNode(nextNode);
            
            return info.Item2;
        }
        else
            throw new RouteNotValidException($"La ruta no es válida");
    }

    private bool DoesTheRouteExist(int node, char road)
    {
        return _roads.ContainsKey((node, road));
    }
}