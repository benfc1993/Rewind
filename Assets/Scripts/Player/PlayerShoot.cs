using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    PlayerController Controller;
    public Bullet CurrentBullet;
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
        Debug.Log("Shoot!");
        Transform bulletTransform = Instantiate(bullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = e.shootPosition - e.gunEndPointPosition;
        bulletTransform.rotation = Controller.GunEnd.rotation;
        CurrentBullet = bulletTransform.GetComponent<Bullet>();
        CurrentBullet.SetDir(shootDir);
    }
    private void PlayerShootProjectiles_OnRewind(object sender, PlayerController.OnRewindEventArgs e)
    {
        Debug.Log("Rewind!");

        Vector3 shootDir = e.gunEndPointPosition - CurrentBullet.transform.position;
        CurrentBullet.SetDir(shootDir);
        CurrentBullet.currentSpeed = CurrentBullet.speed;
        CurrentBullet.rewinding = true;
        rewinding = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CurrentBullet)
        {
            Line.startWidth = 0.05f;
            Line.endWidth = 0.05f;
            Line.positionCount = 2;
            Line.SetPosition(0,Controller.GunEnd.position);
            Line.SetPosition(1, CurrentBullet.transform.position);
        } else
        {
            Line.positionCount = 0;
        }
        if (rewinding)
        {
            Debug.Log("update vector");
            Vector3 shootDir = Controller.GunEnd.position - CurrentBullet.transform.position;
            CurrentBullet.SetDir(shootDir);
        }
    }
}
