using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : Entity
{
    protected Player target;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected NavMeshAgent agent;
    

    [Header("Combat")]
    public float attackRange = 1.5f;
    protected float lastAttackTime = 0f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Stats.Speed;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.GetComponent<Player>();
        }
    }

    protected virtual void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 dir = (target.transform.position - transform.position).normalized;

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);

            // Set IsMoving true if distance is large enough
            if (anim != null)
            {
                anim.SetBool("IsMoving", true);
            }
        }
        else
        {
            agent.isStopped = true;
            StopMovement();

            if (Time.time - lastAttackTime >= 1f / Stats.AtkSpeed)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

        HandleSpriteFlip(dir);
    }

    protected void StopMovement()
    {
        rb.velocity = Vector2.zero;

        if (anim != null)
        {
            anim.SetBool("IsMoving", false);
        }
    }

    protected void HandleSpriteFlip(Vector2 dir)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (dir.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (dir.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public override void Attack()
    {
        Debug.Log("Monster attacks!");
    }
}
