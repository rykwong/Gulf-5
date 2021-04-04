using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private GameObject count;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player picked up: " + gameObject.name);
            count.GetComponent<CollectCount>().IncrementCount();
            Destroy(gameObject);
        }
    }
}
