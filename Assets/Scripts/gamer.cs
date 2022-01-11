using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamer : person
{
    public gamer Hero;
    public gamer(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

}
