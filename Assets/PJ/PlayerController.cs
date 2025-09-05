using UnityEngine;
public class PlayerController
{
    private PlayerBodyMovement _playerBodyMovement;
    private Vector2 _moveInputs;
    [Header("Run")]
    private float _walkSpeed = 5f;
    private float _runSpeed = 10;
    [Header("Jump")]
    private Transform _groundChecker;
    private LayerMask _groundLayer;
    private float radiusChecker = 0.5f;
    private bool _isGrounded;
    private bool _triggerJump;
    public PlayerController(Rigidbody _rigidbody, Transform _groundChecker, LayerMask _groundLayer)
    {
        _playerBodyMovement = new PlayerBodyMovement(_rigidbody, _walkSpeed);
        this._groundChecker = _groundChecker;
        this._groundLayer = _groundLayer;
    }
    private void Running()
    {
        if (PlayerInputs.instance.RunAction())
            _playerBodyMovement.ChangeSpeed(_runSpeed);
        else
            _playerBodyMovement.ChangeSpeed(_walkSpeed);
    }
    private bool CheckGrounded()
    {
        return Physics.CheckSphere(_groundChecker.position, radiusChecker, _groundLayer);
    }
    public void OnUpdate()
    {
        _moveInputs = PlayerInputs.instance.GetMovement();
        Running();
        if (PlayerInputs.instance.JumpAction())
            _triggerJump = true;
        _isGrounded = CheckGrounded();
    }
    public void OnFixedUpdate()
    {
        _playerBodyMovement.Move(_moveInputs);
        if (_triggerJump && _isGrounded)
        {
            _playerBodyMovement.Jump();
            _triggerJump = false;
        }
    }
    public void Disable()
    {
        _playerBodyMovement = null;
    }
}
