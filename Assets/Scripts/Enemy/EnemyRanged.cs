using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyController
{
    public Transform bullet;
    public Transform barrel;
    public float startingFireRate;
    float fireRate = 0;
    MuzzleFlash muzzleFlash;
    public enum RangeType { Pistol, Rifle};
    public RangeType rangeType;
    // Update is called once per frame
    private void Awake()
    {
        muzzleFlash = GetComponent<MuzzleFlash>();
    }
    private void Update()
    {
        if(fireRate == 0)
        {
            if(canSeePlayer() )
            {
                Shoot();
            }
        }
        else
        {
            if(fireRate <= 0)
            {
                fireRate = 0;
            } else
            {
                fireRate -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        if(rangeType == RangeType.Pistol)
        {
            FindObjectOfType<AudioManager>().Play("PistolShot");
        }
        if (rangeType == RangeType.Rifle)
        {
            FindObjectOfType<AudioManager>().Play("RifleShot");
        }
        muzzleFlash.Activate();
        Transform bulletTransform = Instantiate(bullet, barrel.position, Quaternion.identity);

        Vector3 shootDir = Player.position - barrel.position;
        bulletTransform.rotation = barrel.rotation;
        fireRate = startingFireRate;
    }
}
