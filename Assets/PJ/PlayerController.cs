using UnityEngine;
public class PlayerController
{
    private PlayerBodyMovement _playerBodyMovement;
    private PlayerRayCast _playerRayCast;
    private Vector2 _moveInputs;
    [Header("RunAndMove")]
    private float _walkSpeed;
    private float _runSpeed;
    private bool _canMove;
    [Header("Jump")]
    private bool _isGrounded;
    private bool _triggerJump;
    public PlayerController(PlayerBodyMovement playerBodyMovement, PlayerRayCast playerRayCast, float walkSpeed, float runSpeed)
    {
        _playerBodyMovement = playerBodyMovement;
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
        _playerRayCast = playerRayCast;
        _canMove = true;
    }
    private void Running()
    {
        if (PlayerInputs.instance.RunAction())
            _playerBodyMovement.ChangeSpeed(_runSpeed);
        else
            _playerBodyMovement.ChangeSpeed(_walkSpeed);
    }
    public void OnUpdate()
    {
        _moveInputs = PlayerInputs.instance.GetMovement();
        Running();
        if (PlayerInputs.instance.JumpAction())
            _triggerJump = true;
        _isGrounded = _playerRayCast.CheckGrounded();
        _canMove = _playerRayCast.CheckWall();
    }
    public void OnFixedUpdate()
    {
        if (_canMove)
            _playerBodyMovement.Move(_moveInputs);
        else
            _playerBodyMovement.MoveBlockForward(_moveInputs);
        if (_triggerJump && _isGrounded)
        {
            _playerBodyMovement.Jump();
            _triggerJump = false;
        }
    }
    public void Disable()
    {
        _playerBodyMovement = null;
        _playerRayCast = null;
    }
    public bool IsGrounded { get => _isGrounded; }
    public Vector2 GetMoveVector { get => _moveInputs; }
    public bool CanMove { get => _canMove; set => _canMove = value; }
}
