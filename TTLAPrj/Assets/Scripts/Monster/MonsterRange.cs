using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRange : Monster
{
    public override void Attack()
    {
        if (target == null)
        {
            return;
        }
        StopMovement();
        Vector2 dir = (target.transform.position - transform.position).normalized;

        if (projectile != null)
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = dir * 5f;
            }

            //animation?
        }
    }
}
