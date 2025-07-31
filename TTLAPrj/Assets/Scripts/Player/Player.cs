using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    public List<Item> EquipList;
    public Skill SkillEffect;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 inputDir;
    private float lastAttackTime = 0f;
    private Interact interact;

    [Header("Invicibility")]
    private bool isInvincible = false;
    public float invicibilityDuration = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        HandleInput();

        if (inputDir == Vector2.zero && Time.time - lastAttackTime >= 1f / Stats.AtkSpeed)
        {
            AttackNearestEnemy();
            lastAttackTime = Time.time;
        }
    }
    private void FixedUpdate()
    {
        Move(inputDir);
    }

    public override void Damaged(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        base.Damaged(damage);
        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        while (elapsed < invicibilityDuration)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }

            yield return new WaitForSeconds(0.1f);


            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }

            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        isInvincible = false;
    }

    private void HandleInput()
    {
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    public override void Move(Vector2 direction)
    {
        rb.velocity = direction * Stats.Speed;

        if (anim != null)
        {
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
            anim.SetBool("IsMoving", direction != Vector2.zero);
        }

        if (direction.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void AttackNearestEnemy()
    {
        //Debug.Log("Attacking Enemy");
        GameObject nearest = FindNearestEnemy();
        if (nearest == null)
        {
            return;
        }
        Vector2 dir = (nearest.transform.position - transform.position).normalized;

        if (projectile != null)
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.layer = LayerMask.NameToLayer("PlayerProjectile");
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = dir * 10f; // 임시 코드
            }
            ProjectileTarget projTarget = proj.GetComponent<ProjectileTarget>();
            if (projTarget != null)
            {
                projTarget.damage = Stats.Atk;
                projTarget.shooterLayer = gameObject.layer;
                projTarget.targetLayers = LayerMask.GetMask("Enemy");
            }
        }
    }
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        //Debug.Log(closest);
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact"))
        {
            interact = collision.GetComponent<Interact>();
            interact.OnEnterTrigger(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interact"))
        {
            interact = collision.GetComponent<Interact>();
            interact.OnExitTrigger(this);
        }
    }
}
