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
        if (target == null || isDashing) return;

        base.HandleSpriteFlip((target.transform.position - transform.position).normalized);

        if (agent != null && !agent.pathPending && agent.enabled)
        {
            agent.SetDestination(target.transform.position);
        }

        if (Time.time - lastAttackTime >= dashCooldown && agent.hasPath)
        {
            StartCoroutine(DashTowardsTarget());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator DashTowardsTarget()
    {
        isDashing = true;

        if (agent != null)
        {
            agent.isStopped = true;
        }

        yield return new WaitUntil(() => agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.path.corners.Length > 1);

        Vector3 dashTarget = agent.path.corners[1]; // Use next waypoint in path
        Vector2 direction = (dashTarget - transform.position).normalized;

        float dashedDistance = 0f;
        float dashStep;

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
            agent.isStopped = false;
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
