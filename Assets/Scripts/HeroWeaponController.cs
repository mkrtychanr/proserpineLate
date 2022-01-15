using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroWeaponController : MonoBehaviour
{
    //объявление компоненов Unity
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject weaponTexture0;
    [SerializeField] private GameObject weaponTexture1;
    [SerializeField] private GameObject weaponTexture2;
    [SerializeField] private GameObject weaponTexture3;
    private BaseCharacterStatus stats;

    //инициализация актуального оружия
    private int actualWeapon = 0;

    //инициализация актуального уровня оружия
    private int actualLevel  = 0;

    //метод смены актуального оружия
    private void selectAnotherWeapon()
    {
        //смена текстур используемого до этого модуля
        switch (actualWeapon)
            {
                case 0:
                    weaponTexture0.GetComponent<RawImage>().color = Color.white;
                    break;
                
                case 1:
                    weaponTexture1.GetComponent<RawImage>().color = Color.white;
                    break;
                
                case 2:
                    weaponTexture2.GetComponent<RawImage>().color = Color.white;
                    break;

                case 3:
                    weaponTexture3.GetComponent<RawImage>().color = Color.white;
                    break;
            }

        //смена текстуры выбранного модуля
        switch (stats.selectedWeapon)
        {
            case 0:
                weaponTexture0.GetComponent<RawImage>().color = Color.red;
                break;
            
            case 1:
                weaponTexture1.GetComponent<RawImage>().color = Color.red;
                break;
            
            case 2:
                weaponTexture2.GetComponent<RawImage>().color = Color.red;
                break;

            case 3:
                weaponTexture3.GetComponent<RawImage>().color = Color.red;
                break;
        }
        //теперь используемый модуль является выбранным
        actualWeapon = stats.selectedWeapon;
    
    }
    //изменения отображаемого количества орудий
    private void updateWeaponTextures()
    {
        
        switch (stats.weaponLevel)
        {
            case 0:
                weaponTexture0.SetActive(true);
                weaponTexture1.SetActive(false);
                weaponTexture2.SetActive(false);
                weaponTexture3.SetActive(false);
                break;

            case 1:
                weaponTexture0.SetActive(true);
                weaponTexture1.SetActive(true);
                weaponTexture2.SetActive(false);
                weaponTexture3.SetActive(false);
                break;

            case 2:
                weaponTexture0.SetActive(true);
                weaponTexture1.SetActive(true);
                weaponTexture2.SetActive(true);
                weaponTexture3.SetActive(false);
                break;

            case 3:
                weaponTexture0.SetActive(true);
                weaponTexture1.SetActive(true);
                weaponTexture2.SetActive(true);
                weaponTexture3.SetActive(true);
                break;

        }

        actualLevel = stats.weaponLevel;
        stats.selectedWeapon = 0;
        selectAnotherWeapon();

    }

    void Start()
    {
        stats = hero.GetComponent<HeroStatus>();
        actualLevel = stats.weaponLevel;
        weaponTexture0.GetComponent<RawImage>().color = Color.red;
        updateWeaponTextures();

    }

    void Update()
    {
        //если выбранное в контроллере персонажа оружие не соответствует актуальному здесь
        if (stats.selectedWeapon != actualWeapon)
        {
            selectAnotherWeapon();
        }

        //если поменялся уровень оружия
        if (stats.weaponLevel != actualLevel)
        {
            updateWeaponTextures();
        }

    }
}
