using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Animator animator;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("IsDead", true);
        }
    }
}
