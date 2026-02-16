using UnityEngine;
public abstract class Agent : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField]protected float _maxVelocity;
    [SerializeField]protected float _maxSteerForce;
    [SerializeField]protected bool _canMove;
    [Range(0f,10f)][SerializeField]protected float _SeparationRange;
    [Range(0f,10f)][SerializeField]protected float _weightSeparation;
    [Range(0f,10f)][SerializeField]protected float _WeightSeek;
    [SerializeField]protected float _gravityForce;
    [SerializeField]protected LayerMask _groundMask;
    protected virtual void Start()
    {
        _canMove = true;
    }
    public void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
    }
    protected virtual void Update()
    {
        HandleGroundAdherence();
        if (!_canMove) return;
        if (_velocity.magnitude < 0.1f)
            _velocity += transform.forward * 0.3f;
        transform.position += _velocity * Time.deltaTime;
    }
    private void HandleGroundAdherence()
    {
        Vector3 rayOrigin = transform.position + Vector3.up;
        Vector3 rayDirection = Vector3.down;
        float rayLength = 3f;
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength, _groundMask))
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, hit.point.y, Time.deltaTime * 50f);
            transform.position = pos;
            _velocity.y = 0;
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);
            _velocity += Vector3.down * _gravityForce * 20 * Time.deltaTime;
        }
    }
    public void FlockingAndSeek(Vector3 target)
    {
        Vector3 seekForce = Seek(target) * _WeightSeek;
        Vector3 sepForce = Separation(_SeparationRange) * _weightSeparation;
        AddForce(seekForce + sepForce);
    }
    public void ApplySeparation(Vector3 force)
    {
        force.y = 0;
        bool hitWall = Physics.Raycast(transform.position, force.normalized, out RaycastHit hit, 0.4f, LayerMask.GetMask("Wall"));
        if (!hitWall)
        {
            AddForce(force);
        }
        Debug.DrawRay(transform.position, force.normalized * 0.4f, hitWall ? Color.red : Color.green);
    }
    public Vector3 GetSeparationForce()
    {
        return Separation(_SeparationRange) * _weightSeparation;
    }
    public void ChangeMove(bool canMove)
    {
        _canMove = canMove;
    }
    protected Vector3 Seek(Vector3 target)
    {
        var desired = (target - transform.position).normalized;
        desired *= _maxVelocity;
        var steer = desired - _velocity;
        steer = Vector3.ClampMagnitude(steer, _maxSteerForce);
        return steer;
    }
    protected Vector3 Separation(float range)
    {
        Collider[] neighbors = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        if (neighbors.Length == 0) return Vector3.zero;
        var desired = Vector3.zero;
        int count = 0;
        foreach (var obj in neighbors)
        {
            if (obj.gameObject == this.gameObject) continue;
            Vector3 dir = transform.position - obj.transform.position;
            dir.y = 0;
            if (dir.magnitude < range)
            {
                desired += dir;
                count++;
            }
        }
        if (count == 0) return Vector3.zero;
        desired /= count;
        desired = desired.normalized * _maxVelocity;
        Vector3 steer = desired - _velocity;
        steer = Vector3.ClampMagnitude(steer, _maxSteerForce);
        return steer;
    }
    public void StopMovement()
    {
        _velocity = Vector3.zero;
    }
    public Vector3 Velocity { get => _velocity; }
    public float MaxVelocity { get => _maxVelocity; }
    public float MaxForce { get => _maxSteerForce; }
}
