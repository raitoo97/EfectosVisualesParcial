using System.Collections;
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
    public static Node GetClosetNode(Vector3 poisition)
    {
        return null;
    }
}
