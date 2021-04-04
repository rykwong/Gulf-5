using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }


    private IEnumerator wait()
    {
        yield return new WaitForSeconds(20f);
        main.SetActive(true);
    }
}
