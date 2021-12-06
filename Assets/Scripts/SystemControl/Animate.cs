using UnityEngine;

//Script defines, which specific animation of an animated object is active.
public class Animate : MonoBehaviour
{
    public int AnimIndex = 0;
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("AnimIndex", AnimIndex);
        m_animator.SetTrigger("Next");
    }
}
