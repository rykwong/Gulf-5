using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float runSpeed = 5.0f;
    // private float walkSpeed = 3.0f;
    public int attackDamage = 25;
    public override void Start()
    {
        base.Start();
        speed = runSpeed;
        attack = attackDamage;
    }

    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        Jump();
        if(Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                attackTime = Time.time + 1f / attackRate;
            }
        }
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        anim.SetFloat("speed", Mathf.Abs(direction));
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
        }
    }

    protected override void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Button Pressed: " + doublejump);
            if (onGround)
            {
                Debug.Log("Ground Jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                anim.SetTrigger("jump");
            }
            else
            {
                if(Input.GetButtonDown("Jump"))
                {
                    if (doublejump)
                    {
                        Debug.Log("Double Jump");
                        doublejump = false;
                        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                        anim.SetTrigger("jump");
                    }
                }
            }

        }
        
        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("falling",true);
        }
    }
}
