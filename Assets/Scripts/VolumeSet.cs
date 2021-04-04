using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
