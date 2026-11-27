using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
public class NPCCameraManagerL0 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _npcCamera;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    public static NPCCameraManagerL0 Instance;
    private Coroutine deferredPlayerViewCoroutine;
    public Action<string[]> StartDialogue;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        _npcCamera = null;
    }
    public void ChangeCamera(bool isNPC, string[] lines, CinemachineVirtualCamera currentCamera)
    {
        if (isNPC)
        {
            _npcCamera = currentCamera;
            _npcCamera.Priority = 10;
            _mainCamera.Priority = 0;
            GameManager.instance.player.GetPlayerController._isOnCinematic = true;
            CinematicDirectorL0.instance.DesactivateGunAndPlayer();
            StartDialogue?.Invoke(lines);
        }
        else
        {
            if (_npcCamera != null)
                _npcCamera.Priority = 0;
            _mainCamera.Priority = 10;
            if (deferredPlayerViewCoroutine != null)
            {
                StopCoroutine(deferredPlayerViewCoroutine);
            }
            deferredPlayerViewCoroutine = StartCoroutine(CallDeferredPlayerView());
        }
    }
    IEnumerator CallDeferredPlayerView()
    {
        yield return new WaitForSeconds(1f);
        CinematicDirectorL0.instance.ActivateGunAndPlayer();
        GameManager.instance.player.GetPlayerController._isOnCinematic = false;
        deferredPlayerViewCoroutine = null;
        _npcCamera = null;
    }
}
