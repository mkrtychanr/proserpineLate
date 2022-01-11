using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalMode : MonoBehaviour
{
    [SerializeField] private GameObject stand;
    [SerializeField] private GameObject walk;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject falling;
    [SerializeField] private GameObject xDash;
    [SerializeField] private GameObject yDash;
    public float speed;
    private float startX;
    private float startY;
    private bool turn = false;
    private Rigidbody2D rb;
    private bool jumpStatus = false;
    public bool fall = false;
    private bool horizontalDash = false;
    private bool verticalDash = false;
    private void Walk(int direction){
        Turn(direction);
        if (!horizontalDash){
            stand.SetActive(false);
            up.SetActive(false);
            falling.SetActive(false);
            xDash.SetActive(false);
            yDash.SetActive(false);
            walk.SetActive(true);
        }
        transform.position = transform.position + new Vector3 (direction * speed * 0.05f, 0, 0);
    }

    private void Stand(){
        up.SetActive(false);
        falling.SetActive(false);
        walk.SetActive(false);
        xDash.SetActive(false);
        yDash.SetActive(false);
        stand.SetActive(true);
    }

    private void Turn(int direction){
        if (direction > 0) turn = false;
        else turn = true;
        stand.GetComponent<SpriteRenderer>().flipX = turn;
        walk.GetComponent<SpriteRenderer>().flipX = turn;
        up.GetComponent<SpriteRenderer>().flipX = turn;
        falling.GetComponent<SpriteRenderer>().flipX = turn;
        xDash.GetComponent<SpriteRenderer>().flipX = turn;
        yDash.GetComponent<SpriteRenderer>().flipX = turn;
    }

    private void HorizontalDash(int direction){
        stand.SetActive(false);
        walk.SetActive(false);
        up.SetActive(false);
        falling.SetActive(false);
        yDash.SetActive(false);
        xDash.SetActive(true);
        Turn(direction);
        startX = transform.position.x;
        rb.velocity = new Vector2 (direction*50, 0);
        horizontalDash = true;
    }

    private void HorizontalDashCheck(){
        if (horizontalDash){
            stand.SetActive(false);
            walk.SetActive(false);
            up.SetActive(false);
            falling.SetActive(false);
            yDash.SetActive(false);
            xDash.SetActive(true);
        }
        if (horizontalDash && Math.Abs(transform.position.x - startX) >= 10){
            rb.velocity = new Vector2 (0, 0.01f);
            horizontalDash = false;
        }
    }
    private void FallCheck(){
        if (rb.velocity.y == 0){
            fall = false;
            falling.SetActive(false);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stand.SetActive(true);
    }

    private void Update() {
        HorizontalDashCheck();
        FallCheck();
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0 && !horizontalDash && rb.velocity.x == 0) HorizontalDash(1);
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0 && !horizontalDash && rb.velocity.x == 0) HorizontalDash(-1);
        if (rb.velocity.y == 0) jumpStatus = false; 
        if (Input.GetKey(KeyCode.D)) Walk(1);
        if (Input.GetKey(KeyCode.A)) Walk(-1);
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) Stand();
        if (Input.GetKey(KeyCode.W) && !jumpStatus && !horizontalDash){
            startY = transform.position.y;
            jumpStatus = true;
            
            rb.velocity = new Vector2 (0, 15);
        }
        if (transform.position.y > startY+2 && transform.position.y < startY+2.3f){
            rb.AddForce(new Vector2 (0, 5f));
        }
        if (transform.position.y > startY+2.5 && transform.position.y < startY+2.8f){
            rb.AddForce(new Vector2 (0, 2f));
        }
        if (transform.position.y > startY+3 && transform.position.y < startY+3.2f){
            rb.AddForce(new Vector2 (0, 1.5f));
        }
        if (transform.position.y > startY+4){
            fall = true;
            rb.velocity = new Vector2 (rb.velocity.x, -8f);
        }
        if (transform.position.y > startY+3 && transform.position.y < startY+3.2f && fall){
            rb.AddForce(new Vector2 (0, -12f));
        }
        if (transform.position.y > startY+2.5 && transform.position.y < startY+2.8f && fall){
            rb.AddForce(new Vector2 (0, -20f));
        }
        if (rb.velocity.y < 0){
            stand.SetActive(false);
            walk.SetActive(false);
            xDash.SetActive(false);
            yDash.SetActive(false);
            up.SetActive(false);
            falling.SetActive(true);
        }
    }
}// памятка. сделай так, чтобы герой летел по кривой а не по ломанной.