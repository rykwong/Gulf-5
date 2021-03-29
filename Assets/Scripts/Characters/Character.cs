using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float direction;
    protected float initScale;

    [Header("Jump Variables")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float overlap;
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected float jumpForce;
    protected bool doublejump = true;
    protected bool onGround;

    [Header("Attack Variables")]
    
    [Header("Character Stats")]
    
    protected Rigidbody2D rb2d;
    protected Animator anim;
    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initScale = transform.localScale.x;
    }
    public virtual void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position,overlap,ground);
        if (onGround)
        {
            anim.ResetTrigger("jump");
            anim.SetBool("falling",false);
            doublejump = true;
        }
    }
    public virtual void FixedUpdate()
    {
        HandleMovement();
        HandleLayers();
    }

    protected void Move()
    {
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
    }

    protected abstract void Jump();
    protected virtual void HandleMovement()
    {
        Move();
    }

    protected void Flip()
    {
        if(direction != 0)
            transform.localScale = new Vector2(initScale * direction, transform.localScale.y);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,overlap);
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
