using UnityEngine;

public class Glass : LivingEntity
{
    public ParticleSystem DeathEffect;
    public ParticleSystem DamageEffect;

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            Destroy(Instantiate(DeathEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.main.startLifetimeMultiplier);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}

