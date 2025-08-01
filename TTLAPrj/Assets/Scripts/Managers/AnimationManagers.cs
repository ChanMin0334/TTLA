using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagers : MonoBehaviour
{
    private Animator animator;
    [Header("Particle Systems")]
    public GameObject deathParticlePrefab;

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
        if (deathParticlePrefab != null)
        {
            GameObject particle = Instantiate(
                deathParticlePrefab,
                transform.position,
                Quaternion.identity
            );

            // Automatically destroy after particle system duration
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(particle, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(particle, 2f); // fallback
            }
        }
        else
        {
            Debug.LogWarning("Death particle prefab not assigned!");
        }
    }
}
