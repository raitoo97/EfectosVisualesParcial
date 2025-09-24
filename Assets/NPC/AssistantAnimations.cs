using UnityEngine;
public class AssistantAnimations
{
    private Animator _animator;
    public AssistantAnimations(Animator animator)
    {
        _animator = animator;
    }
    public void SetBool(string paramName, bool value)
    {
        _animator.SetBool(paramName, value);
    }
}
