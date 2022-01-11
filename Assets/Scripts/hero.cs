using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : gamePerson
{
    [SerializeField] private GameObject heroSprite;
    Ray2D rayDown;
    RaycastHit2D hit;
    public float fightSpeed;
    public float xDashSpeed;
    public double xDashTime;
    private bool xDash = false;
    private float coefficient;
    private DateTime xDashLastCall;
    public float xDashCallDown;
    private void Start() {
        fightSpeed = 1.5f;
        xDashTime = 0.2;
        xDashSpeed = 3;
        xDashCallDown = 0.3f;
        lastCall = DateTime.Now;
        rb = GetComponent<Rigidbody2D>();
        anim = heroSprite.GetComponent<Animator>();
        sprite = heroSprite.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.F)){
            if (onFight) onFight = false;
            else onFight = true;
        }
        if (onFight) {
            coefficient = fightSpeed;
            anim.SetBool("onFight", onFight);
        }

        else{ 
            coefficient = 1;
            anim.SetBool("onFight", onFight);
        }
        if (xDash && TimeLeft(xDashLastCall) >= xDashTime){
            StopMoving();
            xDash = false;
            anim.SetBool("xDash", xDash);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && !xDash && TimeLeft(xDashLastCall) > xDashCallDown){
            Move(1, 5*xDashSpeed);
            xDash = true;
            anim.SetBool("xDash", xDash);
            xDashLastCall = DateTime.Now;

        }

            
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space) && !xDash && TimeLeft(xDashLastCall) > xDashCallDown){
            Move(-1, -5*xDashSpeed);
            xDash = true;
            anim.SetBool("xDash", xDash);
            xDashLastCall = DateTime.Now;
        }
        if (Input.GetKey(KeyCode.D) && !xDash){
            Move(1, 5*coefficient);
        }
        if (Input.GetKeyUp(KeyCode.D) && !xDash){
            StopMoving();
        }
        if (Input.GetKey(KeyCode.A) && !xDash){
            Move(-1, -5*coefficient);

        }
        if (Input.GetKeyUp(KeyCode.A) && !xDash){
            StopMoving();

        }
        
        
    }




}
