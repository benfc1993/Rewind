using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    public Transform GunEnd;
    // private PlayerLookAt playerLookAt;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;

    private void Awake()
    {
        // playerLookAt = GetComponent<PlayerLookAt>();
        // aimTransform = transform.Find("Aim");
        // aimAnimator = aimTransform.GetComponent<Animator>();
        // aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        // aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        // Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        // Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        // float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Vector3 aimLocalScale = Vector3.one;
        // if (angle > 90 || angle < -90)
        // {
        //     aimLocalScale.y = -1f;
        // }
        // else
        // {
        //     aimLocalScale.y = +1f;
        // }
        // aimTransform.localScale = aimLocalScale;

        // playerLookAt.SetLookAtPosition(mousePosition);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            // aimAnimator.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = GunEnd.position,
                shootPosition = new Vector3(-0.9f, 3.3f, 1),
                // shellPosition = aimShellPositionTransform.position,
            });
        }
    }

}
