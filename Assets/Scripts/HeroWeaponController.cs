using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroWeaponController : MonoBehaviour
{

    //объявление компоненов Unity
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject[] weapons;
    private BaseCharacterStatus stats;

    //инициализация актуального оружия
    private int actualWeapon = 0;

    //смена выбранного оружия
    private void changeWeapon()
    {

        //отключение игрового объекта с активным до этого оружия
        weapons[actualWeapon].SetActive(false);

        //включение игрового объекта с активным оружем
        weapons[stats.selectedWeapon].SetActive(true);

        //обновление актуального оружия
        actualWeapon = stats.selectedWeapon;
    }

    void Start()
    {
        stats = hero.GetComponent<HeroStatus>();
        actualWeapon = stats.selectedWeapon;
    }

    void Update()
    {
        //если оружие было смененно в контроллере
        if (stats.selectedWeapon != actualWeapon)
        {
            changeWeapon();
        }
    }

}
