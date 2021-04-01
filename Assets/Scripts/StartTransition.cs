using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTransition : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private string scene;
    void Start()
    {
        if(time != 0)
            StartCoroutine(end());
    }

    private IEnumerator end()
    {
        yield return new WaitForSeconds(time);
        GetComponent<SceneTransitions>().LoadTransition(scene);
    }
}
