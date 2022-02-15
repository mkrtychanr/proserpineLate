using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{

    //метод поворота спрайта персонажа
    protected void Turn()
    {

        //если персонаж находится в режиме боя
        if (flags.onFight)
        {

            //если соперник находится справа от персонажа
            if (AttentionObject.transform.position.x > transform.position.x)
            {
                sprite.flipX = false;
                leftCollider.SetActive(false);
                rightCollider.SetActive(true);
                status.eyesDirection = 1;
            }

            //если соперник находится слева от персонажа
            else
            {
                sprite.flipX = true;
                leftCollider.SetActive(true);
                rightCollider.SetActive(false);
                status.eyesDirection = -1;
            }

        }
        //если персонаж не находится в режиме боя
        else
        {

            //если ускорение больше нуля, то персонаж двигается вправо
            if (rb.velocity.x > 0)
            {
                sprite.flipX = false;
                leftCollider.SetActive(false);
                rightCollider.SetActive(true);
                status.eyesDirection = 1;
            }

            //если ускорение меньше нуля, то персонаж двигается влево
            else if (rb.velocity.x < 0)
            {
                sprite.flipX = true;
                leftCollider.SetActive(true);
                rightCollider.SetActive(false);
                status.eyesDirection = -1;
            }

        }
    }


    //метод передвижения(и на земле и в воздухе)
    protected void Move(int direction)
    {

        //если стена находится в направлении, противоположном тому, в котором персонаж пытается совершить движение
        if (status.wallCollisionDirection != direction)
        {

            //если есть коллиция с землей и песонаж не находится прыжка или падения (в общем, просто на земле)
            if (collisions.ground && !flags.inJump && !flags.isFalling)
            {

                //если персонаж в режиме боя
                if (flags.onFight)
                {
                    xVelocity = parameters.fightSpeed;
                    yVelocity = -12f;
                }
                //если персонаж не в режиме боя
                else
                {
                    xVelocity = parameters.walkSpeed;
                    yVelocity = -5f;
                }

            }
            //если персонаж находится в воздухе
            else
            {
                xVelocity = parameters.moveInAirSpeed;
                yVelocity = rb.velocity.y;
            }

            //поворот спрайте
            Turn();

            //обновление флага движения
            flags.isMoving = true;

            //обновление флага движения движения в контроллере анимаций
            animator.SetBool("isMoving", flags.isMoving);

            //обновление направления движения
            status.direction = direction;

            //обновление скорости персонажа
            rb.velocity = new Vector2(xVelocity * direction, yVelocity);

        }
        //если персонаж пытается совершить движение в стену
        else
        {
            StopMoving();
        }
    }

    //метод остановки движения
    protected void StopMoving()
    {
        
        //обновление флага движения
        flags.isMoving = false;

        //обновление флага движения в контроллере анимаций
        animator.SetBool("isMoving", flags.isMoving);

        //если персонаж находится на земле
        if (collisions.ground)
        {
            yVelocity = 0f;
        }
        //в воздухе
        else
        {
            yVelocity = rb.velocity.y;
        }

        //обновление скорости персонажа
        rb.velocity = new Vector2(0, yVelocity);

    }


    //метод прыжка
    protected void Jump()
    {

        //если персонаж не прыгал с момента последней коллизии с землей, не делал дэш и не находится в состоянии падения (в общем, находится на земле)
        if (!flags.inJump && !flags.yDash && !flags.isFalling)
        {

            //так как мы прикладываем импульс по Y, то нам надо сначала обнулить скорость по Y
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            //приложение импульса по Y
            rb.AddForce(transform.up * parameters.jumpForce, ForceMode2D.Impulse);

            //обновление флага прыжка
            flags.inJump = true;

            //обновление флага прыжка в контроллере анимаций
            animator.SetBool("inJump", flags.inJump);

        }

        //если персонаж не прыгал, но находится в состоянии падения
        if (flags.isFalling && !flags.inJump)
        {
            
            //так как мы прикладываем импульс по Y, то нам надо сначала обнулить скорость по Y
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            //приложение импульса по Y
            rb.AddForce(transform.up * parameters.jumpForce, ForceMode2D.Impulse);

            //обновление флага прыжка
            flags.inJump = true;

            //обновление флага прыжка в контроллере анимаций
            animator.SetBool("inJump", flags.inJump);

            //вызов триггера прыжка в контроллере анимаций
            animator.SetTrigger("JumpInFallTrigger");

        }

    }

    protected void Fall()
    {
        if (!collisions.ground)
        {
            rb.velocity = new Vector2(rb.velocity.x, parameters.forcedFallSpeed);
        }
    }
}
