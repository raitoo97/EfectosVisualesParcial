using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class Player : MonoBehaviour
{
    private PlayerBodyMovement _playerBodyMovement;
    private PlayerRayCast _playerRayCast;
    private PlayerController _playerController;
    [Header("Jump")]
    [SerializeField]private Transform _groundChecker;
    [SerializeField]private LayerMask _groundLayer;
    [Header("Move")]
    [SerializeField]private LayerMask _wallLayer;
    private float _rayDistance = 1.5f;
    private float _walkSpeed = 5f;
    private float _runSpeed = 10;
    [Header("ViewEnemy")]
    private float _enemyViewDistance = Mathf.Infinity;
    [SerializeField]private LayerMask _enemyLayer;
    [Header("Interact")]
    private float _interactDistance = 5f;
    private void OnEnable()
    {
        _playerBodyMovement = new PlayerBodyMovement(GetComponent<Rigidbody>(), _walkSpeed);
        _playerRayCast = new PlayerRayCast(Camera.main.transform, _groundChecker, _wallLayer, _groundLayer, _enemyLayer, _rayDistance, _enemyViewDistance, _interactDistance);
        _playerController = new PlayerController(_playerBodyMovement, _playerRayCast, _walkSpeed, _runSpeed);
    }
    private void Update()
    {
        _playerController.OnUpdate();
    }
    private void FixedUpdate()
    {
        _playerController.OnFixedUpdate();
    }
    private void OnDisable()
    {
        _playerController?.Disable();
        if(_playerController != null) _playerController = null;
        if (_playerRayCast != null) _playerRayCast = null;
        if (_playerBodyMovement != null) _playerBodyMovement = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _rayDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _enemyViewDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _interactDistance);
    }
    public bool IsGrounded { get => _playerController.IsGrounded; }
    public Vector2 MoveVector { get => _playerController.GetMoveVector; }
    public PlayerController GetPlayerController { get => _playerController; }
}
