using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class person : MonoBehaviour
{
    public bool onStand;
    public bool isMoving;
    public bool onFight;
    public bool jumpStatus = false;
    public bool fall = false;
    public bool turn;
    private float speed = 4;
    public Animator anim;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
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
}
