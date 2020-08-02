using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    PlayerController Controller;
    public SmartBullet CurrentBullet;
    public bool rewinding;
    LineRenderer Line;
    // Start is called before the first frame update
    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Controller.OnShoot += PlayerShootProjectiles_OnShoot;
        Controller.OnRewind += PlayerShootProjectiles_OnRewind;
        Line = GetComponent<LineRenderer>();
    }

    private void PlayerShootProjectiles_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(bullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = e.shootPosition - e.gunEndPointPosition;
        bulletTransform.rotation = Controller.GunEnd.rotation;
        CurrentBullet = bulletTransform.GetComponent<SmartBullet>();
        CurrentBullet.SetDir(shootDir);
        CurrentBullet.fastforward = e.fastforward;
    }
    private void PlayerShootProjectiles_OnRewind(object sender, PlayerController.OnRewindEventArgs e)
    {
        if(CurrentBullet && (CurrentBullet.currentSpeed == 0 || Vector3.Distance(CurrentBullet.transform.position, transform.position) > 10))
        {
            Vector3 shootDir = e.gunEndPointPosition - CurrentBullet.transform.position;
            CurrentBullet.SetDir(shootDir);
            CurrentBullet.currentSpeed = CurrentBullet.speed;
            CurrentBullet.rewinding = true;
            rewinding = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CurrentBullet && (CurrentBullet.currentSpeed == 0 || rewinding))
        {
            Line.startWidth = 0.1f;
            Line.endWidth = 0.1f;
            Line.positionCount = 2;
            Line.SetPosition(0,Controller.GunEnd.position);
            Line.SetPosition(1, CurrentBullet.transform.position);
        } else
        {
            Line.positionCount = 0;
        }
        if (rewinding)
        {
            Vector3 shootDir = Controller.GunEnd.position - CurrentBullet.transform.position;
            CurrentBullet.SetDir(shootDir);
        }
    }
}
