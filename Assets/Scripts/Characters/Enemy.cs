using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Variables")]
    public int maxHealth = 100;
    public bool revive;
    public float distance;
    protected bool movingRight = true;
    protected GameObject player;

    [SerializeField] protected float playerDist;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallDist;
    [SerializeField] protected LayerMask wall;
    [SerializeField] protected LayerMask playerMask;


    public override void Start()
    {
        base.Start();
        health = maxHealth;
        player = GameObject.Find("Player");
    }

    public override void Update()
    {
        base.Update();
        if(!dead)
            Patrol();
    }

    protected virtual void Patrol()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D playerLeft = Physics2D.Raycast(attackCheck.position, Vector2.left, playerDist,playerMask);
        RaycastHit2D playerRight = Physics2D.Raycast(attackCheck.position, Vector2.right, playerDist,playerMask);
        RaycastHit2D wallLeft = Physics2D.Raycast(wallCheck.position, Vector2.left, playerDist,wall);
        RaycastHit2D wallRight = Physics2D.Raycast(wallCheck.position, Vector2.right, playerDist,wall);
        if ((playerLeft.collider && !wallLeft.collider) || (playerRight.collider && !wallRight.collider))
        {
            if (transform.position.x > player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
            if(Time.time >= attackTime && dist < 2f)
            {
                Attack();
                attackTime = Time.time + 1f / attackRate;
            }
            else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack") && dist > 2f)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime); 
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); 
        }
        
        anim.SetFloat("speed", speed);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        bool wallCollide = Physics2D.OverlapCircle(wallCheck.position, wallDist, wall);
        if (!groundInfo.collider || wallCollide)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            movingRight = !movingRight;
        }
    }

    protected override void Jump() { }

    protected override void Die()
    {
        Debug.Log(gameObject.name + " died!");
        dead = true;
        anim.SetBool("dead",true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
        // if(revive)
        //     StartCoroutine(Wait(3));
    }

    // private IEnumerator Wait(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     Debug.Log("Revived");
    //     anim.SetBool("dead",false);
    //     GetComponent<Rigidbody2D>().gravityScale = 1;
    //     GetComponent<Collider2D>().enabled = true;
    //     health = maxHealth;
    //     dead = false;
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            movingRight = !movingRight;
        }
    }
}
