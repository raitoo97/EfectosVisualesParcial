using Cinemachine;
using UnityEngine;
public class Player : MonoBehaviour
{
    private PlayerInputs _characterInputs;
    private PlayerCameraMovement _playerCameraMovement;
    [SerializeField]private CinemachineVirtualCamera _camera;
    private void OnEnable()
    {
        _characterInputs = new PlayerInputs();
        _playerCameraMovement = new PlayerCameraMovement(_characterInputs, _camera);
    }
    private void Start()
    {
        _playerCameraMovement.OnStart();
    }
    private void Update()
    {
        _playerCameraMovement.OnUpdate();
    }
    private void OnDisable()
    {
        _characterInputs.ConfigOnDisable();
        _characterInputs = null;
        _playerCameraMovement = null;
    }
}
