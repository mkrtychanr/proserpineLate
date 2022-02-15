using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCameraController : MonoBehaviour
{
    //игровой объект на котором лофит фокус камера
    private GameObject focusObject;

    //скорость возврата камеры. Чем больше тем медленее камера будет возвращаться в центр
    private float camSpeed = 70f;

    //расстояние на котором намера не фиксирует движения персонажа. Слепая зона
    private float rangeDistance = 5f;

    //высота расположения камеры вне режима боя
    private float camYOnWalk = -2.1f;

    //дальность расположения камеры вне режима боя
    private float camZOnWalk = -13f;

    //высота расположения камеры в режиме боя
    private float camYOnFight = -0.7f;

    //дальность расположения камеры в режиме боя
    private float camZOnFight = -19.4f;

    //private float camZOnCombat = -6f;

    //private float camYOnCombat = -3.33f;

    //объявление компонентов Unity
    private Camera cam;
    private Transform body;
    private Rigidbody2D rb;
    private BaseCharacterFlags flags;

    //внутреняя переменная, которая помогает "доезжать" камере красиво 
    private float delta;
   
    
    private void Start() 
    {
        //инициализация компонентов Unity
        focusObject = GameObject.Find("Hero");
        cam = GetComponent<Camera>();
        body = focusObject.GetComponent<Transform>();
        rb = focusObject.GetComponent<Rigidbody2D>();
        flags = focusObject.GetComponent<BaseCharacterFlags>();
    }
    private void Update() 
    {

        
        //ДВИЖЕНИЕ КАМЕРЫ ЗА ПЕРСОНАЖЕМ
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //если положение камеры (transform.position.x) БОЛЬШЕ положение персонажа (body.position.x), 
        //то персонаж либо двигается влево, либо сдвинулся влево(но остался в слепой зоне rangeDistance)
        //если разница между положением камеры и персонажем БОЛЬШЕ слепой зоны, то выполняется сл.блок
        if (transform.position.x > body.position.x && transform.position.x - body.position.x > rangeDistance)
        {

            //положение камеры по Х будет задаваться так, чтоб персонаж продолжал оставаться на границе со слепой зоной
            //body.position.x + rangeDistance – новое значение камеры по X. Камера держится на расстоянии равном слепой зоне rangeDistance
            transform.position = new Vector3(body.position.x+rangeDistance, transform.position.y, transform.position.z);

        }
        
        //если положение камеры (transform.position.x) МЕНЬШЕ положение персонажа (body.position.x),
        //то персонаж двигается вправо, либо сдвинулся вправо(но остался в слепой зоне rangeDistance)
        //если разница между положением камеры и персонажем БОЛЬШЕ слепой зоны, то выполняется сл.блок
        if (transform.position.x < body.position.x && body.position.x - transform.position.x > rangeDistance)
        {

            //положение камеры по Х будет задаваться так, чтоб персонаж продолжал оставаться на границе со слепой зоной
            //body.position.x - rangeDistance – новое значение камеры по Х. Камера держится на расстоянии равным слепой зоне rangeDistance
            transform.position = new Vector3(body.position.x-rangeDistance, transform.position.y, transform.position.z);

        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        if (!flags.isMoving)
        {

            //ДОЕЗЖАНИЕ КАМЕРЫ ДО ПЕРСОНАЖА
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


            //если персонаж стоит на месте (!stats.flags["isMoving"]) и положение камеры (transform.position) МЕНЬШЕ положения игрока,
            //то игрок находится СПРАВА от камеры (в слепой зоне), следовательно будет выполняться сл.блок.
            if (transform.position.x < body.position.x)
            {

                //delta – расстояние между персонажем и камерой. Делим на camSpeed, чтоб разизовать "доезжание"
                delta = (body.position.x - transform.position.x)/camSpeed;
                transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);

            }

            //если персонаж стоит на месте (!stats.flags["isMoving"]) и положение камеры (transform.position) МЕНЬШЕ положения игрока,
            //то игрок находится СПРАВА от камеры (в слепой зоне), следовательно будет выполняться сл.блок.
            if (transform.position.x > body.position.x)
            {

                //delta – расстояние между персонажем и камерой. Делим на camSpeed, чтоб разизовать "доезжание"
                delta = (transform.position.x - body.position.x)/camSpeed;
                transform.position = new Vector3(transform.position.x - delta, transform.position.y, transform.position.z);
            }
        }
        


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //ИЗМЕНЕНИЕ ХАРАКТЕРИСИК КАМЕРЫ
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //если персонаж находится В БОЮ, то ему нужно изменить угол обзора камеры и установить ее выше, нежели чем ВНЕ БОЯ.
        if (flags.onFight)
        {

            //установка новой позиции по Y
            transform.position = new Vector3 (transform.position.x, camYOnFight, camZOnFight);

            //изменение угла обзора
            //cam.fieldOfView = fightFieldOfView;

        } 
        //если персонаж находится ВНЕ БОЯ, то ему нужно изменить угол обзора камеры и установить ее выше, нежели чем В БОЯ.
        else 
        {

            //установка новой позиции по Y
            transform.position = new Vector3 (transform.position.x, camYOnWalk, camZOnWalk);

            //изменение угла обзора
            //cam.fieldOfView = walkFieldOfView;

        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    
    }
}
