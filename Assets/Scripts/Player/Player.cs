using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {
    public float moveSpeed = 5;

    public float bounceSpeed = 15;
    public float bounceTime = 0.25f;

    public float dashSpeed = 10;
    private float dashing = 0;
    private float dashTime = 0.0f;
    public float startDashTime;

    public int charge = 2;

    public ParticleSystem dust;
    public ParticleSystem DeathEffect;
    public ParticleSystem DamageEffect;
    public GameObject[] bulletUI;

    public HealthBar healthBar;
    public HealthBar chargeBar;

    public LayerMask wallCollisionMask;

    float bouncing;
    PlayerController controller;
    // Start is called before the first frame update
    protected override void Start () {
        base.Start ();
        controller = GetComponent<PlayerController> ();
        //healthBar.SetMaxHealth(startingHealth);
    }

    // Update is called once per frame
    private void Update () {
        handleInputs ();
    }
    void FixedUpdate () {
        handleTimers ();
        handleMovement ();
    }

    void handleTimers () {
        if (dashTime <= 0) {
            dashTime = 0;
            dashing = 0;
        } else {
            dashing = dashSpeed;
            dashTime -= Time.fixedDeltaTime;
        }
        if (bouncing <= 0) {
            bouncing = 0;
        } else {
            bouncing -= Time.fixedDeltaTime;
        }
    }

    void handleMovement () {
        if (Input.GetKeyDown (KeyCode.Space) && dashTime == 0) {
            dust.Play ();
            dashTime = startDashTime;
        }
        if (bouncing == 0) {
            Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
            float distToMove = (moveSpeed + dashing) * Time.deltaTime;
            Vector3 moveVelocity = CheckCollisions (distToMove, moveInput);
            controller.Move (moveVelocity);
        }
    }

    void handleInputs () {
        if (Input.GetKeyDown (KeyCode.Alpha1) && !controller.hasShot) {
            controller.currentEquipped = 0;
        }
        if (Input.GetKeyDown (KeyCode.Alpha2) && !controller.hasShot) {
            controller.currentEquipped = 1;
        }
        if (Input.GetKeyDown (KeyCode.Alpha3) && !controller.hasShot) {
            controller.currentEquipped = 2;
        }
    }

    void handleUI () {
        if (!bulletUI[controller.currentEquipped].activeSelf) {
            for (int i = 0; i < bulletUI.Length; i++) {
                bulletUI[i].SetActive (false);
            }
            bulletUI[controller.currentEquipped].SetActive (true);
        }
    }
    Vector3 CheckCollisions (float distToMove, Vector3 moveInput) {
        Ray ray = new Ray (transform.position, moveInput);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, distToMove, wallCollisionMask, QueryTriggerInteraction.Collide)) {
            Vector3 bounce = Vector3.Reflect (moveInput.normalized, hit.normal) * (bounceSpeed);
            bounce.y = 0;
            bouncing = bounceTime;
            return bounce;
        } else {
            return moveInput.normalized * (moveSpeed + dashing);
        }
    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        if (damage >= health) {
            Destroy (Instantiate (DeathEffect.gameObject, transform.position, Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, DeathEffect.main.startLifetimeMultiplier);
        } else {
            Destroy (Instantiate (DamageEffect.gameObject, transform.position, Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, DamageEffect.main.startLifetimeMultiplier);
        }
        base.TakeHit (damage, hitPoint, hitDirection);

        //healthBar.SetHealth(health);
    }
}