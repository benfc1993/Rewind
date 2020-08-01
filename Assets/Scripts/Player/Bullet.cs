using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float currentSpeed;
    public bool rewinding = false;
    private GameObject Player;
    private Vector3 shootDir;
    PlayerController playerController;

    private void Start()
    {
        Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();
        currentSpeed = speed;
    }
    public void SetDir(Vector3 _shootDir)
    {
        shootDir = _shootDir;
    }

    private void FixedUpdate()
    {
        float moveSpeed = currentSpeed;
        if(rewinding)
        {
            transform.position += shootDir * moveSpeed * Time.fixedDeltaTime;
        } else
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * currentSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(rewinding);
        Debug.Log(other.tag);
        if(rewinding && other.tag == "Gun")
        {
            playerController.hasShot = false;
            Player.GetComponent<PlayerShoot>().CurrentBullet = null;
            Player.GetComponent<PlayerShoot>().rewinding = false;
            Destroy(gameObject);
        }
        if (other.tag == "Wall")
        {
            Debug.Log("hit");
            //Hit target
            currentSpeed = 0;
        }
    }
}
