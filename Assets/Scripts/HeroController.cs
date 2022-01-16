using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{

    private HeroSoundControl sound;

    //метод смены оружия
    protected void SelectWeapon(int weapon)
    {
        if (stats.weaponLevel <= 1)
        {
            if (weapon == 0)
            {
                stats.selectedWeapon = weapon;
                sound.changeAudioClip("weaponChange");
                Debug.Log("Weapon "+ weapon +" is selected");
            }

            if (weapon == 1 && stats.weaponLevel == 1)
            {
                weapon = 2;
                stats.selectedWeapon = weapon;
                sound.changeAudioClip("weaponChange");
                Debug.Log("Weapon "+ weapon +" is selected");
            }
        }
        else
        {
            weapon++;
            if (stats.weaponLevel >= weapon)
            {
                stats.selectedWeapon = weapon;
                sound.changeAudioClip("weaponChange");
                Debug.Log("Weapon "+ weapon +" is selected");
            }
        }
    }

    //метод увеличения уровня оружия
    protected void weaponLevelUp()
    {
        //увелиение уровня
        stats.weaponLevel++;

        //проверка случая когда выбранное орудие теперь недоступно (2 уровень оружия, пропадает нулевое орудие и вместо него появляется первое).
        weaponLevelCheck();
    }

    //проверка на невозможность управления оружием с текущим уровнем
    protected void weaponLevelCheck()
    {

        //если уровень оружия больше первого, то нулевое орудие становится более первым
        if (stats.weaponLevel > 1 && stats.selectedWeapon == 0) 
        {
            SelectWeapon(0);
        }

        //проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (stats.weaponLevel == 1 && stats.selectedWeapon != 0 && stats.selectedWeapon != 2)
        {
            SelectWeapon(0);

        }

        ////проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (stats.weaponLevel != 1 && stats.selectedWeapon > stats.weaponLevel)
        {
            SelectWeapon(0);
        }
        
    }

    void Start()
    {
        //инициализация компонентов Unity
        sound = GetComponent<HeroSoundControl>();
        stats = GetComponent<HeroStatus>();
        rb = GetComponent<Rigidbody2D>();
        anim = objSprite.GetComponent<Animator>();
        sprite = objSprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //проверка на состояние падения
        onFallCheck();

        //проверка на возможность ношения выбранного оружия
        weaponLevelCheck();

        //двойной прыжок. Персонаж должен быть в воздухе(должен быть уже один прыжок) и должна быть зажата клавиша W
        if (Input.GetKeyDown(KeyCode.W) && stats.inJump)
        {
            DoubleJump();
            sound.changeAudioClip("doubleJump");

        }

        //обычный прыжок. Персонаж не должен быть в прыжке и должна быть нажата клавиша W
        if (Input.GetKeyDown(KeyCode.W) && !stats.inJump)
        {
            Up();
            sound.changeAudioClip("jump");

        }

        //движение вправо
        if (Input.GetKey(KeyCode.D))
        {

            //придача ускорения
            Move(1);

            //если персонаж находится в воздухе, то необходимо запускается соотвествующий звук
            if (stats.inJump || stats.isFalling)
            {
                sound.changeAudioClip("moveInAir");

            } 

            else
            {

                //если игрок находится в режиме боя, то запускается звук бега
                if (stats.onFight)
                {
                    sound.changeAudioClip("run");

                }
                //иначе обычной ходьбы
                else
                {
                    sound.changeAudioClip("walk");

                }


            }


        }


        //движение влево
        if (Input.GetKey(KeyCode.A))
        {
            
            //придача ускорения
            Move(-1);

            //если персонаж находится в воздухе, то необходимо запускается соотвествующий звук
            if (stats.inJump || stats.isFalling)
            {
                sound.changeAudioClip("moveInAir");

            } 

            else
            {

                //если игрок находится в режиме боя, то запускается звук бега
                if (stats.onFight)
                {
                    sound.changeAudioClip("run");
                    
                }
                //иначе обычной ходьбы
                else
                {
                    sound.changeAudioClip("walk");

                }


            }
            

        }

        //остановка движения при расжатии клавиш движения
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            StopMoving();
            sound.changeAudioClip("stopMoving");

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
