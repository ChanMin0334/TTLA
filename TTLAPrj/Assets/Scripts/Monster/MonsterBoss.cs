using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoss : Monster
{
    [Header("Boss Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 0.5f;
    public int projectileCount = 5;
    public float projectileSpreadAngle = 30f;
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
        float startAngle = -projectileSpreadAngle * (projectileCount - 1)  / 2f;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + projectileSpreadAngle * i;
            Vector2 shootDir = Quaternion.Euler(0, 0, angle) * baseDir;

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
