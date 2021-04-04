using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectCount : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private int total;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = count + "/" + total;
    }

    public void IncrementCount()
    {
        count++;
        GetComponent<TextMeshProUGUI>().text = count + "/" + total;
    }
}
