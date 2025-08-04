using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;

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
    [Header("Bow Settings")]
    public Transform bowTransform; 
    public SpriteRenderer bowSpriteRenderer;
    public Sprite unpulledSprite;
    public Sprite pulledSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        HandleInput();
        UpdateBowDirection();

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
        StartCoroutine(BowAttackCoroutine(nearest));
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

    private void UpdateBowDirection()
    {
        if (bowTransform == null)
        {
            return;
        }
        GameObject nearest = FindNearestEnemy();
        if (nearest == null)
        {
            return;
        }

        Vector2 dir = (nearest.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        bowTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (bowSpriteRenderer != null)
        {
            bowSpriteRenderer.flipY = dir.x < 0;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }

    private IEnumerator BowAttackCoroutine(GameObject target)
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;

        if (bowSpriteRenderer != null && pulledSprite != null)
        {
            bowSpriteRenderer.sprite = pulledSprite;
        }
        yield return new WaitForSeconds(0.1f);

        if (projectile != null)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject proj = Instantiate(projectile, bowTransform.position, rotation);
            proj.layer = LayerMask.NameToLayer("PlayerProjectile");

            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = dir * 10f;
            }

            ProjectileTarget projTarget = proj.GetComponent<ProjectileTarget>();
            if (projTarget != null)
            {
                projTarget.damage = Stats.Atk;
                projTarget.shooterLayer = gameObject.layer;
                projTarget.targetLayers = LayerMask.GetMask("Monster");
            }
        }

        yield return null;
        if (bowSpriteRenderer != null && unpulledSprite != null)
        {
            bowSpriteRenderer.sprite = unpulledSprite;
        }
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


    //Test

    public void AddStats(Stats stats)
    {
        this.Stats.Atk += stats.Atk;
        this.Stats.Hp += stats.Hp;
        this.Stats.AtkSpeed += stats.AtkSpeed;
        this.Stats.Speed += stats.Speed;
    }

    public void RemoveStats(Stats stats)
    {
        this.Stats.Atk -= stats.Atk;
        this.Stats.Hp -= stats.Hp;
        this.Stats.AtkSpeed -= stats.AtkSpeed;
        this.Stats.Speed -= stats.Speed;
    }
}
