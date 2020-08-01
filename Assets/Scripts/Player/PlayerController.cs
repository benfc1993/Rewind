using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Vector3 velocity;
    Rigidbody myRigidbody;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public event EventHandler<OnShootEventArgs> Rewind;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    public Transform GunEnd;

    private PlayerLookAt PlayerLookAt;


    private void Start()
    {
        PlayerLookAt = GetComponent<PlayerLookAt>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleShooting();
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);

    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            // aimAnimator.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = GunEnd.position,
                shootPosition = new Vector3(PlayerLookAt.point.x, GunEnd.position.y, PlayerLookAt.point.z),
            });
        }
        if (Input.GetMouseButton(1))
        {

        }
    }

}
