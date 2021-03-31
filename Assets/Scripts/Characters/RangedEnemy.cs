using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;

    [SerializeField] private float timeShots;
    [SerializeField] private float initTime;

    [SerializeField] private GameObject projectile;

    public override void Start()
    {
        base.Start();
        timeShots = initTime;
    }

    protected override void Patrol()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist < 5f)
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
            if(dist < 4f)
            {
                anim.SetFloat("speed",0);
                if (timeShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeShots = initTime;
                }
                else
                {
                    timeShots -= Time.deltaTime;
                }
            }
            else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime); 
                anim.SetFloat("speed", speed);
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            anim.SetFloat("speed", speed);
        }
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        if (!groundInfo.collider)
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
