using UnityEngine;
public class PlayerController
{
    private PlayerBodyMovement _playerBodyMovement;
    private PlayerRayCast _playerRayCast;
    private Vector2 _moveInputs;
    public bool _isOnCinematic;
    [Header("RunAndMove")]
    private float _walkSpeed;
    private float _runSpeed;
    private bool _canMove;
    [Header("Jump")]
    private bool _isGrounded;
    private bool _triggerJump;
    [Header("ViewEnemy")]
    private bool _viewEnemy;
    [Header("UnderAcid")]
    private bool _isUnderAcid;
    private float _acidSpeedMultiplier = 0.4f;
    private float _normalSpeedMultiplier = 1f;
    public PlayerController(PlayerBodyMovement playerBodyMovement, PlayerRayCast playerRayCast, float walkSpeed, float runSpeed)
    {
        _playerBodyMovement = playerBodyMovement;
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
        _playerRayCast = playerRayCast;
        _canMove = true;
        _isOnCinematic = false;
    }
    private void Running()
    {
        float speedMultiplier = _isUnderAcid ? _acidSpeedMultiplier : _normalSpeedMultiplier;
        if (PlayerInputs.instance.RunAction())
            _playerBodyMovement.ChangeSpeed(_runSpeed * speedMultiplier);
        else
            _playerBodyMovement.ChangeSpeed(_walkSpeed * speedMultiplier);
    }
    public void OnUpdate()
    {
        _moveInputs = PlayerInputs.instance.GetMovement();
        Running();
        if (PlayerInputs.instance.JumpAction())
            _triggerJump = true;
        _isGrounded = _playerRayCast.CheckGrounded();
        _canMove = _playerRayCast.CheckWall();
        _viewEnemy = _playerRayCast.CheckViewEnemy();
        if (PlayerInputs.instance.InteractAction())
            _playerRayCast.CheckInteract();
        _playerRayCast.CheckGlow();
    }
    public void OnFixedUpdate()
    {
        if (_isOnCinematic) return;
        Rigidbody rb = _playerBodyMovement.GetRigidbody();
        if (_isUnderAcid)
        {
            rb.useGravity = false;
            _playerBodyMovement.FloatMove(_moveInputs,_walkSpeed * _acidSpeedMultiplier);
            return;
        }
        rb.useGravity = true;
        if (_canMove)
            _playerBodyMovement.Move(_moveInputs);
        else
            _playerBodyMovement.MoveBlockForward(_moveInputs);
        if (_triggerJump && _isGrounded)
        {
            if (_isUnderAcid)
                _playerBodyMovement.JumpUnderAcid();
            else
                _playerBodyMovement.Jump();
            _triggerJump = false;
        }
    }
    public void SetUnderAcid(bool value)
    {
        Debug.Log("Under acid: " + value);
        _isUnderAcid = value;
    }
    public void Disable()
    {
        _playerBodyMovement = null;
        _playerRayCast = null;
    }
    public bool IsGrounded { get => _isGrounded; }
    public Vector2 GetMoveVector { get => _moveInputs; }
    public bool CanMove { get => _canMove; set => _canMove = value; }
    public bool ViewEnemy { get => _viewEnemy; }
}
