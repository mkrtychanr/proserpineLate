using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIStatus : MonoBehaviour
{

    //объявление компонентов Unity
    [SerializeField] public InputField panel;
    [SerializeField] protected BaseCharacterStatus stats;
    

    //значение по-умолчанию, прописанное в коде
    protected int inDefault;

    //актуальное значение
    protected int current;

    //временное значение
    protected int temp;

    //имя поля, для удобного поиска по словарю
    protected string field;

    //установка по дефолту
    public void setAsDefault()
    {
        current = inDefault;
        Debug.Log("Default is" + inDefault);
        panel.text = current.ToString();
    }

    //передача значений в объект с состояниями
    void setToGame()
    {

        //при неудачной попытке TryParse в current запишется ноль. На такой случай хранится состояние переменной current до TryParse
        temp = current;

        //возможно ли записать значение в UI элементе в переменную типа int
        if (int.TryParse(panel.text, out current))
        {

            //да
            stats.status[field] = current;

        }
        else
        {
            //нет. В UI элемент запишется то, что было актуальным до попытки применить изменения

            //восстановление состояния переменной current
            current = temp;

            //запись актуального значения в UI элемент
            panel.text = current.ToString();

        }

    }

    //метод сброса несохраненных полей
    public void UpdatePanel()
    {
        panel.text = current.ToString();
    }

}
