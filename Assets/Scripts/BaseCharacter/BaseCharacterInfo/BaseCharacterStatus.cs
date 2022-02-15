using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStatus : MonoBehaviour
{
    public int healthAndEnergyMax = 5;

    
    public int health = 4;

    
    public int energy = 4;

    
    public int weaponLevel = 0;

    
    public int selectedWeapon = 0;

    
    public int direction = 1;

    
    public int wallCollisionDirection = 0;

    
    public int eyesDirection = 1;

    public int Get(string key)
    {
        if (key == "health")
        {
            return health;
        }
        else if (key == "energy")
        {
            return energy;
        }
        else
        {
            Debug.Log($"Wrong status key in get. key: {key}");
            return 0;
        }
    }


}
