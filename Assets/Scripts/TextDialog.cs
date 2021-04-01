using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;

public class TextDialog : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    public string sentence;
    public float typingSpeed;
    private Transform player;
    private bool triggered;
    private bool finished;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position,player.position) < 4f && !triggered)
        {
            triggered = true;
            finished = false;
            StartCoroutine(Type());
        }
        else if(Vector2.Distance(transform.position,player.position) > 4f && finished)
        {
            textDisplay.text = "";
            triggered = false;
        }
        

        
    }
    IEnumerator Type()
    {
        foreach(char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        finished = true;
    }
}
