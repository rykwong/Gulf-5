using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int popUpIndex;
    public GameObject[] walls;
    public bool[] complete;
    public GameObject enemy;
    public GameObject enemy2;
    void Update()
    {
        if (popUpIndex == 0)
        {
            if (Input.GetButtonDown("Horizontal") && !complete[popUpIndex])
            {
                Debug.Log("Completed horizontal");
                complete[popUpIndex] = true;
                walls[popUpIndex].SetActive(false);
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetButtonDown("Jump") && !complete[popUpIndex])
            {
                Debug.Log("Completed jumping");
                complete[popUpIndex] = true;
                walls[popUpIndex].SetActive(false);
            }
        }
        else if (popUpIndex == 2)
        {
            if (!enemy && !complete[popUpIndex])
            {
                Debug.Log("Completed Attacking");
                complete[popUpIndex] = true;
                walls[popUpIndex].SetActive(false);
            }
        }
        else if (popUpIndex == 3)
        {
            if (!enemy2 && !complete[popUpIndex])
            {
                Debug.Log("Completed Shooting");
                complete[popUpIndex] = true;
                walls[popUpIndex].SetActive(false);
            }
        }
        
    }
}
