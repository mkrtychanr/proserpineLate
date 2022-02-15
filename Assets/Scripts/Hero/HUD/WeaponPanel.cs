using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    private BaseCharacterStatus status;
    [SerializeField] private GameObject[] weapons = new GameObject[5];

    private int current;

    void Init()
    {
        status = GameObject.Find("Hero").GetComponent<BaseCharacterStatus>();
        current = status.selectedWeapon;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        weapons[current].SetActive(true);

    }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (current != status.selectedWeapon)
        {
            weapons[current].SetActive(false);
            current = status.selectedWeapon;
            weapons[current].SetActive(true);
        }
    }
}
