using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 10;
    private Rigidbody2D rb2d;
    private Transform player;
    private float time = 1f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = Vector2.right * speed;
        }
        else if(transform.position.x > player.position.x)
        {
            rb2d.velocity = Vector2.left * speed;
        }
        else
        {
            if (player.GetComponent<Player>().dir > 0)
            {
                rb2d.velocity = Vector2.right * speed;
            }
            else
            {
                rb2d.velocity = Vector2.left * speed;
            }
        }
    }

    private void Update()
    {
        if (time <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 moveDirection = other.transform.position - transform.position;
            other.GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * 100f);
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
