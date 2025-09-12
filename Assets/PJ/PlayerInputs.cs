using UnityEngine;
public class PlayerInputs : MonoBehaviour
{
    private PlayerMap _playerMap;
    public static PlayerInputs instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        _playerMap = new PlayerMap();
        _playerMap.Enable();
    }
    public Vector2 GetMovement()
    {
        return _playerMap.PlayerInputs.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseMovement()
    {
        return _playerMap.PlayerInputs.Rotate.ReadValue<Vector2>();
    }
    public bool ShootAction()
    {
        return _playerMap.PlayerInputs.Interact.IsPressed();
    }
    public bool RunAction()
    {
        return _playerMap.PlayerInputs.Run.IsPressed();
    }
    public bool JumpAction()
    {
        return _playerMap.PlayerInputs.Jump.triggered;
    }
    private void OnDisable()
    {
        _playerMap.Disable();
        _playerMap = null;
    }
}
