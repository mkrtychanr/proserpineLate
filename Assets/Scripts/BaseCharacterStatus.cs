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
    [SerializeField] public int weaponLevel;

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
}
