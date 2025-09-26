using Cinemachine;
using UnityEngine;
public class PlayerCameraMovement : CinemachineExtension
{
    private PlayerInputs _characterInputs;
    private Vector3 _startingRotation;
    [SerializeField]private float ClampleAngle = 80f;
    [SerializeField]private float Sensitivity = 10f;
    private Player _player;
    protected override void Awake()
    {
        base.Awake();
        _characterInputs = PlayerInputs.instance;
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (GameManager.instance != null)
            _player = GameManager.instance.player;
        if (_player?.GetPlayerController?._isOnCinematic == true) return;
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startingRotation == Vector3.zero)
                    _startingRotation = transform.localRotation.eulerAngles;
                if (_characterInputs == null) return;
                Vector2 mouseMovement = _characterInputs.GetMouseMovement();
                _startingRotation.x += mouseMovement.x * Sensitivity * Time.deltaTime;
                _startingRotation.y += mouseMovement.y * Sensitivity *Time.deltaTime;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -ClampleAngle, ClampleAngle);
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x,0f);
            }
        }
    }
}
