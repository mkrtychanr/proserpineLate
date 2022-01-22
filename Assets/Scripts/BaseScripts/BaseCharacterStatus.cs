using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStatus : MonoBehaviour
{

    public Dictionary<string, int> status = new Dictionary<string, int>()
    {

        //максимальное количество жизней в игре
        ["healthMax"] = 4,

        //кол-во жизней
        ["health"] = 4,

        //уровень оружия
        ["weaponLevel"] = 0,

        //выбранное оружие
        ["selectedWeapon"] = 0,

        ["direction"] = 1,

        ["wallCollusionDirection"] = 0

    };

    public Dictionary<string, bool> flags = new Dictionary<string, bool>()
    {

        //находимся ли персонаж в движении по Х
        ["isMoving"] = false,

        //находимся ли персонаж в бою
        ["onFight"] = false,

        //совершен ли прижок. Будет активно до того момента как персонаж не коснется земли
        ["inJump"] = false,

        //совершен ли второй прыжок
        ["inDoubleJump"] = false,

        //горизонтальный деш
        ["xDash"] = false,

        //вертикальный деш
        ["yDash"] = false,

        //находимся ли персонаж в состоянии падения
        ["isFalling"] = false,

        //находится ли персонаж в состоянии коллизии со стеной
        ["wall"] = false

    };

    
}
