using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;


public class ExplosionPhysicsForce : MonoBehaviour
{
    public float explosionForce = 4;
    int damage;

    private IEnumerator Start()
    {
        damage = gameObject.GetComponentInParent<ExplodeGrenade>().GrenadeDamage;
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return null;

        float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

        float r = 10 * multiplier;
        var cols = Physics.OverlapSphere(transform.position, r);
        var rigidbodies = new List<Rigidbody>();
        // Combine all the colliders of a single parent to a single entry in the RigidBody List
        foreach (var col in cols)
        {

            if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
            {
                // Add new RigidBody object to list
                rigidbodies.Add(col.attachedRigidbody);
            }
        }
        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(explosionForce * multiplier, transform.position, r, 1 * multiplier, ForceMode.Impulse);
            rb.useGravity = true;
            InflictDamage(rb.gameObject);
        }
    }

    // If GameObject has health script the grenade will inflict damage
    void InflictDamage(GameObject touchedObject)
    {
        EnemyHealth enemy = touchedObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            return;
        }
        PlayerHealth player = touchedObject.GetComponentInParent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
