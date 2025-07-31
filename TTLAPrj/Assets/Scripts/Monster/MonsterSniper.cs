using System.Collections;
using UnityEngine;

public class MonsterSniper : Monster
{
    [Header("Sniper Settings")]
    public LineRenderer laserLine;
    public Transform firePoint;
    public LayerMask wallMask; // Assign in Inspector (e.g., Wall, Ground)

    private bool isCharging = false;

    protected override void Awake()
    {
        base.Awake();
        agent.isStopped = true;

        if (laserLine == null)
        {
            Debug.LogWarning("Laser LineRenderer not assigned, trying to auto-find...");
            laserLine = GetComponentInChildren<LineRenderer>();
        }
    }

    protected override void Update()
    {
        if (target == null)
            return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        HandleSpriteFlip(dir);

        if (!isCharging && Time.time - lastAttackTime >= 1f / Stats.AtkSpeed)
        {
            StartCoroutine(Snipe());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator Snipe()
    {
        isCharging = true;

        float trackingTime = Stats.AtkSpeed - 0.3f;
        float elapsed = 0f;

        if (laserLine != null)
            laserLine.enabled = true;

        // Tracking phase
        while (elapsed < trackingTime)
        {
            if (target == null)
            {
                if (laserLine != null)
                    laserLine.enabled = false;

                isCharging = false;
                yield break;
            }

            Vector2 currentDir = (target.transform.position - transform.position).normalized;
            UpdateLaserLine(currentDir);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Lock in final direction
        Vector2 finalDir = (target.transform.position - transform.position).normalized;
        UpdateLaserLine(finalDir);

        yield return new WaitForSeconds(Stats.AtkSpeed - trackingTime);

        ShootProjectile(finalDir);

        if (laserLine != null)
            laserLine.enabled = false;

        isCharging = false;
    }

    private void UpdateLaserLine(Vector2 dir)
    {
        if (laserLine == null || firePoint == null)
            return;

        laserLine.SetPosition(0, firePoint.position);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, 20f, wallMask);
        if (hit.collider != null)
        {
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, firePoint.position + (Vector3)(dir * 20f));
        }
    }

    private void ShootProjectile(Vector2 dir)
    {
        if (projectile == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(projectile, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = dir * 50f;

        ProjectileTarget pt = bullet.GetComponent<ProjectileTarget>();
        if (pt != null)
        {
            pt.damage = Stats.Atk;
            pt.shooterLayer = gameObject.layer;
        }

        if (anim != null)
        {
            // Add animation trigger here if needed
        }
    }
}
