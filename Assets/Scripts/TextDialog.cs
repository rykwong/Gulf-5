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
    private GameObject player;
    public GameObject button;
    private bool triggered;
    private bool finished;
    public bool transition;

    private void Start()
    {
        player = GameObject.Find("Player");
        textDisplay = GetComponent<TextMeshProUGUI>();
        if (transition)
        {
            type();
        }
    }

    void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 4f && !triggered)
            {
                triggered = true;
                finished = false;
                StartCoroutine(Type());
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > 4f && finished)
            {
                textDisplay.text = "";
                triggered = false;
            }
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
        if (transition)
        {
            button.SetActive(true);
        }
    }

    public void type()
    {
        StartCoroutine(Type());
        
    }
}
