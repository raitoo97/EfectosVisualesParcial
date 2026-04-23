using Cinemachine;
using System.Collections;
using UnityEngine;
public class NPCCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _npcCamera;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    public static NPCCameraManager Instance;
    private Coroutine deferredPlayerViewCoroutine;
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
    public void ChangeCamera(bool isNPC)
    {
        if (isNPC)
        {
            _npcCamera.Priority = 10;
            _mainCamera.Priority = 0;
            GameManager.instance.player.GetPlayerController._isOnCinematic = true;
            CinematicDirector.instance.DesactivateGunAndPlayer();
        }
        else
        {
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
        CinematicDirector.instance.ActivateGunAndPlayer();
        GameManager.instance.player.GetPlayerController._isOnCinematic = false;
        deferredPlayerViewCoroutine = null;
    }
}
