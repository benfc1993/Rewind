using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : LivingEntity
{
    protected UnityEngine.AI.NavMeshAgent pathFinder;
    protected Transform target;
    public ParticleSystem DeathEffect;
    public ParticleSystem DamageEffect;
    protected Transform Player;
    public float distanceToStay = 0;
    public int viewDistance;
    public float viewAngle;
    public LayerMask viewMask;

    public bool hasBattery;
    public Battery battery;

    protected override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = Player;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
    }

    protected bool canSeePlayer()
    {
        if(Player != null)
        {
            if(Vector3.Distance(transform.position, Player.position) < viewDistance)
            {
                Vector3 dirToPlayer = (Player.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
                if(angleToPlayer < viewAngle / 2f)
                {
                    if(Physics.Linecast(transform.position, Player.position, viewMask))
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        return false;
    }
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            Destroy(Instantiate(DeathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.main.startLifetimeMultiplier);
            if(hasBattery)
            {
                Instantiate(battery, transform.position, Quaternion.identity);
                Die();
            }
        }
        else
        {
            Destroy(Instantiate(DamageEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DamageEffect.main.startLifetimeMultiplier);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    protected override void Die()
    {
        FindObjectOfType<ElevatorDoor>().OnEnemyDeath();
        base.Die();
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            if (!dead && Vector3.Distance(target.position, transform.position) > distanceToStay && canSeePlayer())
            {
                pathFinder.SetDestination(target.position);
            }
            else
            {
                if (!dead)
                {
                    pathFinder.ResetPath();
                    transform.LookAt(Player);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
