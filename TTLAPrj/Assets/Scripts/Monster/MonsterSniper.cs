using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSniper : Monster
{
    [Header("Sniper Setting")]
    public float chargeTime = 1.5f; // Time taken to fire.
    public LineRenderer laserLine;
    public Transform firePoint;
    private bool isCharging = false;



    protected override void Awake()
    {
        base.Awake();
        agent.isStopped = true; //Stationary

        if (laserLine == null)
        {
            Debug.Log("Help ME");
            laserLine = GetComponentInChildren<LineRenderer>();
        }
    }

    protected override void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.transform.position);

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

        float trackingtime = chargeTime - 0.3f;
        float elapsed = 0f;

        //Show Red Line
        if (laserLine != null)
        {
            laserLine.enabled = true;
        }

        while (elapsed < trackingtime)
        {
            if (laserLine != null && firePoint != null && target != null)
            {
                Vector2 currentDir = (target.transform.position - transform.position).normalized;
                laserLine.SetPosition(0, firePoint.position);
                laserLine.SetPosition(1, firePoint.position + (Vector3)(currentDir * 20f));
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Vector2 finalDir = (target.transform.position - transform.position).normalized;
        if (laserLine != null && firePoint != null)
        {
            laserLine.SetPosition(0, firePoint.position);
            laserLine.SetPosition(1, firePoint.position + (Vector3)(finalDir * 20f));
        }

        yield return new WaitForSeconds(chargeTime - trackingtime);

        ShootProjectile(finalDir);

        if (laserLine != null)
        {
            laserLine.enabled = false;
        }

        isCharging = false;
    }

    private void ShootProjectile(Vector2 dir)
    {
        if (projectile == null || firePoint == null)
        {
            return;
        }

        GameObject bullet = Instantiate(projectile, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * 50f; //Very fast
        }
        ProjectileTarget pt = bullet.GetComponent<ProjectileTarget>();
        if (pt != null)
        {
            pt.damage = Stats.Atk;
            pt.shooterLayer = gameObject.layer; 
        }

        if (anim != null)
            {
                //Animations?
            }
    }
}
