using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseCharacterController
{

    private HeroSoundControl sound;
    [SerializeField] private PauseMenu pause;

    //метод смены оружия. На вход поступает переменная от 0 до 3. И в зависимости от уровня оружия происходит особая обработка аргумента
    //в том случае, если уровень оружия равен 0, то и первое оружие в панели будет иметь номер 0
    //в том случае, если уровень оружия равен 1, то первое оружие в панели имеет номер 0, а второе – 2
    //в том случае, если уровень оружия равен 2, то первое оружие в пенели уже будет иметь номер 1, второе – 2
    //дальше идет в обычном порядке, на третьем уровне орудия 1 и 2 будут иметь такой же номер, как и на втором, а третье орудие номер 3
    //в четвером так-же. Такая необычная механика выходит из ГДД.
    protected void SelectWeapon(int weapon)
    {

        //если уровень оружия меньше 2
        if (stats.status["weaponLevel"] < 2)
        {

            //если аргумент равен нулю.
            if (weapon == 0)
            {

                //установка вызванного оружия
                stats.status["selectedWeapon"] = weapon;

                //вызов звука
                sound.changeAudioClip("weaponChange");

            }
            
            //если аргумент равен единице и уровень равен единице
            if (weapon == 1 && stats.status["weaponLevel"] == 1)
            {
                
                //присваивание аргументу новое значение
                weapon = 2;

                //установка вызванного оружия
                stats.status["selectedWeapon"] = weapon;

                //вызов звука
                sound.changeAudioClip("weaponChange");

            }
        }
        else
        {

            //увеличиваем значение аргумента, так как после первого уровня оружия номер зажатой кнопки соответствует вызываемому оружию
            weapon++;
            if (stats.status["weaponLevel"] >= weapon)
            {

                //установка вызванного оружия
                stats.status["selectedWeapon"] = weapon;

                //вызов звука
                sound.changeAudioClip("weaponChange");
            }
        }
    }

    //метод увеличения уровня оружия
    protected void weaponLevelUp()
    {
        //увелиение уровня
        stats.status["weaponLevel"]++;

        //проверка случая когда выбранное орудие теперь недоступно (2 уровень оружия, пропадает нулевое орудие и вместо него появляется первое).
        weaponLevelCheck();
    }

    //проверка на невозможность управления оружием с текущим уровнем
    protected void weaponLevelCheck()
    {

        //если уровень оружия больше первого, то нулевое орудие становится более первым
        if (stats.status["weaponLevel"] > 1 && stats.status["selectedWeapon"] == 0) 
        {
            SelectWeapon(0);
        }

        //проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (stats.status["weaponLevel"] == 1 && stats.status["selectedWeapon"] != 0 && stats.status["selectedWeapon"] != 2)
        {
            SelectWeapon(0);

        }

        ////проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (stats.status["weaponLevel"] != 1 && stats.status["selectedWeapon"] > stats.status["weaponLevel"])
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
        if(!pause.isPaused)
        {

            //проверка на состояние падения
            onFallCheck();

            //проверка на возможность ношения выбранного оружия
            weaponLevelCheck();

            //двойной прыжок. Персонаж должен быть в воздухе(должен быть уже один прыжок) и должна быть зажата клавиша W
            if (Input.GetKeyDown(KeyCode.W) && stats.flags["inJump"])
            {
                DoubleJump();
                sound.changeAudioClip("doubleJump");

            }

            //обычный прыжок. Персонаж не должен быть в прыжке и должна быть нажата клавиша W
            if (Input.GetKeyDown(KeyCode.W) && !stats.flags["inJump"])
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
                if (stats.flags["inJump"] || stats.flags["isFalling"])
                {
                    sound.changeAudioClip("moveInAir");

                } 

                else
                {

                    //если игрок находится в режиме боя, то запускается звук бега
                    if (stats.flags["onFight"])
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
                if (stats.flags["inJump"] || stats.flags["isFalling"])
                {
                    sound.changeAudioClip("moveInAir");

                } 

                else
                {

                    //если игрок находится в режиме боя, то запускается звук бега
                    if (stats.flags["onFight"])
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
                if (stats.flags["onFight"])
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
}
