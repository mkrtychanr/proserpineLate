using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevelUIStatus : BaseUIStatus
{

    void Start()
    {

        //поле для словаря
        field = "weaponLevel";

        //инициализация значения по-умолчанию
        inDefault = stats.status[field];

        //инициализация актуального значения
        current = inDefault;

        //запись актуального значения в UI элемент
        panel.text = current.ToString();

    }
    
}
