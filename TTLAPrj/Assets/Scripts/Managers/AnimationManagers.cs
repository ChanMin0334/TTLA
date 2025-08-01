using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagers : MonoBehaviour
{
    private Animator animator;
    [Header("Particle Systems")]
    public ParticleSystem Death;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing" + gameObject.name);
        }
    }

    public void SetMoveAnimation(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void PlayDeath()
    {
        Death.Play();
    }
}
