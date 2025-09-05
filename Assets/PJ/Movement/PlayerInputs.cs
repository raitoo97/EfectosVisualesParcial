using System;
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
        return _playerMap.MoveMap.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseMovement()
    {
        return _playerMap.MoveMap.Rotate.ReadValue<Vector2>();
    }
    public bool GetInteract()
    {
        return _playerMap.MoveMap.Interact.IsPressed();
    }
    private void OnDisable()
    {
        _playerMap.Disable();
        _playerMap = null;
    }
}
