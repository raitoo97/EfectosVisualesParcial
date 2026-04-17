using System.Collections.Generic;
using UnityEngine;
public static class PathFinding
{
    public static List<Vector3> CalculateAStar(Vector3 start, Vector3 goal)
    {
        Node startNode = NodeManager.GetClosetNode(start);
        Node endNode = NodeManager.GetClosetNode(goal);
        var frontier = new PriorityQueue<Node>();
        var cameFrom = new Dictionary<Node, Node>();
        var costSoFar = new Dictionary<Node, float>();
        frontier.Enqueue(startNode, 0);
        cameFrom.Add(startNode, null);
        costSoFar.Add(startNode, 0);
        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            if(current == endNode)
            {
                var path = new List<Vector3>();
                while (current != null)
                {
                    path.Add(current.transform.position);
                    current = cameFrom[current];
                }
                path.Reverse();
                path.Add(goal);
                return path;
            }
            foreach (var node in current.GetNeighbords)
            {
                var newCost = costSoFar[current] + node.cost;
                var distance = Vector3.Distance(node.transform.position, goal);
                var priority = newCost + distance;
                if (!cameFrom.ContainsKey(node))
                {
                    frontier.Enqueue(node, priority);
                    costSoFar.Add(node, newCost);
                    cameFrom.Add(node,current);
                }
                else if (costSoFar[node] > newCost)
                {
                    cameFrom[node] = current;
                    costSoFar[node] = newCost;
                    frontier.Enqueue(node, priority);
                }
            }
        }
            return new List<Vector3>();
    }
    public static List<Vector3>CalculateTheta(Vector3 start, Vector3 goal)
    {
        var aStart = CalculateAStar(start, goal);
        int current = 0;
        while (current + 2 < aStart.Count)
        {
            if (LineOfSight.IsOnSight(aStart[current], aStart[current + 2]))
                aStart.RemoveAt(current + 1);
            else
                current++;
        }
        return aStart;
    }
}