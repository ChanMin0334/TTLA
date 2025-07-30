using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : Entity
{
    protected Player target;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 moveDirection;

    [Header("Combat")]
    public float attackRange = 1.5f;
    protected float lastAttackTime = 0f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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
            moveDirection = dir;
        }
        else
        {
            moveDirection = Vector2.zero;
            StopMovement();

            if (Time.time - lastAttackTime >= 1f / Stats.AtkSpeed)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

        HandleSpriteFlip(dir);
    }

    protected virtual void FixedUpdate()
    {
        Move(moveDirection);
    }

    public override void Move(Vector2 direction)
    {
        rb.velocity = direction * Stats.Speed;

        // if (anim != null)
        // {
        //     anim.SetBool("IsMoving", direction != Vector2.zero);
        //     anim.SetFloat("MoveX", direction.x);
        //     anim.SetFloat("MoveY", direction.y);
        // }
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
