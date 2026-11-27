using Cinemachine;
using UnityEngine;
public class AssistantL0 : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private AssistantAnimations _assistantAnimations;
    public bool IsTalking;
    public bool OverHere;
    private bool playerInRange = false;
    private bool isOnDialogue = false;
    private int dialogueEndFrame = -1;
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private CinemachineVirtualCamera _myCamera;
    private void Awake()
    {
        _assistantAnimations = new AssistantAnimations(_animator);
    }
    private void Start()
    {
        _assistantAnimations.SetBool("IsTalking", IsTalking);
        _assistantAnimations.SetBool("OverHere", OverHere);
        _myCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        DialogueL0.onDialogueEnd += ChangeTalkingMode;
    }
    private void OnDestroy()
    {
        DialogueL0.onDialogueEnd -= ChangeTalkingMode;
    }
    private void Update()
    {
        if (isOnDialogue) return;
        if (Time.frameCount == dialogueEndFrame) return;
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOnDialogue = true;
            StartTalking();
        }
    }
    public void TalkWhenWakeUp()
    {
        if (isOnDialogue) return;
        isOnDialogue = true;
        StartTalking();
    }
    private void ChangeTalkingMode()
    {
        if (!IsTalking) return;
        isOnDialogue = false;
        dialogueEndFrame = Time.frameCount;
        StopTalking();
    }
    private void StartTalking()
    {
        IsTalking = true;
        OverHere = false;
        _assistantAnimations.SetBool("IsTalking", true);
        _assistantAnimations.SetBool("OverHere", false);
        NPCCameraManagerL0.Instance.ChangeCamera(true, dialogue.lines, _myCamera);
        CanvasManager.instance.ShowTalkWhitNullText(false);
    }
    private void StopTalking()
    {
        IsTalking = false;
        OverHere = true;
        _assistantAnimations.SetBool("IsTalking", false);
        _assistantAnimations.SetBool("OverHere", true);
        NPCCameraManagerL0.Instance.ChangeCamera(false, null, null);
        CanvasManager.instance.ShowTalkWhitNullText(playerInRange);
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
            _assistantAnimations.SetBool("OverHere", OverHere);
            CanvasManager.instance.ShowTalkWhitNullText(false);
        }
    }
}
