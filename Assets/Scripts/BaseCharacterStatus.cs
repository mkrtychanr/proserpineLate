using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStatus : MonoBehaviour
{

    //максимальное количество жизней в игре
    [SerializeField] public int healthMax;

    //максимальное количество жизней на данных момент игры
    [SerializeField] public int healthCurrent;
    
    //жизни персонажа
    [SerializeField] public int health;

    //уровень оружия
    [SerializeField] public int weaponLevel = 0;

    //выбранное оружие
    [SerializeField] public int selectedWeapon = 0;

    //находимся ли мы в движении по Х
    [SerializeField] public bool isMoving = false;

    //находимся ли мы в бою
    [SerializeField] public bool onFight = false;

    //совершен ли прижок. Будет активно до того момента как персонаж не коснется земли
    [SerializeField] public bool inJump = false;

    //совершен ли второй прыжок
    [SerializeField] public bool inDoubleJump = false;

    //горизонтальный дэш
    [SerializeField] public bool xDash = false;

    //вертикальный деш
    [SerializeField] public bool yDash = false;

    //находимся ли мы в состоянии падения
    [SerializeField] public bool isFalling = false;



    
}
