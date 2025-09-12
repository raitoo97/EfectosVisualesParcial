using UnityEngine;
public class PlayerBodyMovement
{
    private Rigidbody _rigidbody;
    private float _speed;
    private Transform _cameraTransform;
    private float _jumpForce = 7.5f;
    public PlayerBodyMovement(Rigidbody _rigidbody,float _speed)
    {
        this._rigidbody = _rigidbody;
        _cameraTransform = Camera.main.transform;
        this._speed = _speed;
    }
    public void Move(Vector2 MoveVector)
    {
        Vector3 moveVector = new Vector3(MoveVector.x, 0, MoveVector.y);
        moveVector = _cameraTransform.forward * moveVector.z + _cameraTransform.right * moveVector.x;
        moveVector.y = 0f;
        _rigidbody.MovePosition(_rigidbody.position + moveVector.normalized * _speed * Time.fixedDeltaTime);
    }
    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
    public void ChangeSpeed(float _speed)
    {
        this._speed = _speed;
    }
}
