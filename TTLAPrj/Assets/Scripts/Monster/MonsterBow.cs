using UnityEngine;

public class MonsterBow : MonsterRange
{
    public Transform bowPoint; // Arrow spawn position
    public SpriteRenderer bowSpriteRenderer; // The visual bow's SpriteRenderer

    public Sprite unpulledSprite; // Default sprite
    public Sprite pulledSprite;   // Pulled bow sprite

    public float shootDelay = 0.2f; // Optional delay before firing

    protected override void Update()
    {
        base.Update();

        if (target != null && bowPoint != null)
        {
            Vector2 dir = (target.transform.position - bowPoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bowPoint.rotation = Quaternion.Euler(0, 0, angle);

            if (bowSpriteRenderer != null)
            {
                bowSpriteRenderer.flipY = dir.x < 0;
            }
        }
    }

    public override void Attack()
    {
        if (target == null || projectile == null || bowPoint == null) return;

        StopMovement();
        HandleSpriteFlip((target.transform.position - transform.position).normalized);

        // Change sprite to pulled bow
        if (bowSpriteRenderer != null && pulledSprite != null)
            bowSpriteRenderer.sprite = pulledSprite;

        // Fire after delay
        Invoke(nameof(FireArrow), shootDelay);
    }

    private void FireArrow()
    {
        Vector2 dir = (target.transform.position - transform.position).normalized;
        GameObject proj = Instantiate(projectile, bowPoint.position, bowPoint.rotation);
        proj.layer = LayerMask.NameToLayer("EnemyProjectile");

        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        if (projRb != null)
        {
            projRb.velocity = bowPoint.right * 5f;
        }

        ProjectileTarget projTarget = proj.GetComponent<ProjectileTarget>();
        if (projTarget != null)
        {
            projTarget.damage = Stats.Atk;
            projTarget.shooterLayer = gameObject.layer;
            projTarget.targetLayers = LayerMask.GetMask("Player");
        }

        // Revert sprite to unpulled after short time
        if (bowSpriteRenderer != null && unpulledSprite != null)
            bowSpriteRenderer.sprite = unpulledSprite;
    }
}
