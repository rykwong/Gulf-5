using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private GameObject transformer;
    [SerializeField] private GameObject boss;
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        line.SetPosition(0,transformer.transform.position);
        line.SetPosition(1,boss.transform.position);
    }
    
}
