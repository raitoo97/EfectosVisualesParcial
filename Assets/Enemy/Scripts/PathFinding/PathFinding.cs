using System.Collections.Generic;
public static class PathFinding
{
    public static List<Node> CalculateDijkstra(Node start, Node goal)
    {
        var frontier = new PriorityQueue<Node>();
        var cameFrom = new Dictionary<Node, Node>();
        var costSoFar = new Dictionary<Node, float>();
        frontier.Enqueue(start, 0);
        cameFrom.Add(start, null);
        costSoFar.Add(start, 0);
        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            if(current == goal)
            {
                var path = new List<Node>();
                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }
            foreach (var node in current.GetNeighbords)
            {
                var newCost = costSoFar[current] + node.cost;
                if (!cameFrom.ContainsKey(node))
                {
                    frontier.Enqueue(node, newCost);
                    costSoFar.Add(node,newCost);
                    cameFrom.Add(node,current);
                }
                else if (costSoFar[node] > newCost)
                {
                    cameFrom[node] = current;
                    costSoFar[node] = newCost;
                    frontier.Enqueue(node, newCost);
                }
            }
        }
            return new List<Node>();
    }
}
