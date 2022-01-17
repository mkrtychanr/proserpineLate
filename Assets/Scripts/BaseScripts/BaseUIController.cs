using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{

    //объявление компонентов Unity
    [SerializeField] protected InputField panel;
    [SerializeField] protected BaseCharacterController stats;


    //значение по-умолчанию, прописанное в коде
    protected float inDefault;

    //актуальное значение
    protected float current;

    //временное значение
    protected float temp;

    //имя поля, для удобного поиска по словарю
    protected string field;

    //установка по дефолту
    public void setAsDefault()
    {
        current = inDefault;
        panel.text = current.ToString();
        stats.parameters[field] = current;
    }

    //передача значений в объект с состояниями
    public void setToGame()
    {

        //при неудачной попытке TryParse в current запишется ноль. На такой случай хранится состояние переменной current до TryParse
        temp = current;

        //возможно ли записать значение в UI элементе в переменную типа int
        if (float.TryParse(panel.text, out current))
        {

            //да
            stats.parameters[field] = current;

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
