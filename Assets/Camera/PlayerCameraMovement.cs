using Cinemachine;
using UnityEngine;
public class PlayerCameraMovement : CinemachineExtension
{
    private PlayerInputs _characterInputs;
    private Vector3 _startingRotation;
    [SerializeField] private float ClampleAngle;
    [SerializeField] private float Sensitivity;
    private Player _player;
    protected override void Awake()
    {
        base.Awake();
        ClampleAngle = 60f;
        Sensitivity = 0.35f;
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
                _startingRotation.x += mouseMovement.x * Sensitivity;
                _startingRotation.y += mouseMovement.y * Sensitivity; 
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -ClampleAngle, ClampleAngle);
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x,0f);
            }
        }
    }
    public void SlerpToDirection(Vector3 targetDirection, float speed)
    {
        Quaternion currentRotation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion smoothRotation = Quaternion.Slerp(currentRotation,targetRotation,speed * Time.deltaTime);
        Vector3 euler = smoothRotation.eulerAngles;
        _startingRotation.x = euler.y;
        _startingRotation.y = -NormalizeAngle(euler.x);
        _startingRotation.y = Mathf.Clamp(_startingRotation.y, -ClampleAngle, ClampleAngle);
    }
    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }
    public void SnapToDirection(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Vector3 euler = targetRotation.eulerAngles;
        _startingRotation.x = euler.y;
        _startingRotation.y = -NormalizeAngle(euler.x);
        _startingRotation.y = Mathf.Clamp(_startingRotation.y, -ClampleAngle, ClampleAngle);
    }
    public Vector3 GetCurrentForward()
    {
        return Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f) * Vector3.forward;
    }
}
