using Cinemachine;
using UnityEngine;
public class Assistant : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private AssistantAnimations _assistantAnimations;
    public bool IsTalking;
    public bool OverHere;
    private bool playerInRange = false;
    private bool isOnDialogue = false;
    [SerializeField]private DialogueData dialogue;
    [SerializeField]private CinemachineVirtualCamera _myCamera;
    private void Awake()
    {
        _assistantAnimations = new AssistantAnimations(_animator);
    }
    private void Start()
    {
        _assistantAnimations.SetBool("IsTalking", IsTalking);
        _assistantAnimations.SetBool("OverHere", OverHere);
        _myCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        Dialogue.onDialogueEnd += ChangeTalkingMode;
    }
    private void Update()
    {
        if(isOnDialogue) return;
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOnDialogue = true;
            StartTalking();
        }
    }
    private void ChangeTalkingMode()
    {
        if (!IsTalking) return;
        isOnDialogue = false;
        StopTalking();
    }
    private void StartTalking()
    {
        IsTalking = true;
        OverHere = false;
        _assistantAnimations.SetBool("IsTalking", true);
        _assistantAnimations.SetBool("OverHere", false);
        NPCCameraManager.Instance.ChangeCamera(true, dialogue.lines, _myCamera);
        CanvasManager.instance.ShowTalkWhitNullText(false);
    }
    private void StopTalking()
    {
        IsTalking = false;
        OverHere = true;
        _assistantAnimations.SetBool("IsTalking", false);
        _assistantAnimations.SetBool("OverHere", true);
        NPCCameraManager.Instance.ChangeCamera(false, null, null);
        CanvasManager.instance.ShowTalkWhitNullText(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            OverHere = true;
            _assistantAnimations.SetBool("OverHere", OverHere);
            CanvasManager.instance.ShowTalkWhitNullText(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            OverHere = false;
            CanvasManager.instance.ShowTalkWhitNullText(false);
            _assistantAnimations.SetBool("OverHere", OverHere);
            if (IsTalking)
            {
                IsTalking = false;
                _assistantAnimations.SetBool("IsTalking", false);
                NPCCameraManager.Instance.ChangeCamera(false, dialogue.lines, _myCamera);
            }
        }
    }
}
