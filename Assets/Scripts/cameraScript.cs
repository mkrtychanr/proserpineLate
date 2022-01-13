using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private float camSpeed = 0.05f;
    [SerializeField] private float rangeDistance = 5f;
    private Transform body;
    private Rigidbody2D rb;
    private HeroController hc;
    private float delta;
   

    private void Start() 
    {
        body = hero.GetComponent<Transform>();
        rb = hero.GetComponent<Rigidbody2D>();
        hc = hero.GetComponent<HeroController>();
    }
    private void Update() 
    {
        //transform.position = new Vector3 (body.position.x, transform.position.y, transform.position.z);

        if (transform.position.x > body.position.x && transform.position.x - body.position.x > rangeDistance)
        {
            transform.position = new Vector3(body.position.x+rangeDistance, transform.position.y, transform.position.z);
        }

        if (transform.position.x < body.position.x && body.position.x - transform.position.x > rangeDistance)
        {
            transform.position = new Vector3(body.position.x-rangeDistance, transform.position.y, transform.position.z);
        }

        if (!hc.isMoving && transform.position.x < body.position.x)
        {
            delta = (body.position.x - transform.position.x)/camSpeed;
            transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        }

        if (!hc.isMoving && transform.position.x > body.position.x)
        {
            delta = (transform.position.x - body.position.x)/camSpeed;
            transform.position = new Vector3(transform.position.x - delta, transform.position.y, transform.position.z);
        }
    }
}
