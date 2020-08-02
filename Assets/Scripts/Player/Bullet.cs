using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float currentSpeed;
    public bool rewinding = false;
    public bool fastforward = false;
    public LayerMask enemyCollisionMask;
    public LayerMask wallCollisionMask;
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

    private void Update()
    {
        float distToMove = currentSpeed * Time.deltaTime;
        CheckCollisions(distToMove);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = 0;
        }
        float moveSpeed = currentSpeed;
        if (rewinding)
        {
            transform.position += shootDir.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        }
    }

    void CheckCollisions(float distToMove)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distToMove, enemyCollisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitEnemy(hit);
        }
        if (Physics.Raycast(ray, out hit, distToMove, wallCollisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitWall(hit);
        }
    }

    void OnHitEnemy(RaycastHit hit)
    {
        print(hit.collider.gameObject.name);
    }

    void OnHitWall(RaycastHit hit)
    {
        string tag = hit.collider.gameObject.tag;
        print(hit.collider.gameObject.name);
        if (!rewinding)
        {
            if (tag == "OuterWall")
            {
                currentSpeed = 0;
            }
            if (tag == "Wall" && !fastforward)
            {
                currentSpeed = 0;
            }
        }
            // GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rewinding && other.tag == "Gun")
        {
            playerController.hasShot = false;
            Player.GetComponent<PlayerShoot>().CurrentBullet = null;
            Player.GetComponent<PlayerShoot>().rewinding = false;
            Destroy(gameObject);
        }
    }
}
