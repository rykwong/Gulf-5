using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private string stat;
    private Dictionary<string, int> powerup = new Dictionary<string, int>() {{"attack",35},{"ranged",20}};
    private GameObject player;
    [SerializeField] private GameObject uiPowerUp;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player picked up: " + gameObject.name);
            if(stat == "attack")
                player.GetComponent<Player>().setAttack(powerup[stat]);
            else if(stat == "ranged")
                player.GetComponent<Player>().projDamage = powerup[stat];
            uiPowerUp.SetActive(true);
            Destroy(gameObject);
        }
    }
}
