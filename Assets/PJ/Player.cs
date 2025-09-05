using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class Player : MonoBehaviour
{
    private PlayerBodyMovement _playerBodyMovement;
    private Vector2 _moveInputs;
    private float _walkSpeed = 5f;
    private float _runSpeed = 10;
    private void OnEnable()
    {
        _playerBodyMovement = new PlayerBodyMovement(GetComponent<Rigidbody>(), _walkSpeed);
    }
    private void Update()
    {
        _moveInputs = PlayerInputs.instance.GetMovement();
        Running();
    }
    private void FixedUpdate()
    {
        _playerBodyMovement.Move(_moveInputs);
    }
    private void Running()
    {
        if (PlayerInputs.instance.IsRunning())
            _playerBodyMovement.ChangeSpeed(_runSpeed);
        else
            _playerBodyMovement.ChangeSpeed(_walkSpeed);
    }
    private void OnDisable()
    {
        _playerBodyMovement = null;
    }
}
