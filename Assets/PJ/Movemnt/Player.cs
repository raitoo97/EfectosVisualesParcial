using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class Player : MonoBehaviour
{
    private PlayerBodyMovement _playerBodyMovement;
    private void OnEnable()
    {
        _playerBodyMovement = new PlayerBodyMovement(GetComponent<Rigidbody>());
    }
    private void Update()
    {
        _playerBodyMovement.GetInputs();
    }
    private void FixedUpdate()
    {
        _playerBodyMovement.Move();
    }
    private void OnDisable()
    {
        _playerBodyMovement = null;
    }
}
