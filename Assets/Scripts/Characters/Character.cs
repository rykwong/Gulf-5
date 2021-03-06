using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float direction;
    protected float initScale;
    protected float curDirection;

    [Header("Jump Variables")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float overlap;
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected float jumpForce;
    protected bool doublejump = true;
    protected bool onGround;

    [Header("Attack Variables")] 
    [SerializeField] protected Transform attackCheck;
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected float attackRate = 2f;
    [SerializeField] protected float timeShots;
    [SerializeField] protected float initTime;
    [SerializeField] protected GameObject projectile;
    protected float attackTime = 0f;
    [SerializeField] protected bool dead;

    [Header("Character Stats")] 
    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    
    protected Rigidbody2D rb2d;
    protected Animator anim;

    #region mono
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
    #endregion

    #region movement
    protected virtual void Move()
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
        if(direction != 0){
            transform.localScale = new Vector2(initScale * direction, transform.localScale.y);
            curDirection = direction;
        }
    }
    
    #endregion

    #region attack
    protected virtual void Attack()
    {
        anim.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(gameObject.name + " hit " + enemy.name);
            Vector3 moveDirection = transform.position - enemy.transform.position;
            GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * -1000f);
            enemy.GetComponent<Character>().TakeDamage(attack);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("hit");
        Debug.Log(gameObject.name + " has " + health + " remaining");
        if (health <= 0)
        {
            anim.SetTrigger("hit");
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        anim.SetBool("dead",true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        dead = true;
    }

    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,overlap);
    }

    protected virtual void HandleLayers()
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
