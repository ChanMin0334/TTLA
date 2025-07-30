using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Monster : Entity
{
    protected Player target;
    protected Rigidbody2D rb;

    [Header("Combat System")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    protected float lastAttackTime = 0f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            target = playerObj.GetComponent<Player>();
        }
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            return;
        }

        float dist = Vector2.Distance(transform.position, target.transform.position);
        Vector2 dir = (target.transform.position - transform.position).normalized;
    }
}
