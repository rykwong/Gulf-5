using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 moveDirection = other.transform.position - transform.position;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * 100f);
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * 100f);
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
