using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{

    public Dictionary<string, float> parameters = new Dictionary<string, float>()
    {

        //скорость ходьбы
        ["walkSpeed"] = 10f,

        //скорость бега
        ["fightSpeed"] = 20f,

        //скорость падения
        ["fallSpeed"] = -5f,

        //скорость движения в воздухе
        ["jumpXSpeed"] = 10f,

        //сила прыжка
        ["jumpYSpeed"] = 6f,

        //сила двойного прыжка
        ["doubleJumpYSpeed"] = 6f
    };

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
        stats.flags["isMoving"] = true;
        anim.SetBool("isMoving", stats.flags["isMoving"]);

        //если мы находимся в прыжке или падаем (земля из под ног ушла), то нам нужно изменить скорость передвижения
        if (stats.flags["inJump"] || stats.flags["isFalling"])
        {
            
            //скорость по Х задаем сами выше
            xVelocity = parameters["jumpXSpeed"];

            //а тут оставляем все на душе физики Unity
            yVelocity = rb.velocity.y;

        }
        else
        {

            //в зависимости от того в бою мы или нет – меняем скорость задаваемую при ходьбе
            if (stats.flags["onFight"])
            {
                xVelocity = parameters["fightSpeed"];
                yVelocity = -12f;
            }
            else
            {
                xVelocity = parameters["walkSpeed"];
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
        if (stats.flags["inJump"] && rb.velocity.y < 0) stats.flags["isFalling"] = true;
        anim.SetBool("isFalling", stats.flags["isFalling"]);
    }

    protected void Up()
    {
        if (!stats.flags["inJump"])
        {
            Debug.Log("JustUp");
            stats.flags["inJump"] = true;
            anim.SetBool("inJump", stats.flags["inJump"]);
            rb.velocity = new Vector2(rb.velocity.x, parameters["jumpYSpeed"]);
        }
    }
    //метод двойного прыжка
    protected void DoubleJump()
    {   
        //если персонаж находится в прыжке
        if (stats.flags["inJump"] && !stats.flags["inDoubleJump"])
        {
            Debug.Log("JustDoubleJump");
            stats.flags["inDoubleJump"] = true;
            stats.flags["isFalling"] = false;
            anim.SetBool("isFalling", stats.flags["isFalling"]);
            anim.SetBool("inDoubleJump", stats.flags["inDoubleJump"]);

            rb.velocity = new Vector2(rb.velocity.x, parameters["doubleJumpYSpeed"]);
        }
    }

    //метод остановки персонажа
    protected void StopMoving()
    {
         //меняем статус булиевых переменных. т.к. мы перестаем двигаться. Необходимо для анимаций и внутренней обработки кода
        stats.flags["isMoving"] = false;
        anim.SetBool("isMoving", stats.flags["isMoving"]);
        if (stats.flags["inJump"])
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
        stats.flags["onFight"] = true;
        anim.SetBool("onFight", stats.flags["onFight"]);
        anim.SetTrigger("onFightTrigger");
    }

    //метод, который выводит персонажа из режима боя
    protected void SetToWalk()
    {
        //меняем статус булиевых переменных и вызываем триггер. Необходимо для анимаций и внутренней обработки кода
        stats.flags["onFight"] = false;
        anim.SetBool("onFight", stats.flags["onFight"]);
        anim.SetTrigger("onFightTrigger");
    }

    protected void Die()
    {
        anim.SetTrigger("die");
    }

    //метод стрельбы
    protected void Shoot()
    {
        if (stats.flags["onFight"])
        {
            Debug.Log("Выстрел");
        }
    }

}
