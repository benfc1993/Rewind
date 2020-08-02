using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public float speed = 100;
    public LayerMask playerCollisionMask;
    public LayerMask wallCollisionMask;
    public ParticleSystem sparkEffect;
    public Light BulletHit;

    private void Update()
    {
        float distToMove = speed * Time.deltaTime;
        CheckCollisions(distToMove);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void CheckCollisions(float distToMove)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distToMove, playerCollisionMask, QueryTriggerInteraction.Collide))
        {
        Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 1f);
            OnHitPlayer(hit.collider, hit.point);
        }
        if (Physics.Raycast(ray, out hit, distToMove, wallCollisionMask, QueryTriggerInteraction.Collide))
        {
            Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 1f);
            OnHitWall(hit);
        }
    }

    void OnHitPlayer(Collider c, Vector3 hitPoint)
    {
        print(c.gameObject.name);
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        GameObject.Destroy(gameObject);
    }

    void OnHitWall(RaycastHit hit)
    {
        Destroy(Instantiate(sparkEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, -transform.forward)) as GameObject, sparkEffect.startLifetime);
        GameObject.Destroy(gameObject);
    }
}
