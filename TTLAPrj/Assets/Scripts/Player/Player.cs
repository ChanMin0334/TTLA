using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    //Public List<Item> EquipList; 
    //Public Skill SkillEffect;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 inputDir;
    private float lastAttackTime = 0f;

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
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = dir * 10f; // 임시 코드
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
}
