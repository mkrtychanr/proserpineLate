using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{


    //метод, который вводит персонажа в режим боя
    protected void SetToFight()
    {
        //обновление флага боя
        flags.onFight = true;

        //обновление флага боя в контроллере анимаций
        animator.SetBool("onFight", flags.onFight);

        //вызов триггера в контроллере анимаций
        animator.SetTrigger("onFightTrigger");

        //поворот спрайта персонажа (если мы находимся в бою, то персонаж всегда стоит лицом к противнику)
        Turn();

    }


    //метод, который выводит персонажа из режима боя
    protected void SetToWalk()
    {

        //обновление флага боя
        flags.onFight = false;

        //обновление флага боя в контроллере анимаций
        animator.SetBool("onFight", flags.onFight);

        //вызов триггера в контроллере анимаций
        animator.SetTrigger("onFightTrigger");

    }


    //метод удобного переключения между режимами боя
    protected void Fight()
    {
        if (flags.onFight)
        {
            SetToWalk();
        }
        else
        {
            SetToFight();
        }
    }

}
