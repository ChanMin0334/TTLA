using System.Collections;
using UnityEngine;

public class MonsterSplit : Monster
{
    public GameObject splitMonsterPrefab;
    public float dashForce = 5f;         // Dash speed
    public float dashDistance = 3f;      // Distance to dash
    public float dashCooldown = 2f;

    private bool isDashing = false;

    protected override void Update()
    {
        if (target == null || isDashing)
        {
            return;
        }
        base.HandleSpriteFlip((target.transform.position - transform.position).normalized);

        if (Time.time - lastAttackTime >= dashCooldown)
        {
            StartCoroutine(DashTowardsTarget());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator DashTowardsTarget()
    {
        isDashing = true;
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float dashedDistance = 0f;
        float dashStep;

        // Temporarily disable NavMeshAgent if active
        if (agent != null)
        {
            agent.enabled = false; // When Moving disabled in order to stop chasing. 
        }
        while (dashedDistance < dashDistance)
        {
            dashStep = dashForce * Time.deltaTime;
            rb.MovePosition(rb.position + direction * dashStep);
            dashedDistance += dashStep;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);

        if (agent != null)
        {
            agent.enabled = true;
        }

        isDashing = false;
    }

    public override void Damaged(float damage)
    {
        Stats.Hp -= damage;
        if (Stats.Hp <= 0)
        {
            DieAndSplit();
        }
    }

    private void DieAndSplit()
    {
        animationManager?.PlayDeath();

        if (splitMonsterPrefab != null)
        {
            Instantiate(splitMonsterPrefab, transform.position + Vector3.left * 0.5f, Quaternion.identity);
            Instantiate(splitMonsterPrefab, transform.position + Vector3.right * 0.5f, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Damaged(Stats.Atk);
            }
        }
    }
}
