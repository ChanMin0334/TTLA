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

        if (otherLayer == shooterLayer)
        {
            return;
        }

        if ((targetLayers.value & (1 << otherLayer)) != 0) //Bitmask Check
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity != null)
            {
                entity.Damaged(damage);
            }

            Destroy(gameObject);
        }
    }
}
