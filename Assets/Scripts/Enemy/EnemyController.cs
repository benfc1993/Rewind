using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent pathFinder;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        while ( target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathFinder.SetDestination(target.position);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
