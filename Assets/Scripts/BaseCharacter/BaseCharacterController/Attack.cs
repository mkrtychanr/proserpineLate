using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController
{

    //координаты стартовой точки пули (для правильного места создания префаба)
    private Vector3 bulletStartPosition;

    //метод восполнения ячейки здоровья и энергии
    protected void Recovery()
    {
        if (status.health < status.healthAndEnergyMax)
        {

            status.health++;

        }

        if (status.energy < status.healthAndEnergyMax)
        {

            status.energy++;

        }
    }

    protected void TakeDamage(int damage)
    {

        status.health -= damage;

    }

    protected void LossEnergy(int loss)
    {

       status.energy -= loss;
        
    }

    protected void Shoot()
    {
        switch (status.selectedWeapon)
        {
            case 0:
                if (true)
                {
                    if (status.eyesDirection == 1)
                    {
                        bulletStartPosition = rightCollider.transform.position;
                    }
                    else
                    {
                        bulletStartPosition = leftCollider.transform.position;
                    }

                    bullet = Instantiate(bullets[0], bulletStartPosition, Quaternion.identity) as GameObject;
                    bullet.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
                    bullet.GetComponent<BaseBullet>().Launch(AttentionObject);
                    
                }
                else
                {
                    //звук перезарядки (типо не может стрельнуть)
                }
                break;
        }
    }


}
