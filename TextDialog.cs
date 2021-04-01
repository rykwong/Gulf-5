using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;

public class TextDialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool triggered = true;
    public float delay;

    void Update()
    {
        
        
        if (textDisplay.enabled == true && triggered == true) 
        {
            
            StartCoroutine(Type());
            triggered = false;

        }

        
    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(delay);
        textDisplay.enabled = false;
    }
}
