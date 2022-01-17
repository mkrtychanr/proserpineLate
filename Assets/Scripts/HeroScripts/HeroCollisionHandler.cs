using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroController))]

public class HeroCollisionHandler : MonoBehaviour
{

    //объявление экземпляра типа HeroController
    private HeroController hero;

    //объявление экземпляра типа HeroStatus
    private HeroStatus stats;

    void Start()
    {   
        //инициализация экземпляра типа HeroController через поиск компонента
        hero = GetComponent<HeroController>();

        //инициализация экземпляра типа HeroStatus через поиск компонента
        stats = GetComponent<HeroStatus>();
    }

    //метод, обрабатываемый при коллизиях.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //если регестрируется коллизия с объектом с тегом Floor (персонаж коснулся пола), то выполняется сл. блок
        if (collision.gameObject.tag == "Floor")
        {

            //персонаж перестал падать
            stats.flags["isFalling"] = false;

            //персонаж не в прыжке больше
            stats.flags["inJump"] = false;

            //персонаж не в двойном прыжке
            stats.flags["inDoubleJump"] = false;

            //передаем новые сосотояния в аниматор персонажа
            hero.anim.SetBool("isFalling", stats.flags["isFalling"]);
            hero.anim.SetBool("inJump", stats.flags["inJump"]);
            hero.anim.SetBool("inDoubleJump", stats.flags["inDoubleJump"]);

        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FallZone" && stats.flags["inJump"] == false)
        {
            Debug.Log("FallZone");
            stats.flags["isFalling"] = true;
            hero.anim.SetBool("isFalling", stats.flags["isFalling"]);
        }    
    }
}
