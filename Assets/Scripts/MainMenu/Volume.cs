using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    void Start()
    {
        float savedVol = PlayerPrefs.GetFloat("volume");
        GetComponent<Slider>().value = savedVol;
    }

    public void ChangeVol(float newValue) {
        float newVol = AudioListener.volume;
        newVol = newValue;
        PlayerPrefs.SetFloat("volume", newVol);
        AudioListener.volume = newVol;
    }
}
