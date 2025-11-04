using System.Collections.Generic;
using UnityEngine;
public static class NodeManager
{
    private static List<Node> _allNode = new List<Node>();
    public static void RegisterNode(Node node)
    {
        if (!_allNode.Contains(node))
            _allNode.Add(node);
    }
    public static void UnregisterNode(Node node)
    {
        if (_allNode.Contains(node))
            _allNode.Remove(node);
    }
    public static void CompleteNeighbords()
    {
        if (_allNode.Count <= 0) return;
        foreach (var node in _allNode)
        {
            foreach (var otherNode in _allNode)
            {
                if (otherNode == node) continue;
                if (LineOfSight.IsOnSight(node.transform.position, otherNode.transform.position))
                {
                    node.AddNeighbord(otherNode);
                }
            }
        }
    }
    public static Node GetClosetNode(Vector3 poisition)
    {
        if(_allNode.Count <= 0) return null;
        Node closetNode = null;
        float closetDistance = Mathf.Infinity;
        foreach (var node in _allNode)
        {
            var distance = poisition - node.transform.position; 
            if (distance.magnitude < closetDistance)
            {
                closetDistance = distance.magnitude;
                closetNode = node;
            }
        }
        return closetNode;
    }
}
