using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Stats Stats;
    public Animator anim;
    public GameObject projectile;
    public AnimationManagers animationManager;
    protected SoundManager soundManager;

    public void Awake()
    {
        Stats = new Stats(0f, 0f, 0f, 0f, 0f);
    }
    public void Start()
    {
        soundManager = SoundManager.Instance;
    }

    public virtual void Damaged(float damage)
    {
        Stats.Hp -= damage;
        if (Stats.Hp <= 0)
        {
            animationManager.PlayDeath();
            Destroy(gameObject);
        }
        soundManager.PlaySFX(SFX_Name.Player_Attack);
    }
    public virtual void Attack()
    {

    }

    public virtual void Move(Vector2 movement)
    {
        transform.Translate(movement * Stats.Speed * Time.deltaTime);
        anim.SetBool("IsMoving", true);
    }
}
