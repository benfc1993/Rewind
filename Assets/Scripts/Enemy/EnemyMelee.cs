using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyController
{
    LayerMask collisionMask;
    int damage = 1;
    public float attackCountdown = 0;
    public float startAttackCountdown = 10;
    // Update is called once per frame
    void Update()
    {
        float distToMove = pathFinder.speed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distToMove, collisionMask, QueryTriggerInteraction.Collide))
        {
            print("ray player");
        }
            if (attackCountdown != 0)
        {
            handleAttackCountdown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Enemy collide");
        if (attackCountdown == 0)
        {
            print(attackCountdown);
            
            print("hit player");
            if (other.tag == "Player")
            {
                attackCountdown = startAttackCountdown;
                IDamagable damagableObject = other.GetComponent<IDamagable>();
                if (damagableObject != null)
                {
                    damagableObject.TakeHit(damage, transform.forward, transform.forward);
                }
            }
        }
    }

    void handleAttackCountdown()
    {
        if (attackCountdown <= 0)
        {
            attackCountdown = 0;
        }
        else
        {
            attackCountdown -= Time.fixedDeltaTime;
        }
    }
}
