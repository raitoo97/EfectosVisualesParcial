using UnityEngine;
public abstract class Agent : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField]protected float _maxVelocity;
    [SerializeField]protected float _maxSteerForce;
    [SerializeField] protected bool _canMove;
    protected virtual void Start()
    {
        _canMove = true;
    }
    protected void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
        _velocity.y = 0;
    }
    protected virtual void Update()
    {
        if(!_canMove)return;
        transform.position += _velocity * Time.deltaTime;
    }
    protected Vector3 Seek(Vector3 target)
    {
        var desired = (target - transform.position).normalized;
        desired *= _maxVelocity;
        var steer = desired - _velocity;
        steer = Vector3.ClampMagnitude(steer, _maxSteerForce);
        return steer;
    }
    public void GetSeekForce(Vector3 target)
    {
        AddForce(Seek(target));
    }
    public void ChangeMove(bool canMove)
    {
        _canMove = canMove;
    }
}
