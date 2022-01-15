using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{

    void Start()
    {
        stats = GetComponent<HeroStatus>();
        rb = GetComponent<Rigidbody2D>();
        anim = objSprite.GetComponent<Animator>();
        sprite = objSprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //проверка на состояние падения
        onFallCheck();

        //стрельба
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();

        }

        //двойной прыжок. Персонаж должен быть в воздухе(должен быть уже один прыжок) и должна быть зажата клавиша W
        if (Input.GetKeyDown(KeyCode.W) && stats.inJump)
        {
            DoubleJump();

        }

        //обычный прыжок. Персонаж не должен быть в прыжке и должна быть нажата клавиша W
        if (Input.GetKeyDown(KeyCode.W) && !stats.inJump)
        {
            Up();

        }

        //движение вправо
        if (Input.GetKey(KeyCode.D))
        {
            Move(1);

        }


        //движение влево
        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);

        }

        //если клавишу отпустили происходит остановка движения
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            StopMoving();

        }

        //включение режима боя
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


        //выбор первого оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);

        }

        //выбор второго оружия
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);

        }

        //выбор третьего оружия
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);

        }

        //выбор четвертого оружия
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon(3);

        }

    }
}
