using System.Collections.Generic;
using UnityEngine;
public enum ConnectionType
{
    Walk,
    Jump
}
public class Node : MonoBehaviour
{
    [SerializeField]private List<Node> _neighbords = new List<Node>();
    public Dictionary<Node, ConnectionType> _connectionTypes = new Dictionary<Node, ConnectionType>();
    public float cost = 1f;
    public bool isJumpNode = false;
    private void Awake()
    {
        NodeManager.RegisterNode(this);
    }
    void Start()
    {
        NodeManager.CompleteNeighbords();
    }
    private void OnDestroy()
    {
        NodeManager.UnregisterNode(this);
    }
    public void AddNeighbord(Node node)
    {
        if (!_neighbords.Contains(node))
        {
            _neighbords.Add(node);
            if (node.isJumpNode)
            {
                _connectionTypes[node] = ConnectionType.Jump;
            }
            else
            {
                _connectionTypes[node] = ConnectionType.Walk;
            }
        }
    }
    public ConnectionType GetConnectionType(Node node)
    {
        if (_connectionTypes.TryGetValue(node, out var type))
            return type;
        return ConnectionType.Walk;
    }
    public List<Node> GetNeighbords { get { return _neighbords; } }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, NodeManager._maxDistanceNeighbord);
    }
}
