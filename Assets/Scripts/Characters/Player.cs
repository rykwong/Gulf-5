using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    [Header("Player Variables")]
    public float runSpeed = 5.0f;
    public int attackDamage = 25;
    [SerializeField] protected LayerMask objectLayers;
    public float dir;

    public override void Start()
    {
        base.Start();
        speed = runSpeed;
        attack = attackDamage;
        dir = 1f;
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
        if (timeShots <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeShots = initTime;
            }
        }
        else
        {
            timeShots -= Time.deltaTime;
        }

        dir = curDirection;
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
            // Debug.Log("Button Pressed: " + doublejump);
            if (onGround)
            {
                // Debug.Log("Ground Jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                anim.SetTrigger("jump");
            }
            else
            {
                if(Input.GetButtonDown("Jump"))
                {
                    if (doublejump)
                    {
                        // Debug.Log("Double Jump");
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

    protected override void Attack()
    {
        base.Attack();
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, objectLayers);
        foreach (Collider2D temp in hitObjects)
        {
            Debug.Log(gameObject.name + " hit " + temp.name);
            temp.GetComponent<DestructibleItem>().TakeDamage(attack);
        }
    }
}
