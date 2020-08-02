using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    public float dashSpeed = 10;
    private float dashing = 0;
    private float dashTime = 0.0f;
    public float startDashTime;
    private int direction;
    public ParticleSystem dust;

    PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
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
}
