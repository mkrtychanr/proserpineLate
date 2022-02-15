using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{

    protected bool inDash()
    {
        return !flags.inYDash && !flags.inXDash;
    }
    

    //метод вертикального дэша
    protected void YDash()
    {
        
        //если персонаж не делал дэш с момента последней коллизии с землей и уже разблокировал мехаинку вертикального дэша
        if (!flags.yDash && flags.yDashActive)
        {

            //обновление флага, показывающего, вызывался ли дэш с момента последней коллизии с землей
            flags.yDash = true;

            //обновление флага, показывающего, исполняется ли дэш сейчас или нет (в активном своем состоянии он не дает манипулировать персонажем)
            flags.inYDash = true;

            //обновление флага вертикального дэша в контроллере анимаций 
            animator.SetBool("inYDash", flags.inYDash);

            //вызов триггера вертикаольного дэша в контроллере анимаций
            animator.SetTrigger("yDashTrigger");

            //запуск корутины дэша
            StartCoroutine(yDashCoroutine());

        }

    }


    //метод горизонтального дэша
    protected void XDash(int direction)
    {
        //если персонаж не делал дэш с момента последней коллизии с землей, разблокировал механику и не пытается дэшнуться в стену
        if (!flags.xDash && flags.xDashActive && status.wallCollisionDirection != direction)
        {

            //обновление флага, показывающего, вызывался ли дэш с момента последней коллизии с землей
            flags.xDash = true;

            //обновление флага, показывающего, исполняется ли дэш сейчас или нет (в активном своем состоянии он не дает манипулировать персонажем)
            flags.inXDash = true;

            //обновление флага горизонтального дэша в контроллере анимаций
            animator.SetBool("inXDash", flags.inXDash);

            //вызов триггера горизонтального дэша в контроллере анимаций
            animator.SetTrigger("xDashTrigger");

            //обновление статуса направления персонажа
            status.direction = direction;

            //запуск корутины дэша
            StartCoroutine(xDashCoroutine(direction));

        }
        //если персонаж пытается дэшнуться в стену
        else if (status.wallCollisionDirection == direction)
        {
            StopMoving();

        }
    }


    //корутина горизонтального дэша
    IEnumerator xDashCoroutine(int direction)
    {

        //выключение heatBox персонажа, так как во время дэша персонаж неуязвим
        heatBox.SetActive(false);

        //обнуление скорости по Y(во время горизонтального дэша персонаж не двигается по Y) и установка скорости по X(аргумент direction показывает направление персонажа. -1 и 1)
        rb.velocity = new Vector2(parameters.xDashForce * direction, 0f);

        //поворот спрайта персонажа
        Turn();

        //задержка 
        yield return new WaitForSeconds(parameters.xDashTime);

        //остановка движения после дэша
        StopMoving();

        //если персонаж совершал дэш на земле, то следует обновить флаг
        if (collisions.ground)
        {
            flags.xDash = false;
        }

        //обновление флага, теперь персонаж "разморожен" для движений
        flags.inXDash = false;

        //обновление флага в контроллере анимаций
        animator.SetBool("inXDash", flags.inXDash);

        //включение heatBox персонажа
        heatBox.SetActive(true);
        
    }


    //корутина вертикольного дэша
    IEnumerator yDashCoroutine()
    {

        //выключение heatBox персонажа, так как во время дэша персонаж неуязвим
        heatBox.SetActive(false);

        //обнуление скорости по X(во время вертикольного дэша персонаж не двигается по X) и установка скорости по Y
        rb.velocity = new Vector2(0f, parameters.yDashForce);

        //задержка
        yield return new WaitForSeconds(parameters.yDashTime);

        //сразу после конца задержки – обнуление скорости по X и Y (останавливаемся в воздухе)
        rb.velocity = new Vector2(0f, 0f);

        //обновление флага, теперь персонаж "разморожен" для движений
        flags.inYDash = false;

        //обновление флага в контроллере анимаций
        animator.SetBool("inYDash", flags.inYDash);

        //включение heatBox персонажа
        heatBox.SetActive(true);
    }
}
