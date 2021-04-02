using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{ 
    public override void Start()
    {
        base.Start();
        timeShots = initTime;
    }

    protected override void Patrol()
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
}
