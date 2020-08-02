using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBullet : MonoBehaviour
{
    public float damage = 1;
    public float speed;
    public float currentSpeed;
    private Vector3 shootDir;

    public bool hitShield = false;
    public bool rewinding = false;
    public bool fastforward = false;

    public LayerMask enemyCollisionMask;
    public LayerMask wallCollisionMask;
    public LayerMask shieldCollisionMask;

    private GameObject Player;
    PlayerController playerController;

    public ParticleSystem sparkEffect;

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
        if (rewinding)
        {
            shootDir = Player.transform.position - transform.position;
            transform.position += shootDir.normalized * currentSpeed * Time.deltaTime;
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
        if (Physics.Raycast(ray, out hit, distToMove, shieldCollisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitShield(hit);
            hitShield = true;
        }
        if(!hitShield)
        {
            if (Physics.Raycast(ray, out hit, distToMove, enemyCollisionMask, QueryTriggerInteraction.Collide))
            {
                OnHitEnemy(hit.collider, hit.point);
            }

        }
        if (Physics.Raycast(ray, out hit, distToMove, wallCollisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitWall(hit);
        }
    }

    void OnHitEnemy(Collider c, Vector3 hitPoint)
    {
        print(c.gameObject.name);
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            Vector3 direction = rewinding ? transform.forward * -1 : transform.forward;
            damagableObject.TakeHit(damage, hitPoint, direction);
        }
    }

    void OnHitWall(RaycastHit hit)
    {
        string tag = hit.collider.gameObject.tag;
        print(hit.collider.gameObject.name);
        Destroy(Instantiate(sparkEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, -transform.forward)) as GameObject, sparkEffect.startLifetime);
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

    void OnHitShield(RaycastHit hit)
    {
        Destroy(Instantiate(sparkEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, -transform.forward)) as GameObject, sparkEffect.startLifetime);
        rewinding = true;
        SetDir(Player.transform.position - transform.position);
        currentSpeed = speed / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rewinding && other.gameObject.layer == 11)
        {
            playerController.hasShot = false;
            Player.GetComponent<PlayerShoot>().CurrentBullet = null;
            Player.GetComponent<PlayerShoot>().rewinding = false;
            Destroy(gameObject);
        }
    }
}
