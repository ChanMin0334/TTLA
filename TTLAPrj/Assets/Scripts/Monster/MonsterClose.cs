using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClose : Monster
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Damaged(Stats.Atk);
                soundManager.PlaySFX(SFX_Name.Player_Attack);
            }
        }
    }
}
