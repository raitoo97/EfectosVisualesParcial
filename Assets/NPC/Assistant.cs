using UnityEngine;
public class Assistant : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private AssistantAnimations _assistantAnimations;
    public bool IsTalking;
    public bool OverHere;
    private bool playerInRange = false;
    private void Awake()
    {
        _assistantAnimations = new AssistantAnimations(_animator);
    }
    private void Start()
    {
        _assistantAnimations.SetBool("IsTalking", IsTalking);
        _assistantAnimations.SetBool("OverHere", OverHere);
    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleTalk();
        }
    }
    private void ToggleTalk()
    {
        IsTalking = !IsTalking;
        OverHere = false;
        _assistantAnimations.SetBool("OverHere", OverHere);
        _assistantAnimations.SetBool("IsTalking", IsTalking);
        NPCCameraManager.Instance.ChangeCamera(IsTalking);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered assistant trigger");
            playerInRange = true;
            OverHere = true;
            _assistantAnimations.SetBool("OverHere", OverHere);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited assistant trigger");
            playerInRange = false;
            OverHere = false;
            _assistantAnimations.SetBool("OverHere", OverHere);
            if (IsTalking)
            {
                IsTalking = false;
                _assistantAnimations.SetBool("IsTalking", false);
                NPCCameraManager.Instance.ChangeCamera(false);
            }
        }
    }
}
