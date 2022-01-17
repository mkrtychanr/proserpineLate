using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    //пауза
    public bool isPaused = false;

    //объявление компонента Unity
    [SerializeField] private GameObject pause;

    //объект GameObject, в котором хранятся статусы
    private GameObject statusFields;

    //объект GameObject, в котором хранятся контроллеры
    private GameObject controllerFields;
    
    //объявление массива статусов
    private BaseUIStatus[] status = new BaseUIStatus[4];

    //объявление массива контроллеров
    private BaseUIController[] controller = new BaseUIController[6];

    //метод, вызываемый при паузе
    private void Pause()
    {

        //включение объекта с интерфейсом паузы
        pause.SetActive(true);

        //остановка игрового времени
        Time.timeScale = 0f;

        //установка на паузу
        isPaused = true;

        //в цикле обратабываются все статусы
        for (int i = 0; i < status.Length; i++)
        {

            //установка i - ого элемента в актуальное значение
            status[i].UpdatePanel();

        }

        //в цикле обратабываются все контроллеры
        for (int i = 0; i < controller.Length; i++)
        {

            //установка i - ого элемента в актуальное значение
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

    //метод, вызываемый для установки всего на дефолт
    public void setToDefault()
    {
        
        //в цилке обрабатываются все статусы
        for (int i = 0; i < status.Length; i++)
        {

            //установка i - ого элемента в дефолтное значение
            status[i].setAsDefault();

        }

        //в цилке обрабатываются все контроллеры
        for (int i = 0; i < controller.Length; i++)
        {
            //установка i - ого элемента в дефолтное значение
            controller[i].setAsDefault();

        }
    }

    //метод применения введенных полей
    public void apply()
    {

        //в цилке обрабатываются все статусы
        for (int i = 0; i < status.Length; i++)
        {

            //установка i - ого элемента в введенное значение
            status[i].setToGame();

        }

        //в цилке обрабатываются все контроллеры
        for (int i = 0; i < controller.Length; i++)
        {

            //установка i - ого элемента в введенное значение
            controller[i].setToGame();

        }
    }

    void Start()
    {
        //выключение объекта с интерфейсом паузы(на всякий случай)
        pause.SetActive(false);

        //снятие с паузы (на всякий случай)
        isPaused = false;

    }

    void Awake()
    {
        init();
    }

    
    void init()
    {

        //поиск объекта со статусами по имени
        statusFields = GameObject.Find("StatusFields");

        //поиск объекта с контроллерами по имени
        controllerFields = GameObject.Find("ControllerFields");

        //инициализация массива со статусами
        status[0] = statusFields.transform.GetChild(0).GetComponent<HealthMaxUIStatus>();
        status[1] = statusFields.transform.GetChild(1).GetComponent<HealthUIStatus>();
        status[2] = statusFields.transform.GetChild(2).GetComponent<WeaponLevelUIStatus>();
        status[3] = statusFields.transform.GetChild(3).GetComponent<SelectedWeaponUIStatus>();

        //инициализация массива с контроллерами
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
