using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        if (player.GetComponent<Player>().dir >= 0)
        { 
            rb2d.velocity = Vector2.right * speed;
        }
        else 
        { 
            rb2d.velocity = Vector2.left * speed;
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
        if (other.CompareTag("Enemy"))
        {
            Vector3 moveDirection = other.transform.position - transform.position;
            other.GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * 100f);
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Destructible"))
        {
            other.GetComponent<DestructibleItem>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
