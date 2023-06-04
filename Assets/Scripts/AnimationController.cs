using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    public void Jump()
    {
        m_Animator.SetTrigger("Jump");
    }

    public void Die()
    {
        m_Animator.SetBool("IsDead", true);
    }
}

