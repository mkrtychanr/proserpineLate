using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLegsTriggerHandler : MonoBehaviour
{
    //protected BaseCharacterInfo previousInfo;

    protected BaseCharacterCollisions collisions;
    
    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Ground")
        {
            collisions.ground = false;
            Debug.Log("Я оторвался от земли");
        }
        if (collider.gameObject.tag == "Wall")
        {
            collisions.wall = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            collisions.ground = true;
        }
        if (collider.gameObject.tag == "Wall")
        {
            collisions.wall = true;
            //Debug.Log("Collision with wall");
        }
    }

    protected void Init()
    {
        collisions = transform.root.gameObject.GetComponent<BaseCharacterCollisions>();
    }
    
}
