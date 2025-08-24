using Cinemachine;
using UnityEngine;
public class PlayerCameraMovement
{
    private PlayerInputs _characterInputs;
    private CinemachineVirtualCamera _camera;
    float sensitivity = 100f;
    public PlayerCameraMovement(PlayerInputs _characterInputs , CinemachineVirtualCamera _camera)
    {
        this._characterInputs = _characterInputs;
        this._camera = _camera;
    }
    public void OnStart()
    {
        var config = _camera.GetCinemachineComponent<CinemachinePOV>();
        config.m_VerticalAxis.m_MaxValue = 30;
        config.m_VerticalAxis.m_MinValue = -30;
        config.m_VerticalAxis.m_InvertInput = false;
    }
    public void OnUpdate()
    {
        Vector2 lookDelta = _characterInputs.GetMouseMovement();
        var config = _camera.GetCinemachineComponent<CinemachinePOV>();
        config.m_HorizontalAxis.Value += lookDelta.x * sensitivity * Time.deltaTime;
        config.m_VerticalAxis.Value -= lookDelta.y * sensitivity * Time.deltaTime;
    }
}
