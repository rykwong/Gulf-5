using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWall : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float time;
    [SerializeField] private float initTime;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 moveDirection = other.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce( moveDirection.normalized * 1000f);
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (time <= 0)
        {
            StartCoroutine(Electrify());
            time = initTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private IEnumerator Electrify()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
