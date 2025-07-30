using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClose : Monster
{
    public override void Attack()
    {
        if (target != null)
        {
            StopMovement();
            target.Damaged(Stats.Atk);
            //Animation for Attack
            //
        }
    }
}
