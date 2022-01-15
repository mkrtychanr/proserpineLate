using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{

    //находимся ли мы в движении по Х
    [SerializeField] public bool isMoving = false;

    //находимся ли мы в бою
    [SerializeField] public bool onFight = false;

    //совершен ли прижок. Будет активно до того момента как персонаж не коснется земли
    [SerializeField] public bool inJump = false;

    //совершен ли второй прыжок
    [SerializeField] public bool inDoubleJump = false;

    //находимся ли мы в состоянии падения
    [SerializeField] public bool isFalling = false;

    //уроень оружия игрока
    [SerializeField] public int weaponLevel = 0;

    //выбранное оружие
    [SerializeField] public int selectedWeapon = 0;

    //[SerializeField] public int wKeyIsDown

    //скорость передвижения вне боя
    [SerializeField] protected float walkSpeed = 10f;

    //скорость передвижения в бою
    [SerializeField] protected float fightSpeed = 20f;

    //скорость падения
    [SerializeField] protected float fallSpeed = -5f;

    //скорость передвижения в прыжке
    [SerializeField] protected float jumpXSpeed = 15f;

    //сила прыжка
    [SerializeField] protected float jumpYSpeed = 5f;

    [SerializeField] protected float doubleJumpYSpeed = 3f;

    //объявление компонентов Unity 
    [SerializeField] public Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected GameObject objSprite;
    [SerializeField] protected SpriteRenderer sprite;

    //скорость при передвижении по Х
    private float xVelocity;

    //скорость при передвижении по Y
    private float yVelocity;

    //метод поворота персонажа
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

    //метод передвижения персонажа по X. в качестве применяемого параметра – направление. 1 – вправо. (-1) – влево
    protected void Move(int direction)
    {

        //меняем статус булиевых переменных. т.к. мы переходим в движение. Необходимо для анимаций и внутренней обработки кода
        isMoving = true;
        anim.SetBool("isMoving", isMoving);

        if (inJump || isFalling)
        {
            Debug.Log("Двигаемся в воздухе");
            xVelocity = jumpXSpeed;
            yVelocity = rb.velocity.y;
        }
        else
        {
            //в зависимости от того в бою мы или нет – меняем скорость задаваемую при ходьбе
            if (onFight)
            {
                xVelocity = fightSpeed;
                yVelocity = -12f;
            }
            else
            {
                xVelocity = walkSpeed;
                yVelocity = -5f;
            }
            Debug.Log("А ты думал?");

        }

        //передаем новые состояния скорости персонажу
        rb.velocity = new Vector2(xVelocity * direction, yVelocity);
        //производим поворот спрайта
        Turn();
    }

    protected void onFallCheck()
    {
        if (inJump && rb.velocity.y < 0) isFalling = true;
        anim.SetBool("isFalling", isFalling);
    }

    protected void Up()
    {
        if (!inJump)
        {
            Debug.Log("JustUp");
            inJump = true;
            anim.SetBool("inJump", inJump);
            rb.velocity = new Vector2(rb.velocity.x, jumpYSpeed);
        }
    }

    protected void DoubleJump()
    {
        if (inJump && !inDoubleJump)
        {
            Debug.Log("JustDoubleJump");
            inDoubleJump = true;
            isFalling = false;
            anim.SetBool("isFalling", isFalling);
            anim.SetBool("inDoubleJump", inDoubleJump);

            rb.velocity = new Vector2(rb.velocity.x, doubleJumpYSpeed);
        }
    }

    //метод остановки персонажа
    protected void StopMoving()
    {
         //меняем статус булиевых переменных. т.к. мы перестаем двигаться. Необходимо для анимаций и внутренней обработки кода
        isMoving = false;
        anim.SetBool("isMoving", isMoving);
        if (inJump)
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
        onFight = true;
        anim.SetBool("onFight", onFight);
        anim.SetTrigger("onFightTrigger");
    }

    //метод, который выводит персонажа из режима боя
    protected void SetToWalk()
    {
        //меняем статус булиевых переменных и вызываем триггер. Необходимо для анимаций и внутренней обработки кода
        onFight = false;
        anim.SetBool("onFight", onFight);
        anim.SetTrigger("onFightTrigger");
    }

    protected void Die()
    {
        anim.SetTrigger("die");
    }

}
