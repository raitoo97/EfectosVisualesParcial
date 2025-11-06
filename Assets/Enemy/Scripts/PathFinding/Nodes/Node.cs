using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    [SerializeField]private List<Node> _neighbords = new List<Node>();
    public float cost = 1f;
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
            _neighbords.Add(node);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(this.transform.position, NodeManager._maxDistanceNeighbord);
    }
    public List<Node> GetNeighbords { get { return _neighbords; } }
}
