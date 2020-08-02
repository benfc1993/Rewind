using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity
{
    public float moveSpeed = 5;
    public float dashSpeed = 10;
    private float dashing = 0;
    private float dashTime = 0.0f;
    public float startDashTime;
    private int direction;
    public ParticleSystem dust;
    public ParticleSystem DeathEffect;
    public ParticleSystem DamageEffect;

    PlayerController controller;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dashTime == 0)
        {
            dust.Play();
            dashTime = startDashTime;
        }
        if(dashTime <= 0)
        {
            dashTime = 0;
            dashing = 0;
        } else
        {
            dashing = dashSpeed;
            dashTime -= Time.fixedDeltaTime;
        }
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * (moveSpeed + dashing);
        controller.Move(moveVelocity);
    }
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(DeathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathEffect.startLifetime);
        } else
        {
            Destroy(Instantiate(DamageEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DamageEffect.startLifetime);
        }

        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
