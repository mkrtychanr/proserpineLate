using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    //игровой объект на котором лофит фокус камера
    [SerializeField] private GameObject hero;

    //скорость возврата камеры. Чем больше тем медленее камера будет возвращаться в центр
    [SerializeField] private float camSpeed = 70f;

    //расстояние на котором намера не фиксирует движения персонажа. Слепая зона
    [SerializeField] private float rangeDistance = 5f;

    //угол обзора камеры вне режима боя
    [SerializeField] private float walkFieldOfView = 50;

    //высота расположения камеры вне режима боя
    [SerializeField] private float camHeightOnWalk = -2.4f;

    //угол обзора камеры в режиме боя
    [SerializeField] private float fightFieldOfView = 100f;

    //высота расположения камеры в режиме боя
    [SerializeField] private float camHeightOnFight = 4;

    //объявление компонентов Unity
    private Camera cam;
    private Transform body;
    private Rigidbody2D rb;
    private HeroController hc;

    //внутреняя переменная, которая помогает "доезжать" камере красиво 
    private float delta;
   
    
    private void Start() 
    {
        //инициализация компонентов Unity
        cam = GetComponent<Camera>();
        body = hero.GetComponent<Transform>();
        rb = hero.GetComponent<Rigidbody2D>();
        hc = hero.GetComponent<HeroController>();
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




        //ДОЕЗЖАНИЕ КАМЕРЫ ДО ПЕРСОНАЖА
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //если персонаж стоит на месте (!hc.isMoving) и положение камеры (transform.position) МЕНЬШЕ положения игрока,
        //то игрок находится СПРАВА от камеры (в слепой зоне), следовательно будет выполняться сл.блок.
        if (!hc.isMoving && transform.position.x < body.position.x)
        {

            //delta – расстояние между персонажем и камерой. Делим на camSpeed, чтоб разизовать "доезжание"
            delta = (body.position.x - transform.position.x)/camSpeed;
            transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);

        }

        //если персонаж стоит на месте (!hc.isMoving) и положение камеры (transform.position) МЕНЬШЕ положения игрока,
        //то игрок находится СПРАВА от камеры (в слепой зоне), следовательно будет выполняться сл.блок.
        if (!hc.isMoving && transform.position.x > body.position.x)
        {

            //delta – расстояние между персонажем и камерой. Делим на camSpeed, чтоб разизовать "доезжание"
            delta = (transform.position.x - body.position.x)/camSpeed;
            transform.position = new Vector3(transform.position.x - delta, transform.position.y, transform.position.z);
        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //ИЗМЕНЕНИЕ ХАРАКТЕРИСИК КАМЕРЫ
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //если персонаж находится В БОЮ, то ему нужно изменить угол обзора камеры и установить ее выше, нежели чем ВНЕ БОЯ.
        if (hc.onFight)
        {

            //установка новой позиции по Y
            transform.position = new Vector3 (transform.position.x, camHeightOnFight, transform.position.z);

            //изменение угла обзора
            cam.fieldOfView = fightFieldOfView;

        } 
        //если персонаж находится ВНЕ БОЮ, то ему нужно изменить угол обзора камеры и установить ее выше, нежели чем В БОЯ.
        else 
        {

            //установка новой позиции по Y
            transform.position = new Vector3 (transform.position.x, camHeightOnWalk, transform.position.z);

            //изменение угла обзора
            cam.fieldOfView = walkFieldOfView;

        }


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
