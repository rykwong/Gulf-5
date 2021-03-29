using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [Header("Public Vars")]
    public float jumpForce;
    public bool onGround;
    private bool doublejump = true;

    [Header("Private Vars")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float overlap;
    [SerializeField] private LayerMask ground;
    
    [Header("Components")]
    private Rigidbody2D rb2d;
    private Animator anim;
    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position,overlap,ground);
        if (onGround)
        {
            anim.ResetTrigger("jump");
            anim.SetBool("falling",false);
            doublejump = true;
        }

        
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,overlap);
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!onGround)
        {
            anim.SetLayerWeight(1,1);
        }
        else
        {
            anim.SetLayerWeight(1,0);
        }
    }
}
