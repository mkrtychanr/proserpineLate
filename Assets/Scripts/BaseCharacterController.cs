using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{

    //скорость передвижения вне боя
    [SerializeField] protected float walkSpeed = 10f;

    //скорость передвижения в бою
    [SerializeField] protected float fightSpeed = 20f;

    //скорость падения
    [SerializeField] protected float fallSpeed = -5f;

    //скорость передвижения в прыжке
    [SerializeField] protected float jumpXSpeed = 10f;

    //сила прыжка
    [SerializeField] protected float jumpYSpeed = 6f;

    [SerializeField] protected float doubleJumpYSpeed = 6f;

    //объявление компонентов Unity 
    [SerializeField] public Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected GameObject objSprite;
    [SerializeField] protected SpriteRenderer sprite;
    protected BaseCharacterStatus stats;

    //скорость при передвижении по Х
    private float xVelocity;

    //скорость при передвижении по Y
    private float yVelocity;

    //метод поворота персонажа
    protected void Turn()
    {

        //если персонаж двигается ВПРАВО, то его ускорение (rb.velocity.x) ПОЛОЖИТЕЛЬНО (>0)
        if (rb.velocity.x > 0)
        {

            //разворот спрайта
            sprite.flipX = false;

        }

        //если персонаж двигается ВЛЕВО, то его ускорение (rb.velocity.x) ОТРИЦАТЕЛЬНО (<0)
        else
        {
            //разворот спрайта
            sprite.flipX = true;
        }

    }

    //метод передвижения персонажа по X. в качестве применяемого параметра – направление. 1 – вправо. (-1) – влево
    protected void Move(int direction)
    {

        //меняем статус булиевых переменных. т.к. мы переходим в движение. Необходимо для анимаций и внутренней обработки кода
        stats.isMoving = true;
        anim.SetBool("isMoving", stats.isMoving);

        //если мы находимся в прыжке или падаем (земля из под ног ушла), то нам нужно изменить скорость передвижения
        if (stats.inJump || stats.isFalling)
        {
            
            //скорость по Х задаем сами выше
            xVelocity = jumpXSpeed;

            //а тут оставляем все на душе физики Unity
            yVelocity = rb.velocity.y;

        }
        else
        {

            //в зависимости от того в бою мы или нет – меняем скорость задаваемую при ходьбе
            if (stats.onFight)
            {
                xVelocity = fightSpeed;
                yVelocity = -12f;
            }
            else
            {
                xVelocity = walkSpeed;
                yVelocity = -5f;
            }

        }

        //передаем новые состояния скорости персонажу
        rb.velocity = new Vector2(xVelocity * direction, yVelocity);
        //производим поворот спрайта
        Turn();
    }

    protected void onFallCheck()
    {
        if (stats.inJump && rb.velocity.y < 0) stats.isFalling = true;
        anim.SetBool("isFalling", stats.isFalling);
    }

    protected void Up()
    {
        if (!stats.inJump)
        {
            Debug.Log("JustUp");
            stats.inJump = true;
            anim.SetBool("inJump", stats.inJump);
            rb.velocity = new Vector2(rb.velocity.x, jumpYSpeed);
        }
    }
    //метод двойного прыжка
    protected void DoubleJump()
    {   
        //если персонаж находится в прыжке
        if (stats.inJump && !stats.inDoubleJump)
        {
            Debug.Log("JustDoubleJump");
            stats.inDoubleJump = true;
            stats.isFalling = false;
            anim.SetBool("isFalling", stats.isFalling);
            anim.SetBool("inDoubleJump", stats.inDoubleJump);

            rb.velocity = new Vector2(rb.velocity.x, doubleJumpYSpeed);
        }
    }

    //метод остановки персонажа
    protected void StopMoving()
    {
         //меняем статус булиевых переменных. т.к. мы перестаем двигаться. Необходимо для анимаций и внутренней обработки кода
        stats.isMoving = false;
        anim.SetBool("isMoving", stats.isMoving);
        if (stats.inJump)
        {
            xVelocity = 0;
            yVelocity = rb.velocity.y;    
        }
        else
        {
            xVelocity = 0;
            yVelocity = -12f;
        }
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    //метод, который переводит персонажа в режим боя
    protected void SetToFight()
    {
        //меняем статус булиевых переменных и вызываем триггер. Необходимо для анимаций и внутренней обработки кода
        stats.onFight = true;
        anim.SetBool("onFight", stats.onFight);
        anim.SetTrigger("onFightTrigger");
    }

    //метод, который выводит персонажа из режима боя
    protected void SetToWalk()
    {
        //меняем статус булиевых переменных и вызываем триггер. Необходимо для анимаций и внутренней обработки кода
        stats.onFight = false;
        anim.SetBool("onFight", stats.onFight);
        anim.SetTrigger("onFightTrigger");
    }

    protected void Die()
    {
        anim.SetTrigger("die");
    }

    //метод стрельбы
    protected void Shoot()
    {
        if (stats.onFight)
        {
            Debug.Log("Выстрел");
        }
    }
    //метод смены оружия
    protected void SelectWeapon(int weapon)
    {
        if (weapon <= stats.weaponLevel)
        {
            stats.selectedWeapon = weapon;
            Debug.Log("Weapon "+ weapon +" is selected");
        }
    }

}
