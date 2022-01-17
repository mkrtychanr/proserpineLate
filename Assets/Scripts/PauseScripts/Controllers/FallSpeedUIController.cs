using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpeedUIController : BaseUIController
{
    void Start()
    {

        //поле для словаря
        field = "fallSpeed";

        //инициализация значения по-умолчанию
        inDefault = stats.parameters[field];

        //инициализация актуального значения
        current = inDefault;

        //запись актуального значения в UI элемент
        panel.text = current.ToString();

    }
}
