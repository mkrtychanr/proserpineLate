using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    //пауза
    public bool isPaused = false;

    //объявление компонентов Unity
    [SerializeField] private GameObject pause;

    private GameObject statusFields;
    private GameObject controllerFields;
    private BaseUIStatus[] status = new BaseUIStatus[4];
    private BaseUIController[] controller = new BaseUIController[6];
    
    //метод вызываемый при паузе
    private void Pause()
    {

        //включение объекта с интерфейсом паузы
        pause.SetActive(true);

        //остановка игрового времени
        Time.timeScale = 0f;

        //установка на паузу
        isPaused = true;

        //обновление объектов с данными (эти два цикла удаляют несохраненные изменения)
        for (int i = 0; i < status.Length; i++)
        {
            status[i].UpdatePanel();

        }

        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].UpdatePanel();

        }

    }

    //метод вызываемый после паузы
    private void Resume()
    {

        //выключение объекта с интерфейсом паузы
        pause.SetActive(false);

        //возобновление игрового времени
        Time.timeScale = 1f;

        //снятие с паузы
        isPaused = false;

    }

    private void setToDefault()
    {
        
        for (int i = 0; i < status.Length; i++)
        {
            status[i].setAsDefault();

        }

        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].setAsDefault();

        }
    }

    void Start()
    {
        //выключение объекта с интерфейсом паузы(на всякий случай)
        pause.SetActive(true);

        //снятие с паузы (на всякий случай)
        isPaused = true;

    }

    void Awake()
    {
        init();
    }

    void init()
    {
        statusFields = GameObject.Find("StatusFields");
        controllerFields = GameObject.Find("ControllerFields");

        status[0] = statusFields.transform.GetChild(0).GetComponent<HealthMaxUIStatus>();
        status[1] = statusFields.transform.GetChild(1).GetComponent<HealthUIStatus>();
        status[2] = statusFields.transform.GetChild(2).GetComponent<WeaponLevelUIStatus>();
        status[3] = statusFields.transform.GetChild(3).GetComponent<SelectedWeaponUIStatus>();

        controller[0] = controllerFields.transform.GetChild(0).GetComponent<WalkSpeedUIController>();
        controller[1] = controllerFields.transform.GetChild(1).GetComponent<FightSpeedUIController>();
        controller[2] = controllerFields.transform.GetChild(2).GetComponent<FallSpeedUIController>();
        controller[3] = controllerFields.transform.GetChild(3).GetComponent<JumpXSpeedUIController>();
        controller[4] = controllerFields.transform.GetChild(4).GetComponent<JumpYSpeedUIController>();
        controller[5] = controllerFields.transform.GetChild(5).GetComponent<DoubleJumpYSpeedUIController>();

    }

    void Update()
    {

        //если нажата кнопка паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {

                //если до этого игра стояла на паузе, то продолжить игру
                Resume();

            }
            else
            {

                //поставить на паузу, если игра на ней не стоит
                Pause();

            }

        }

        if (Input.GetKeyDown(KeyCode.D) && isPaused)
        {
            setToDefault();
        }

    }
    
}
