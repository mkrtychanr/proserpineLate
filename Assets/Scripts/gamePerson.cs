using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePerson : MonoBehaviour
{   
    private bool onStand;
    private bool isMoving;
    public bool onFight;
    private bool jumpStatus = false;
    public bool fall = false;
    public bool turn;
    // регистры состояний 
    public DateTime lastCall;
    private float speed = 4;
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    //public GameObject obj;
    public void Turn(int direction){
        if (direction > 0) turn = false;
        else turn = true;
        sprite.flipX = turn;
    }
    public void Move(int direction, float acceleration){
        Turn(direction);
        isMoving = true;
        anim.SetBool("isMoving", isMoving);
        rb.velocity = new Vector2 (speed * acceleration, -12f);
    }
    public void StopMoving(){
        isMoving = false;
        anim.SetBool("isMoving", isMoving);
        rb.velocity = new Vector2 (0, 0);
    }
    public double TimeLeft(DateTime lastCall){
        return (DateTime.Now - lastCall).TotalSeconds;
    }
}
    
