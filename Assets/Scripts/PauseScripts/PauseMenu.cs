using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    //пауза
    public bool isPaused = false;

    //объявление компонентов Unity
    [SerializeField] private GameObject pause;
    [SerializeField] private BaseUIStatus[] status;
    [SerializeField] private BaseUIController[] controller;
    
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
            Debug.Log(i + "is clr");
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
        pause.SetActive(false);

        //снятие с паузы (на всякий случай)
        isPaused = false;

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
            Debug.Log("Set to default");
            setToDefault();
        }

    }
    
}
