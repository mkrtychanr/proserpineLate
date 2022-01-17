using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIStatus : BaseUIStatus
{

    void Start()
    {

        //поле для словаря
        field = "health";

        //инициализация значения по-умолчанию
        inDefault = stats.status[field];

        //инициализация актуального значения
        current = inDefault;

        //запись актуального значения в UI элемент
        panel.text = current.ToString();

    }

}
