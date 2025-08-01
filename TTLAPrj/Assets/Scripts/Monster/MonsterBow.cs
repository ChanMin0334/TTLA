using UnityEngine;

public class MonsterBow : MonsterRange
{
    public Transform bowPoint; // Where the projectile is spawned
    public Transform bowVisual; // Optional: visual object that rotates with the bow

    protected override void Update()
    {
        base.Update();

        if (target != null && bowPoint != null)
        {
            Vector2 dir = (target.transform.position - bowPoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bowPoint.rotation = Quaternion.Euler(0, 0, angle);

            // Optional: flip visual if aiming left
            if (bowVisual != null)
            {
                bowVisual.localScale = new Vector3(1, dir.x < 0 ? -1 : 1, 1);
            }
        }
    }

    public override void Attack()
    {
        if (target == null || projectile == null || bowPoint == null) return;

        StopMovement();
        HandleSpriteFlip((target.transform.position - transform.position).normalized);

        // Fire arrow immediately
        Vector2 dir = (target.transform.position - transform.position).normalized;
        GameObject proj = Instantiate(projectile, bowPoint.position, bowPoint.rotation);
        proj.layer = LayerMask.NameToLayer("EnemyProjectile");

        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        if (projRb != null)
        {
            projRb.velocity = dir * 5f;
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
