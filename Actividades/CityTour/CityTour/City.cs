namespace CityTour;

public class City
{
    private Dictionary<(int, char), (int, double)> roads;
    
    public City(string path)
    {
        roads = new Dictionary<(int, char), (int, double)>();
        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            // Formato: "nodo1,ruta,nodo2,costo"
            string[] info = line.Split(',');
            int n1 = Convert.ToInt32(info[0]); // nodo de inicio
            char r = Convert.ToChar(info[1]); // nombre ruta
            int n2 = Convert.ToInt32(info[2]); // nodo de destino
            double c = Convert.ToDouble(info[3]); // costo de tomar esta ruta
            roads[(n1, r)] = (n2, c); // del nodo n1 por la ruta r se llega al nodo n2 con un costo c
            roads[(n2, r)] = (n1, c); // las rutas son en ambas direcciones por lo que también se puede ir de n2 a n1
        }
    }
    
    public (bool, double, List<int>) GetTripInfo(int startingNode, string route)
    {
        bool isRouteValid = IsRouteValid(startingNode, route);
        double cost = GetCost(startingNode, route);
        List<int> nodesInRoute = GetNodes(startingNode, route);
        return (isRouteValid, cost, nodesInRoute);
    }

    // Retorna una lista con los ids de los nodos en la ruta
    private List<int> GetNodes(int startingNode, string route)
    {
        List<int> nodes = new List<int>();
        int node = startingNode;
        nodes.Add(node);
        foreach (char road in route)
        {
            if (roads.ContainsKey((node, road)))
            {
                // Si la ruta existe la tomamos
                (int, double) info = roads[(node, road)];
                node = info.Item1; // siguiente nodo si tomamos esta ruta
                nodes.Add(node);
            }
            else
                // Si la ruta no existe su costo es infinito
                return new List<int>();
        }

        return nodes;
    }

    // Retorna el costo de seguir una ruta
    private double GetCost(int startingNode, string route)
    {
        double cost = 0;
        int node = startingNode;
        foreach (char road in route)
        {
            if (roads.ContainsKey((node, road)))
            {
                // Si la ruta existe la tomamos
                (int, double) info = roads[(node, road)];
                node = info.Item1;  // siguiente nodo si tomamos esta ruta
                cost += info.Item2; // costo de tomar esta ruta
            }
            else
                // Si la ruta no existe su costo es infinito
                return double.PositiveInfinity;
        }

        return cost;
    }

    // Una ruta es válida si todos los caminos en ella existen
    private bool IsRouteValid(int startingNode, string route)
    {
        int node = startingNode;
        foreach (char road in route)
        {
            if (roads.ContainsKey((node, road)))
            {
                // Si la ruta existe la tomamos
                (int, double) info = roads[(node, road)];
                node = info.Item1; // siguiente nodo si tomamos esta ruta
            }
            else
                // Si la ruta no existe su costo es infinito
                return false;
        }

        return true;
    }
    
    
}