using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{

    //проверка на коллизию с землей
    protected void CollisionWithGroundCheck()
    {
        //если коллизия изменила свое положение (от true до false)
        if (lastCollisions.ground != collisions.ground)
        {
            //если персонаж упал на землю надо обнулить соответсвующие флаги
            if (collisions.ground)
            {
                
                flags.isFalling = false;
                flags.inJump = false;
                flags.xDash = false;
                flags.yDash = false;
                rb.velocity = new Vector2(rb.velocity.x, 0f);

                //и закинуть их в аниматор
                animator.SetBool("isFalling", flags.isFalling);
                animator.SetBool("inJump", flags.inJump);
                
                //SetBoolToAnimator("yDash");
                //SetBoolToAnimator("xDash");

            }
            //else нет, так как вся последующая обработка реализована непосредствована в других методах класса

            //обновление коллизии
            lastCollisions.ground = collisions.ground;

        }        

    }

    //проверка на коллизию со стеной (чтоб персонаж не ходил уперевшись в стену)
    protected void CollisionWithWallCheck()
    {

        //если коллизия изменила свое положение (от true до false)
        if (lastCollisions.wall != collisions.wall)
        {

            //персонаж уперся в стену
            if (collisions.wall)
            {

                //обновление значения для статуса коллизии со стеной, в будующем, попытка движения в этом направлении будет проигнорирована
                status.wallCollisionDirection = status.direction;

            }
            //персонаж отошел от стены
            else
            {
                //коллизии со стеной больше нет – обновляение значения на 0 (ноль в status.Get("direction") получить невозможно)
                status.wallCollisionDirection = 0;

            }

            //обновление коллизии
            lastCollisions.wall = collisions.wall;

        }

    }

    //проверка на состояние падения
    protected void FallingCheck()
    {

        //если нет коллизии (персонаж) не на земле и не находится в состоянии падения, но при этом скорость по Y < 0 – персонаж падает
        if (!collisions.ground && !flags.isFalling && rb.velocity.y < 0)
        {

            //добавление (а не изменение) скорости персонажу, чтоб падал быстрее
            rb.AddForce(transform.up * parameters.fallSpeed, ForceMode2D.Impulse);

            //обновление флага падения
            flags.isFalling = true;

            //обновление флага падения в контроллере анимаций
            animator.SetBool("isFalling", flags.isFalling);


        } 

    }

    protected void AttentionObjectCheck()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        int min = 0;
        if (objects.Length != 0)
        {
            
            for (int i = 1; i < objects.Length; i++)
            {
                if (GetDistanceToObj(objects[min]) > GetDistanceToObj(objects[i]))
                {
                    min = i;
                }
            }
            
            AttentionObject = objects[min];
        }
    }

    //метод вызова всех проверок
    protected void Check()
    {
        FallingCheck();
        weaponLevelCheck();
        AttentionObjectCheck();
        CollisionWithWallCheck();
        CollisionWithGroundCheck();
    }
}
