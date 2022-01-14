using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = objSprite.GetComponent<Animator>();
        sprite = objSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onFallCheck();
        if (Input.GetKey(KeyCode.W))
        {
            Up();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(1);
        }

        if (Input.GetKeyUp(KeyCode.D)){
            StopMoving();
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMoving();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (onFight)
            {
                SetToWalk();
            }
            else
            {
                SetToFight();
            }

        }

    }
}
