using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<HeroStatus>();
        rb = GetComponent<Rigidbody2D>();
        anim = objSprite.GetComponent<Animator>();
        sprite = objSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onFallCheck();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.W) && stats.inJump)
        {
            DoubleJump();
        }
        if (Input.GetKeyDown(KeyCode.W) && !stats.inJump)
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
            if (stats.onFight)
            {
                SetToWalk();
            }
            else
            {
                SetToFight();
            }

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

    }
}
