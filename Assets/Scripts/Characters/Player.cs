using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float runSpeed = 5.0f;
    private float walkSpeed = 3.0f;
    public override void Start()
    {
        base.Start();
        speed = runSpeed;
    }

    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        Jump();
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        anim.SetFloat("speed", Mathf.Abs(direction));
        Flip();
    }
    
    protected override void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                doublejump = true;
                anim.SetTrigger("jump");
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                    anim.SetTrigger("jump");
                }
            }

        }
        
        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("falling",true);
        }
    }
}
