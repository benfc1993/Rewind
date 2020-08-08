using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {
    public bool disabled;

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

    public LayerMask wallCollisionMask;
    AudioManager audioManager;
    float bouncing;
    PlayerController controller;
    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    protected override void Start () {
        base.Start ();
        healthBar.SetMaxHealth(startingHealth);
        controller = GetComponent<PlayerController> ();
    }

    // Update is called once per frame
    private void Update () {
        if (!dead && !disabled) {
            handleInputs ();
            handleUI();
        }
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
        if (Input.GetKeyDown (KeyCode.Space) && dashTime == 0 && !disabled) {
            dust.Play ();
            audioManager.Play("Dodge");
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
        if (Input.GetKeyDown (KeyCode.Alpha1) && controller.currentEquipped != 0  && !controller.hasShot) {
            controller.currentEquipped = 0;
            audioManager.ChangeSong(0);
        }
        if (Input.GetKeyDown (KeyCode.Alpha2) && controller.currentEquipped != 1 && !controller.hasShot) {
            controller.currentEquipped = 1;
            audioManager.ChangeSong(1);
        }
        if (Input.GetKeyDown (KeyCode.Alpha3) && controller.currentEquipped != 2 && !controller.hasShot) {
            controller.currentEquipped = 2;
            audioManager.ChangeSong(2);
        }
    }

    void handleUI () {
        if (!bulletUI[controller.currentEquipped].activeSelf) {
            print("change highlight");
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
            Die();
            Destroy(Instantiate(DeathEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.main.startLifetimeMultiplier);
        } else {
            Destroy (Instantiate (DamageEffect.gameObject, transform.position, Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, DamageEffect.main.startLifetimeMultiplier);
        }
        base.TakeHit (damage, hitPoint, hitDirection);

        healthBar.SetHealth(health);
    }

    protected override void Die()
    {
        dead = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Time.timeScale = 0.5f;
        audioManager.Play("Death");
        audioManager.Pause();
        FindObjectOfType<LevelManager>().levelFailed();
        StartCoroutine(Slowtime());
        Cursor.visible = true;
    }
    IEnumerator Slowtime()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
}