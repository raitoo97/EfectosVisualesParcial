using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class Player : MonoBehaviour
{
    private PlayerController _playerController;
    [Header("Jump")]
    [SerializeField]private Transform _groundChecker;
    [SerializeField]private LayerMask _groundLayer;
    private void OnEnable()
    {
        _playerController = new PlayerController(GetComponent<Rigidbody>(), _groundChecker, _groundLayer);
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
    }
    public bool IsGrounded { get => _playerController.IsGrounded; }
}
