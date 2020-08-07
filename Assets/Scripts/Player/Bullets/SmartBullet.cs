using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBullet : MonoBehaviour
{
    public float damage = 1;
    public float speed;
    public float currentSpeed;
    public enum bulletTypes { basic, pellet, armorPiercing};
    public bulletTypes bulletType;

    private Vector3 shootDir;

    public bool hitShield = false;
    public bool rewinding = false;
    public bool fastforward = false;

    public LayerMask enemyCollisionMask;
    public LayerMask wallCollisionMask;
    public LayerMask shieldCollisionMask;
    public LayerMask furnitureCollisionMask;

    public Transform GunEnd;
    public GameObject Player;
    protected PlayerController playerController;
    public Transform _shell;

    public Light BulletHit;
    public ParticleSystem sparkEffect;

    protected virtual void Start()
    {
        Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();
        currentSpeed = speed;
        if(bulletType == bulletTypes.pellet)
        {
            Destroy(gameObject, 1.5f);
        }
    }
    private void Update()
    {
        if(Player && Player.transform)
        {
            float distToMove = currentSpeed * Time.deltaTime;
            CheckCollisions(distToMove);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = 0;
            }
            if (rewinding )
            {
                if(Vector3.Distance(playerController.GunEnd.transform.position, transform.position) < 1.5f)
                {
                    catchBullet();
                }
                shootDir = playerController.GunEnd.transform.position - transform.position;
                transform.position += shootDir.normalized * currentSpeed * Time.deltaTime;
            }
            else
            {
                shootDir = transform.forward;
                transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
            }
            if (Vector3.Distance(transform.position, playerController.GunEnd.transform.position) > 70 && !rewinding)
            {
                currentSpeed = 0;
            }

        }
    }

    void CheckCollisions(float distToMove)
    {
        Ray ray = new Ray(transform.position, shootDir);
        RaycastHit hit;
        if(currentSpeed > 0)
        {

            if (Physics.Raycast(ray, out hit, distToMove, shieldCollisionMask, QueryTriggerInteraction.Collide))
            {
                print("hit shield");
                Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 0.2f);
                OnHitShield(hit);
                FindObjectOfType<AudioManager>().Play("BulletHitShield");
            }
            if(!hitShield)
            {
                if (Physics.Raycast(ray, out hit, distToMove, enemyCollisionMask, QueryTriggerInteraction.Collide))
                {
                    Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 0.2f);
                    OnHitEnemy(hit.collider, hit.point);
                    FindObjectOfType<AudioManager>().Play("BulletHit");

                }

            }
            if (Physics.Raycast(ray, out hit, distToMove, wallCollisionMask, QueryTriggerInteraction.Collide))
            {
                Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 0.2f);
                OnHitWall(hit);
                FindObjectOfType<AudioManager>().Play("BulletHit");

            }
            if (Physics.Raycast(ray, out hit, distToMove, furnitureCollisionMask, QueryTriggerInteraction.Collide))
            {
                Destroy(Instantiate(BulletHit.gameObject, hit.point, Quaternion.identity) as GameObject, 0.2f);
                OnHitEnemy(hit.collider, hit.point);
                FindObjectOfType<AudioManager>().Play("BulletHit");
            }
        }
    }

    void OnHitEnemy(Collider c, Vector3 hitPoint)
    {
        IDamagable damagableObject = c.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            Vector3 direction = rewinding ? transform.forward * -1 : transform.forward;
            damagableObject.TakeHit(damage, hitPoint, direction);
            if(bulletType == bulletTypes.pellet)
            {
                    Destroy(gameObject);
            }
        }
    }

    void OnHitWall(RaycastHit hit)
    {

        string tag = hit.collider.gameObject.tag;
        Destroy(Instantiate(sparkEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, -transform.forward)) as GameObject, sparkEffect.main.startLifetimeMultiplier);
        if (!rewinding)
        {
            if (tag == "OuterWall")
            {
                currentSpeed = 0;
                if (gameObject && bulletType == bulletTypes.pellet)
                {
                    Destroy(gameObject);
                }
            }
            if (gameObject &&  bulletType != bulletTypes.pellet && tag == "Wall" && !fastforward)
            {
                currentSpeed = 0;
            }
            if (_shell &&  bulletType == bulletTypes.pellet && !_shell.GetComponent<Shell>().fastforward)
            {
                Destroy(gameObject);
            }
        }
            // GameObject.Destroy(gameObject);
    }

    void OnHitShield(RaycastHit hit)
    {
        if(bulletType != bulletTypes.armorPiercing)
        {
            print(Vector3.Angle(transform.forward, hit.transform.forward));
            if ((Vector3.Angle(transform.forward, hit.transform.forward) < 90 && Vector3.Angle(transform.forward, hit.transform.forward) > -90 && !rewinding) || ((Vector3.Angle(transform.forward, hit.transform.forward) < 90 && Vector3.Angle(transform.forward, hit.transform.forward) > -90 && rewinding)))
            {
                hitShield = true;
                Destroy(Instantiate(sparkEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, -transform.forward)) as GameObject, sparkEffect.main.startLifetimeMultiplier);
                rewinding = true;
                currentSpeed = speed / 2;

            }
            if (gameObject && bulletType == bulletTypes.pellet)
            {
                Destroy(gameObject);
            }
        }
    }

    private void catchBullet()
    { 
        FindObjectOfType<AudioManager>().Play("CollectBullet");
        playerController.hasShot = false;
        Player.GetComponent<PlayerShoot>().CurrentBullet = null;
        Player.GetComponent<PlayerShoot>().rewinding = false;
        Player.GetComponent<PlayerShoot>().ammoCounter.text = "1/1";
        Destroy(gameObject);
    }
}
