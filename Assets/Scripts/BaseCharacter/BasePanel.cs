using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{

    private BaseCharacterStatus status;
    private GameObject[] panel = new GameObject[12];

    private int current;

    private string field;

    protected void Init(string name)
    {
        field = name;
        status = transform.root.gameObject.GetComponent<BaseCharacterController>().status;
        current = status.Get(field);
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i] = transform.GetChild(i).gameObject;
        }

        for (int i = status.Get(field); i < panel.Length; i++)
        {
            panel[i].SetActive(false);
        }
    }

    protected void Update()
    {
        if (current != status.Get(field))
        {
            for (int i = 0; i < status.Get(field); i++)
            {
                panel[i].SetActive(true);
            }

            for (int i = status.Get(field); i < panel.Length; i++)
            {
                panel[i].SetActive(false);
            }        
            current = status.Get(field);
        }
    }
}
