using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.transform.position = GameObject.Find("Start").transform.position;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
