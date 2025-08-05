using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    public LayerMask targetLayers; //What this projectile can hit
    public int shooterLayer; //Who shot this projectile
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayer = collision.gameObject.layer;

        // Ignore collision with the shooter
        //Debug.Log(otherLayer);
        if (otherLayer == shooterLayer || otherLayer == 0)
        {
            return;
        }

        // If it's a valid target (e.g. Player), deal damage
        if ((targetLayers.value & (1 << otherLayer)) != 0)
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity != null)
            {
                entity.Damaged(damage);
            }
        }
        // Destroy the projectile on any collision
        Destroy(gameObject);
    }
}
