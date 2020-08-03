using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    PlayerController Controller;
    public SmartBullet CurrentBullet;
    public Transform[] bulletTypes;
    public bool rewinding;
    public bool fastForward;
    MuzzleFlash muzzleFlash;
    MuzzleFlashSecondary muzzleFlashSecondary;
    LineRenderer Line;

    public TMPro.TextMeshProUGUI ammoCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Controller.OnShoot += PlayerShootProjectiles_OnShoot;
        Controller.OnRewind += PlayerShootProjectiles_OnRewind;
        Line = GetComponent<LineRenderer>();
        muzzleFlash = GetComponent<MuzzleFlash>();
        muzzleFlashSecondary = GetComponent<MuzzleFlashSecondary>();
        ammoCounter.text = "1/1";
    }

    private void PlayerShootProjectiles_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
        if(e.fastforward)
        {
        muzzleFlashSecondary.Activate();

        }
        else
        {
        muzzleFlash.Activate();

        }
        ammoCounter.text = "0/1";
        Transform bulletTransform = Instantiate(bulletTypes[Controller.currentEquipped], e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = e.shootPosition - e.gunEndPointPosition;
        bulletTransform.rotation = Controller.GunEnd.rotation;
        switch (Controller.currentEquipped)
        {
            case 0:
                CurrentBullet = bulletTransform.GetComponent<SmartBullet>();
                break;
            case 1:
                CurrentBullet = bulletTransform.GetComponent<SmartBullet>() as Shell;
                break;
            case 2:
                CurrentBullet = bulletTransform.GetComponent<SmartBullet>();
                break;


        }
        CurrentBullet.GunEnd = Controller.GunEnd;
        CurrentBullet.fastforward = e.fastforward;
    }
    private void PlayerShootProjectiles_OnRewind(object sender, PlayerController.OnRewindEventArgs e)
    {
        if(CurrentBullet && (CurrentBullet.currentSpeed == 0 || Vector3.Distance(CurrentBullet.transform.position, transform.position) > 10))
        {
            
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

        }
    }
}
