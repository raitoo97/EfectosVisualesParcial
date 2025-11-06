using UnityEngine;
public abstract class Agent : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField]protected float _maxVelocity;
    [SerializeField]protected float _maxSteerForce;
    [SerializeField]protected bool _canMove;
    [SerializeField]protected float _SeparationRange;
    [SerializeField]protected float _weightSeparation;
    [SerializeField]protected float _WeightSeek;
    protected virtual void Start()
    {
        _canMove = true;
    }
    protected void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
    }
    protected virtual void Update()
    {
        if(!_canMove)return;
        if (_velocity.magnitude < 0.1f)
        {
            _velocity += transform.forward * 0.3f;
        }
        transform.position += _velocity * Time.deltaTime;
    }
    public void FlockingAndSeek(Vector3 target)
    {
        Vector3 seekForce = Seek(target) * _WeightSeek;
        Vector3 sepForce = Separation(_SeparationRange) * _weightSeparation;
        AddForce(seekForce + sepForce);
    }
    public void ApplySeparation(Vector3 force)
    {
        AddForce(force);
        transform.position += _velocity * Time.deltaTime;
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
}
