using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    private void Update()
    {
        float moveSpeed = speed;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = collider.GetComponent<Target>();
        if (target != null)
        {
            //Hit target
            gameObject.speed = 0;
        }
    }
}
