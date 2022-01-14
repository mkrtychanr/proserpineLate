using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroController))]

public class HeroCollisionHandler : MonoBehaviour
{
    private HeroController hero;

    void Start()
    {
        hero = GetComponent<HeroController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Столкновение");
        if (collision.gameObject.tag == "Floor")
        {
            hero.isFalling = false;
            hero.inAir = false;
            hero.isJump = false;
            hero.anim.SetBool("isFalling", hero.isFalling);
            hero.anim.SetBool("inAir", hero.inAir);
        }
    }
}
