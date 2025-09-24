using UnityEngine;
public class Assistant : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private AssistantAnimations _assistantAnimations;
    public bool IsTalking;
    private void Awake()
    {
        _assistantAnimations = new AssistantAnimations(_animator);
    }
    private void Start()
    {
        _assistantAnimations.SetBool("IsTalking", IsTalking);
    }
}
