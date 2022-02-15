using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackZoneTriggerHandler : MonoBehaviour
{

    [SerializeField] private BaseCharacterCollisions collisions;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Enemy")
        {
            collisions.enemy = true;
            Debug.Log("Подошел ко врагу");
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collisions.enemy = false;
            Debug.Log("Отошел от врага");
        }
    }

    protected void Init()
    {
        collisions = transform.root.gameObject.GetComponent<BaseCharacterCollisions>();
    }

}
