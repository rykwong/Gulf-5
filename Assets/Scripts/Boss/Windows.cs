using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    private GameObject boss;

    [SerializeField] private float speed = 100f;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private float angle;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(angle*Mathf.Deg2Rad), Mathf.Cos(angle*Mathf.Deg2Rad)) * radius; 
        transform.position = (Vector2)boss.transform.position + offset;
    }
    
    
}
