using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoss : Monster
{
    [Header("Boss Settings")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 1f;
    public int projectileCount = 8;
    public float projectileSpreadAngle = 20f;
    public float projectileSpeed = 6f;
    private float lastActionTime;

    protected override void Update()
    {
        if (target == null)
        {
            return;
        }

        base.agent.isStopped = true;
        Vector2 dir = (target.transform.position - transform.position).normalized;
        //HandleSpriteFlip(dir)

        if (Time.time - lastActionTime >= dashCooldown)
        {
            lastActionTime = Time.time;

            int random = Random.Range(0, 2);
            if (random == 0)
            {
                StartCoroutine(DashAttack(dir));
            }
            else
            {
                SpreadShot(dir);
            }
        }
    }

    private IEnumerator DashAttack(Vector2 direction)
    {
        float elapsed = 0f;
        StopMovement();

        while (elapsed < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;

    }

    private void SpreadShot(Vector2 baseDir)
    {
        float maxOffset = projectileSpreadAngle;

        for (int i = 0; i < projectileCount; i++)
        {
            float randomAngle = Random.Range(-maxOffset, maxOffset);
            Vector2 shootDir = Quaternion.Euler(0, 0, randomAngle) * baseDir;

            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.layer = LayerMask.NameToLayer("EnemyProjectile");

            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = shootDir.normalized * 5f;
            }

            ProjectileTarget projTarget = proj.GetComponent<ProjectileTarget>();
            if (projTarget != null)
            {
                projTarget.damage = Stats.Atk;
                projTarget.shooterLayer = gameObject.layer;
                projTarget.targetLayers = LayerMask.GetMask("Player");
            }
        }
    }
}
