using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool unlocked;
    [SerializeField] private float initTime;
    private GameObject player;
    private Transform camera;
    private bool triggered = false;
    private float time;
    
    
    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
        time = initTime;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        Debug.Log("Player: " + player.transform.position + " " + gameObject.name + ": " + transform.position + "Dist: " + Vector2.Distance(player.transform.position,transform.position));
        if (Input.GetKeyDown(KeyCode.T) && unlocked && !triggered && Vector2.Distance(player.transform.position,transform.position) < 2f)
        {
            triggered = true;
            StartCoroutine(transport());
        }

    }

    private IEnumerator transport()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position = target.transform.position;
        camera.position = target.transform.position;
        triggered = false;
    }
}
