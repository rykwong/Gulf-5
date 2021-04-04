using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private GameObject tutorialManager;
    private bool triggered;

    private void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            tutorialManager.GetComponent<TutorialManager>().popUpIndex++;
        }
    }
}
