using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSoundControl : MonoBehaviour
{
    //объявление звуков
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip doubleJump;
    [SerializeField] private AudioClip moveInAir;
    [SerializeField] private AudioClip xDash;
    [SerializeField] private AudioClip weaponChange;
    [SerializeField] private AudioSource audioSource;

    //объявление словаря со звуками
    private Dictionary<string, AudioClip> bankOfSound = new Dictionary<string, AudioClip>();

    //объявление активного звука
    private string activeSound;


    void Start()
    {

        //заполнение словаря со звуками
        bankOfSound.Add("walk", walk);
        bankOfSound.Add("run", run);
        bankOfSound.Add("jump", jump);
        bankOfSound.Add("moveInAir", moveInAir);
        bankOfSound.Add("doubleJump", doubleJump);
        bankOfSound.Add("xDash", xDash);
        bankOfSound.Add("weaponChange", weaponChange);
    }

    //смена активного звука. key – передаваемый параметр, сравнивается с ключем из словаря выше
    public void changeAudioClip(string key)
    {

        //если происходит остановка движения
        if (key == "stopMoving")
        {
            
            //остановка звука
            audioSource.Stop();

            //обновление активного звука
            activeSound = key;

        }

        //проверка на то, чтоб одно и то же собитие случайно не запускало один и тот же звук постоянно
        else if (activeSound != key)
        {   
            //остановка звука
            audioSource.Stop();

            //смена звука по переданному ключу
            audioSource.clip = bankOfSound[key];

            //запуск нового звука
            audioSource.Play();

            //обновление активного звука
            activeSound = key;

        }

    }
    
}
