using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool unlocked;
    [SerializeField] private bool levelExit;
    [SerializeField] private string toLevel;
    private GameObject player;
    private GameObject menuManager;
    private Transform camera;
    private bool triggered = false;



    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
        menuManager = GameObject.Find("MenuManager");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        // Debug.Log("Player: " + player.transform.position + " " + gameObject.name + ": " + transform.position + "Dist: " + Vector2.Distance(player.transform.position,transform.position));
        if (Input.GetKeyDown(KeyCode.T) && unlocked && !triggered && Vector2.Distance(player.transform.position,transform.position) < 2f)
        {
            triggered = true;
            StartCoroutine(transport());
        }

    }

    private IEnumerator transport()
    {
        yield return new WaitForSeconds(0.5f);
        if (levelExit)
        {
            menuManager.GetComponent<SceneTransitions>().LoadTransition(toLevel);
        }
        else
        {
            player.transform.position = target.transform.position;
            camera.position = target.transform.position;
            triggered = false;
        }
    }

    public void ToggleLock()
    {
        unlocked = !unlocked;
    }
    
}
