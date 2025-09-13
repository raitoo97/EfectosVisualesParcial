using Cinemachine;
using System.Collections;
using UnityEngine;
public class CameraAnimation : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera _camera;
    [Header("FOV")]
    private float _defaultFOV = 60f;
    private float _runningFOV = 85f;
    private float _transitionDuration = 0.5f;
    private Coroutine _coroutineFov;
    private void Update()
    {
        var moveVector = GameManager.instance.player.MoveVector;
        if (PlayerInputs.instance.RunAction() && moveVector != Vector2.zero)
        {
            if (_coroutineFov != null)
                StopCoroutine(_coroutineFov);
            _coroutineFov = StartCoroutine(ChangeFOV(_runningFOV));
        }
        else
        {
            if (_coroutineFov != null)
                StopCoroutine(_coroutineFov);
            _coroutineFov = StartCoroutine(ChangeFOV(_defaultFOV));
        }
    }
    private IEnumerator ChangeFOV(float targetFov)
    {
        float startFov = _camera.m_Lens.FieldOfView;
        float elapsedTime = 0f;
        while(elapsedTime <= _transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            _camera.m_Lens.FieldOfView = Mathf.Lerp(startFov, targetFov, elapsedTime/ _transitionDuration);
            yield return new WaitForEndOfFrame();
        }
        _camera.m_Lens.FieldOfView = targetFov;
    }
}
