using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{


    //метод смены оружия. На вход поступает переменная от 0 до 3. И в зависимости от уровня оружия происходит особая обработка аргумента
    //в том случае, если уровень оружия равен 0, то и первое оружие в панели будет иметь номер 0
    //в том случае, если уровень оружия равен 1, то первое оружие в панели имеет номер 0, а второе – 2
    //в том случае, если уровень оружия равен 2, то первое оружие в пенели уже будет иметь номер 1, второе – 2
    //дальше идет в обычном порядке, на третьем уровне орудия 1 и 2 будут иметь такой же номер, как и на втором, а третье орудие номер 3
    //в четвером так-же. Такая необычная механика выходит из ГДД.
    protected void SelectWeapon(int weapon)
    {

        //если уровень оружия меньше 2
        if (status.weaponLevel < 2)
        {

            //если аргумент равен нулю.
            if (weapon == 0)
            {

                //установка вызванного оружия
                status.selectedWeapon = weapon;

                //вызов звука
                //sound.changeAudioClip("weaponChange");

            }
            
            //если аргумент равен единице и уровень равен единице
            if (weapon == 1 && status.weaponLevel == 1)
            {
                
                //присваивание аргументу новое значение
                weapon = 2;

                //установка вызванного оружия
                status.selectedWeapon = weapon;

                //вызов звука
                //sound.changeAudioClip("weaponChange");

            }
        }
        else
        {
            //увеличиваем значение аргумента, так как после первого уровня оружия номер зажатой кнопки соответствует вызываемому оружию
            weapon++;
            if (status.weaponLevel >= weapon)
            {

                //установка вызванного оружия
                status.selectedWeapon = weapon;

                //вызов звука
                //sound.changeAudioClip("weaponChange");
            }
        }
    }

    
    //метод увеличения уровня оружия
    protected void weaponLevelUp()
    {
        //увелиение уровня
        status.weaponLevel++;

        //проверка случая когда выбранное орудие теперь недоступно (2 уровень оружия, пропадает нулевое орудие и вместо него появляется первое).
        weaponLevelCheck();
    }
    
    //проверка на невозможность управления оружием с текущим уровнем
    protected void weaponLevelCheck()
    {

        //если уровень оружия больше первого, то нулевое орудие становится более первым
        if (status.weaponLevel > 1 && status.selectedWeapon == 0) 
        {
            SelectWeapon(0);
        }

        //проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (status.weaponLevel == 1 && status.selectedWeapon != 0 && status.selectedWeapon != 2)
        {
            SelectWeapon(0);

        }

        ////проверка на невозможность носимого оружия с выбранным (предупреждаем баги)
        if (status.weaponLevel != 1 && status.selectedWeapon > status.weaponLevel)
        {
            SelectWeapon(0);
        }
        
    }
}
