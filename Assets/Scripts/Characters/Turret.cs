using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject indicator;
    [SerializeField] private float force;
    
    [SerializeField] protected float timeShots;
    [SerializeField] protected float spray;
    [SerializeField] protected float initTime;
    
    private Transform player;

    private bool detected = false;
    
    private Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = player.position;
        direction = target - (Vector2)transform.position;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, range,mask);

        if (ray)
        {
            if (!detected)
            {
                detected = true;
                // Debug.Log("Detected");
                indicator.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            if (detected)
            {
                detected = false;
                // Debug.Log("No longer detected");
                indicator.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        if (detected)
        {
            transform.right = -direction;
            if (timeShots <= 0)
            {
                Debug.Log("Turret shooting");
                StartCoroutine(Shoot());
                timeShots = initTime;
            }
            else
            {
                timeShots -= Time.deltaTime;
            }
        }

    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(laser, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction*force);
            yield return new WaitForSeconds(spray);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
