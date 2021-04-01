using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckpointScript : MonoBehaviour
{

    public GameObject uiText;
    public int index;


    void Start()
    {
        uiText.GetComponent<TextMeshProUGUI>().enabled = false; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uiText.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

   // void OnCollisionExit2D(Collision2D collision)
   // {
   //      if(collision.gameObject.tag == "Player")
   //      {
   //         uiText.enabled = false;
   //         
   //     }
   // }
}
