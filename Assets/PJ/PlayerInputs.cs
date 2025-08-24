using UnityEngine;
public class PlayerInputs
{
    private PlayerMap _playerMap;
    public PlayerInputs()
    {
        ConfigOnEnable();
    }
    private void ConfigOnEnable()
    {
        _playerMap = new PlayerMap();
        _playerMap.Enable();
    }
    public Vector2 GetMovement()
    {
        return _playerMap.MoveMap.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseMovement()
    {
        return _playerMap.MoveMap.Rotate.ReadValue<Vector2>();
    }
    public void ConfigOnDisable()
    {
        _playerMap.Disable();
        _playerMap = null;
    }
}
