using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Stats Stats;
    public Animator anim;
    public GameObject projectile;

    public virtual void Damaged(float damage)
    {
        Stats.Hp -= damage;
        if (Stats.Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void Attack()
    {

    }

    public virtual void Move(Vector2 movement)
    {
        transform.Translate(movement * Stats.Speed * Time.deltaTime);
    }
}
