using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : LivingEntity
{
    protected UnityEngine.AI.NavMeshAgent pathFinder;
    protected Transform target;
    public ParticleSystem DeathEffect;
    protected Transform Player;
    public float distanceToStay = 0;
    public int viewDistance;
    public float viewAngle;
    public LayerMask viewMask;

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
        if(Vector3.Distance(transform.position, Player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (Player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleToPlayer < viewAngle / 2f)
            {
                if(Physics.Linecast(transform.position, Player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if(damage >= health)
        {
            Destroy(Instantiate(DeathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.startLifetime);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    IEnumerator UpdatePath()
    {
        print("update path");
        float refreshRate = 0.25f;
        while (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) > distanceToStay)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                if (!dead)
                {
                    pathFinder.SetDestination(target.position);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
