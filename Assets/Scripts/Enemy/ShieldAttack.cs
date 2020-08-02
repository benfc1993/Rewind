using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    LayerMask collisionMask;
    int damage = 3;
    public float attackCountdown = 0;
    public float startAttackCountdown = 1;
    // Update is called once per frame
    void Update()
    {
        if (attackCountdown != 0)
        {
            handleAttackCountdown();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackCountdown == 0)
        {
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
