using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : LivingEntity
{
    public ParticleSystem DeathEffect;
    public ParticleSystem DamageEffect;

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(DeathEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.main.startLifetimeMultiplier);
        }
        else
        {
            Destroy(Instantiate(DamageEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DamageEffect.main.startLifetimeMultiplier);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
