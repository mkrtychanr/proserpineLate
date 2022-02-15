using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{
    
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu)
        {
            Check();

            if (inDash())
            {

                //ускоренное движение вниз
                if (Input.GetKey(KeyCode.S))
                {
                    Fall();
                }


                //движение вправо
                if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space))
                {
                    Move(1);
                }


                //движение влево
                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Space))
                {
                    Move(-1);
                }


                //остановка движения
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
                {
                    StopMoving();
                }


                //переключение режима боя
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Fight();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();

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


                //прыжок
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Jump();
                }


                //вертикальный дэш с зажатым пробелом
                if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
                {
                    YDash();
                }


                //вертикальный дэш c нажатым пробелом
                if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.W))
                {
                    YDash();
                }


                //горизонтальный дэш вправо с зажатым пробелом
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
                {
                    XDash(1);
                }


                //горизонтальный дэш вправо с нажатым пробелом
                if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.D))
                {
                    XDash(1);
                }


                //горизонтальный дэш влево с зажатым пробелом
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
                {
                    XDash(-1);
                }


                //горизонтальный дэш влево с нажатым пробелом
                if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.A))
                {
                    XDash(-1);
                }

            }

        }

        Turn();
    }
}
