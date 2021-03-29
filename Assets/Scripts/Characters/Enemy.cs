using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public bool revive;
    public override void Start()
    {
        base.Start();
        health = maxHealth;
    }

    protected override void Jump() { }

    protected override void Die()
    {
        Debug.Log(gameObject.name + " died!");
        anim.SetBool("dead",true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        if(revive)
            StartCoroutine(Wait(1));
    }

    private IEnumerator Wait(float time)
    {
        
        yield return new WaitForSeconds(time);
        anim.SetBool("dead",false);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Collider2D>().enabled = true;
        health = maxHealth;
    }
}
