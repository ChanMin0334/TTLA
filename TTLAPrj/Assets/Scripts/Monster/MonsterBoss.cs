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
    private bool isPhaseTwo = false;
    private float initialHp;
    private bool isPerformingAttack = false;
    public bool isActive = false;

    public bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        initialHp = Stats.Hp;
    }

    protected override void Update()
    {
        if (!isActive) return;
        if (target == null || isPerformingAttack)
        {
            return;
        }

        base.agent.isStopped = true;
        Vector2 dir = (target.transform.position - transform.position).normalized;
        HandleSpriteFlip(dir);

        if (!isPhaseTwo && Stats.Hp <= initialHp / 2f)
        {
            EnterPhaseTwo();
        }
        if (Time.time - lastActionTime >= dashCooldown)
        {
            lastActionTime = Time.time;
            if (isPhaseTwo)
            {
                int random = Random.Range(0, 10); // Three Attacks, based on chance
                Debug.Log("Chance" + random);
                if (0 <= random && random <= 4)
                {
                    StartCoroutine(DashAttack(dir));
                }
                else if (5 <= random && random <= 8)
                {
                    SpreadShot(dir);
                }
                else
                {
                    StartCoroutine(SpiralShotAttack());
                }
            }
            else
            {     
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
    }

    private IEnumerator DashAttack(Vector2 direction)
    {
        animationManager?.SetMoveAnimation(true);
        HandleSpriteFlip(direction);
        rb.bodyType = RigidbodyType2D.Kinematic;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        animationManager?.SetMoveAnimation(false);
    }

    private void SpreadShot(Vector2 baseDir)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        float maxOffset = projectileSpreadAngle;

        SoundManager.Instance.PlaySFX(SFX_Name.Player_Attack);
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
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void EnterPhaseTwo()
    {
        isPhaseTwo = true;

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(1f, 0.5f, 0.5f);
        }

        dashSpeed *= 1.5f;

        StartCoroutine(SpiralShotAttack());
    }

    private IEnumerator SpiralShotAttack()
    {
        isPerformingAttack = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Vector2 centerPosition = Vector2.zero; // Change if Center is not 0,0
        float moveSpeed = 6f;

        // Move to center
        while (Vector2.Distance(transform.position, centerPosition) > 0.1f)
        {
            Vector2 direction = (centerPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        // Spiral shot Settings
        int totalShots = 30;
        float angle = 0f;
        float angleStep = 15f;

        SoundManager.Instance.PlaySFX(SFX_Name.Player_Attack);

        for (int i = 0; i < totalShots; i++)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector2 shootDir = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.layer = LayerMask.NameToLayer("EnemyProjectile");

            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = shootDir * projectileSpeed;
            }

            ProjectileTarget projTarget = proj.GetComponent<ProjectileTarget>();
            if (projTarget != null)
            {
                projTarget.damage = Stats.Atk;
                projTarget.shooterLayer = gameObject.layer;
                projTarget.targetLayers = LayerMask.GetMask("Player");
            }

            angle += angleStep;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f); // Pause before resuming normal behavior
        rb.bodyType = RigidbodyType2D.Dynamic;
        isPerformingAttack = false;
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

    public void ActivateBoss()
    {
        isActive = true;
    }

    public override void Damaged(float damage)
    {
        Stats.Hp -= damage;
        if (Stats.Hp <= 0)
        {
            
            animationManager.PlayDeath();
            Destroy(gameObject);

            if(!isDead)
            {
                isDead = true;
                UIManager.Instance.CallGameClear();
            }

        }
        SoundManager.Instance.PlaySFX(SFX_Name.Player_Attack);
    }
}
