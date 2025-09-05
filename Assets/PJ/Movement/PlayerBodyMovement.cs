using UnityEngine;
public class PlayerBodyMovement
{
    private Rigidbody _rigidbody;
    private float speed;
    private Transform _cameraTransform;
    public PlayerBodyMovement(Rigidbody _rigidbody)
    {
        this._rigidbody = _rigidbody;
        _cameraTransform = Camera.main.transform;
        speed = 5f;
    }
    public void Move()
    {
        Vector3 moveVector = new Vector3(GetInputs().x, 0, GetInputs().y);
        moveVector = _cameraTransform.forward * moveVector.z + _cameraTransform.right * moveVector.x;
        moveVector.y = 0f;
        _rigidbody.MovePosition(_rigidbody.position + moveVector.normalized * speed * Time.fixedDeltaTime);
    }
    public Vector2 GetInputs()
    {
        return PlayerInputs.instance.GetMovement();
    }
}
