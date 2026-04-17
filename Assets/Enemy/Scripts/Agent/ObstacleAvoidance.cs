using UnityEngine;
[RequireComponent(typeof(Agent))]
public class ObstacleAvoidance : MonoBehaviour
{
    private Agent _agent;
    [SerializeField]private LayerMask _obstacleMask;
    [SerializeField]private float _priority;
    private void Awake()
    {
        _agent = GetComponent<Agent>();
    }
    private void Update()
    {
        _agent.AddForce(ApplyObstacleAvoidance() * _priority);
    }
    private Vector3 ApplyObstacleAvoidance()
    {
        Vector3 pos = transform.position;
        Vector3 dir = transform.forward;
        float distance = _agent.Velocity.magnitude;
        if(Physics.SphereCast(pos,0.5f,dir,out var hit, distance, _obstacleMask))
        {
            var obstacle = hit.transform;
            var dirToObstacle = obstacle.position - pos;
            float angle = Vector3.SignedAngle(dir, dirToObstacle, Vector3.up);
            var desired = angle >= 0 ? -transform.right : transform.right;
            desired *= _agent.MaxVelocity;
            var steer = desired - _agent.Velocity;
            steer = Vector3.ClampMagnitude(steer, _agent.MaxForce);
            return steer;
        }
        return Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        if (_agent == null) return;
        float radius = 0.5f;
        Vector3 origin = transform.position;
        Vector3 dir = transform.forward;
        float distance = _agent.Velocity.magnitude;
        Vector3 forwardSpherePos = origin + dir * distance;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawWireSphere(forwardSpherePos, radius);
        Gizmos.DrawLine(origin, forwardSpherePos);
    }
}