using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{ 
    public enum Stage
    {
        Stage1,
        Stage2,
        Stage3
    }
    [SerializeField] protected float spray;
    [SerializeField] private float force;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject[] transformers;
    [SerializeField] private GameObject levelExit;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject audio;

    private Stage stage;
    private Vector2 aim;

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.Find("Player");
        stage = Stage.Stage1;
        audio = GameObject.Find("AudioManager");
        audio.SetActive(false);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    public override void Update()
    {
        Vector2 target = player.transform.position;
        aim = target - (Vector2)transform.position;
        transform.right = -aim;
        if (timeShots <= 0)
        {
            StartCoroutine(Shoot());
            timeShots = initTime;
        }
        else
        {
            timeShots -= Time.deltaTime;
        }
        TestAllTransformersDead();
    }
    

    private IEnumerator Shoot()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(laser, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(aim*force);
            yield return new WaitForSeconds(spray);
        }
    }

    protected override void Move()
    {
    }

    protected override void HandleLayers()
    {
    }
    
    public override void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " has " + health + " remaining");
        switch (stage)
        {
            case Stage.Stage1:
                if (health <= 150)
                {
                    StartNext();
                }

                break;
            case Stage.Stage2:
                if (health <= 60)
                {
                    StartNext();
                }

                break;
            case Stage.Stage3:
                if (health <= 0)
                {
                    Die();
                }
                break;
        }
    }

    private void StartNext()
    {
        switch (stage)
        {
            case Stage.Stage1:
                stage = Stage.Stage2;
                shield.SetActive(true);
                break;
            case Stage.Stage2:
                stage = Stage.Stage3;
                initTime = 5;
                force = 200;
                break;
        }
        Debug.Log("Starting next stage: " + stage);
    }

    protected override void Die()
    {
        Debug.Log(gameObject.name + " died!");
        levelExit.GetComponent<Tube>().ToggleLock();
        win.SetActive(true);
        audio.SetActive(true);
        Destroy(gameObject);
    }
    
    private void TestAllTransformersDead() {
        bool allDead = true;
        foreach (GameObject transformer in transformers) {
            if (transformer != null)
            {
                Debug.Log(transformer + " still alive");
                allDead = false;
                break;
            }
        }
        if (allDead) {
            // All transformers are dead!
            Destroy(shield);
        }
    }
}
