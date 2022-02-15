using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseCharacterController : MonoBehaviour
{


    //объявление объекта, на котором сфокусирование внимание персонажа во время боя
    [SerializeField] private GameObject AttentionObject;
    


    //объявление объекта с хитбоксом персонажа
    [SerializeField] protected GameObject heatBox;


    //объявление объекта с атаковыми зонами персонажа
    [SerializeField] protected GameObject attackZone;


    //объявление объекта с левой атаковой зоной
    [SerializeField] protected GameObject leftCollider;


    //объявление объекта с правой атаковой зоной
    [SerializeField] protected GameObject rightCollider;


    //объявление массива объектов с префабами пуль
    [SerializeField] protected GameObject[] bullets = new GameObject[5];


    //объявление объекта с пулей(для создания префаба)
    protected GameObject bullet;


    //объявление объекта со всеми флагами
    public BaseCharacterFlags flags;


    //объявление объекта со всеми коллизиями
    public BaseCharacterCollisions collisions;


    //объявление объекта со всеми параметрами
    public BaseCharacterParameters parameters;


    //объялвение объекта со всеми статусами
    public BaseCharacterStatus status;


    //объявление объекта со всеми последними коллизиями
    protected BaseCharacterLastCollisions lastCollisions;

    //protected HeroSoundControl sound;


    //объявление компонента Sprite персонажа
    protected SpriteRenderer sprite;


    //объявление компонента Rigidbody2D персонажа
    protected Rigidbody2D rb;


    //объявление компонента Animator персонажа
    protected Animator animator;


    //объявление компонента объекта с паузой
    //protected PauseMenu pauseMenu;
    protected bool pauseMenu;


    //передаваемая персонажу сила по горизонтали
    private float xVelocity;


    //передаваемая персонажу сила по вертикали
    private float yVelocity;




    protected void Init()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        heatBox = transform.GetChild(0).gameObject;
        attackZone = transform.GetChild(2).gameObject;
        leftCollider = attackZone.transform.GetChild(0).gameObject;
        rightCollider = attackZone.transform.GetChild(1).gameObject;
        pauseMenu = false;//GameObject.Find("EventSystem").GetComponent<PauseMenu>();
        status = GetComponent<BaseCharacterStatus>();
        parameters = GetComponent<BaseCharacterParameters>();
        collisions = GetComponent<BaseCharacterCollisions>();
        flags = GetComponent<BaseCharacterFlags>();
        lastCollisions = GetComponent<BaseCharacterLastCollisions>();
    }

    protected void DependencyCheck()
    {
        if (!GetComponent<BaseCharacterStatus>())
        {
            gameObject.AddComponent<BaseCharacterStatus>();
        }

        if (!GetComponent<BaseCharacterParameters>())
        {
            gameObject.AddComponent<BaseCharacterParameters>();
        } 

        if (!GetComponent<BaseCharacterCollisions>())
        {
            gameObject.AddComponent<BaseCharacterCollisions>();
        }

        if (!GetComponent<BaseCharacterFlags>())
        {
            gameObject.AddComponent<BaseCharacterFlags>();
        }

        if (!GetComponent<BaseCharacterLastCollisions>())
        {
            gameObject.AddComponent<BaseCharacterLastCollisions>();
        }
    }


    public void Restart()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        rb.velocity = new Vector2(0f, 0f);
        flags.isMoving = false;
        flags.onFight = false;
        flags.inJump = false;
        flags.forcedFall = false;
        flags.xDash = false;
        flags.inXDash = false;
        flags.yDash = false;
        flags.inYDash = false;
        flags.isFalling = false;
        flags.xDashActive = false;
        flags.yDashActive = false;

        animator.SetBool("isMoving", flags.isMoving);
        animator.SetBool("onFight", flags.onFight);
        animator.SetBool("inJump", flags.inJump);
        animator.SetBool("forcedFall", flags.forcedFall);
        animator.SetBool("xDash", flags.xDash);
        animator.SetBool("inXDash", flags.inXDash);
        animator.SetBool("yDash", flags.yDash);
        animator.SetBool("inYDash", flags.inYDash);
        animator.SetBool("isFalling", flags.isFalling);
        animator.SetBool("xDashActive", flags.xDashActive);
        animator.SetBool("yDashActive", flags.yDashActive);

        animator.SetTrigger("Restart");
        
    }


    //метод высчета расстояния между персонажем и передаваемым объектом
    private float GetDistanceToObj(GameObject gameObject)
    {
        Vector3 position = gameObject.transform.position;
        return (float)Math.Sqrt( Math.Pow(position.x - transform.position.x, 2) + Math.Pow(position.y - transform.position.y, 2) );
    }



}
