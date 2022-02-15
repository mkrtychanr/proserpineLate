using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{

    Vector2 from;

    Vector2 destination;

    float bulletSpeed;

    bool launch = false;

    float l;

    float angle;

    float m;

    float x;

    float y;
    
    int i;

    
    protected void Init(float speed)
    {

        bulletSpeed = speed;

    }


    public void Launch(GameObject destinationObject)
    {
        destination = new Vector2(destinationObject.transform.position.x, destinationObject.transform.position.y);
        from = new Vector2(transform.position.x, transform.position.y);
        angle = Vector2.Angle(destination, from);
        transform.eulerAngles = new Vector3 (0f, 0f, angle);
        Debug.Log($"destination: x = {destination.x}, y = {destination.y}, from: x = {from.x}, y = {from.y}, angle: {angle}");
        //GetComponent<Rigidbody2D>().AddForce(transform.right * 1f , ForceMode2D.Impulse);
        //x = GetComponent<Rigidbody2D>().velocity.x;
        //y = GetComponent<Rigidbody2D>().velocity.y;
        //launch = true;
        
    }


    void Update()
    {
        if (launch)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag == "HeatBox")
        {
            Destroy(gameObject);
        }
    }

}
