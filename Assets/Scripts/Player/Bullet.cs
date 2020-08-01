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

    private void FixedUpdate()
    {
        float moveSpeed = speed;
        transform.position += shootDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Debug.Log(other.tag);
        if (other.tag == "Wall")
        {
            Debug.Log("hit");
            //Hit target
            speed = 0;
        }
    }
}
