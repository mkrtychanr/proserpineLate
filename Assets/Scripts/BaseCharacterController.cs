using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    [SerializeField] protected GameObject objSprite;
    [SerializeField] public bool isMoving = false;
    [SerializeField] public bool onFight = false;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sprite;

    protected void Turn()
    {
        Debug.Log(rb.velocity.x);
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    protected void Move(int direction)
    {
        isMoving = true;
        anim.SetBool("isMoving", isMoving);
        if (onFight)
        {
            rb.velocity = new Vector2(speed * direction, -12f);
        }
        else
        {
            rb.velocity = new Vector2(speed * direction, -12f);
        }
        Turn();
    }

    protected void StopMoving()
    {
        isMoving = false;
        anim.SetBool("isMoving", isMoving);
        rb.velocity = new Vector2(0, -12f);
    }

    protected void SetToFight()
    {
        onFight = true;
        anim.SetBool("onFight", onFight);
    }

    protected void SetToWalk()
    {
        onFight = false;
        anim.SetBool("onFight", onFight);
    }
}
