using System;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
    public Boolean hasShot = false;
    Vector3 velocity;
    Rigidbody myRigidbody;
    public int currentEquipped = 0;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public event EventHandler<OnRewindEventArgs> OnRewind;
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

    public GameObject batteryOne;
    public GameObject batteryTwo;


    private PlayerLookAt PlayerLookAt;
    private Player player;
    private PlayerShoot playerShoot;

    bool paused = false;
    private void Start () {
        PlayerLookAt = GetComponent<PlayerLookAt> ();
        myRigidbody = GetComponent<Rigidbody> ();
        player = GetComponent<Player> ();
        playerShoot = GetComponent<PlayerShoot> ();
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
            if(paused)
            {
                FindObjectOfType<AudioManager>().Resume();
                paused = false;
            }
            OnRewind?.Invoke (this, new OnRewindEventArgs {
                gunEndPointPosition = GunEnd.position,
            });
        }
        if (Input.GetKeyDown (KeyCode.Q) && !hasShot) {
            // aimAnimator.SetTrigger("Shoot");
            if (player.charge > 0) {
                player.charge -= 1;
                hasShot = true;
                SetBatteries();
                OnShoot?.Invoke (this, new OnShootEventArgs {
                    gunEndPointPosition = GunEnd.position,
                        shootPosition = new Vector3 (PlayerLookAt.point.x, GunEnd.position.y, PlayerLookAt.point.z),
                        fastforward = true
                });
            }
        }
        if (Input.GetKeyDown (KeyCode.E) && !paused && hasShot && playerShoot.CurrentBullet.currentSpeed != 0) {
            paused = true;
            FindObjectOfType<AudioManager>().Pause();
            playerShoot.CurrentBullet.currentSpeed = 0; 
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Battery" && player.charge < 2) {
            other.GetComponent<Battery> ().die ();
            print (player.charge);
            player.charge += 1;
            SetBatteries();
        }
    }

        public void SetBatteries()
        {
            switch (player.charge)
            {
                case 0:
                    batteryOne.SetActive(false);
                    batteryTwo.SetActive(false);
                    break;
                case 1:
                    batteryOne.SetActive(true);
                    batteryTwo.SetActive(false);
                    break;
                case 2:
                    batteryOne.SetActive(true);
                    batteryTwo.SetActive(true);
                    break;
            }
        }

}