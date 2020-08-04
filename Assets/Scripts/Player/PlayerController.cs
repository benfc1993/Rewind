using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Boolean hasShot = false;
    Vector3 velocity;
    Rigidbody myRigidbody;
    public int currentEquipped = 0;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public event EventHandler<OnRewindEventArgs> OnRewind;
    public AudioSource Music;
    private float MusicTime;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public bool fastforward;
    }
    public class OnRewindEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
    }

    public Transform GunEnd;

    private PlayerLookAt PlayerLookAt;

    private void Start () {
        PlayerLookAt = GetComponent<PlayerLookAt> ();
        myRigidbody = GetComponent<Rigidbody> ();
    }

    private void FixedUpdate () {
        HandleShooting ();
        myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);

    }

    public void Move (Vector3 _velocity) {
        velocity = _velocity;
    }

    private void HandleShooting () {
        if (Input.GetMouseButton (0) && !hasShot) {
            // aimAnimator.SetTrigger("Shoot");
            hasShot = true;
            OnShoot?.Invoke (this, new OnShootEventArgs {
                gunEndPointPosition = GunEnd.position,
                    shootPosition = new Vector3 (PlayerLookAt.point.x, GunEnd.position.y, PlayerLookAt.point.z),
                    fastforward = false
            });
        }
        if (Input.GetMouseButton (1) && playerShoot.CurrentBullet && playerShoot.CurrentBullet.currentSpeed == 0) {

            Music.time = MusicTime;
            Music.Play ();
            OnRewind?.Invoke (this, new OnRewindEventArgs {
                gunEndPointPosition = GunEnd.position,
            });
        }
        if (Input.GetKeyDown (KeyCode.Q) && !hasShot) {
            // aimAnimator.SetTrigger("Shoot");
            if (player.charge > 0) {
                player.charge -= 1;
                hasShot = true;
                OnShoot?.Invoke (this, new OnShootEventArgs {
                    gunEndPointPosition = GunEnd.position,
                        shootPosition = new Vector3 (PlayerLookAt.point.x, GunEnd.position.y, PlayerLookAt.point.z),
                        fastforward = true
                });
            }
        }
        if (Input.GetKeyDown (KeyCode.LeftShift)) {
            MusicTime = Music.time;
            Music.Pause ();
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Battery" && player.charge < 2) {
            other.GetComponent<Battery> ().die ();
            print (player.charge);
            player.charge += 1;

        }
    }

}