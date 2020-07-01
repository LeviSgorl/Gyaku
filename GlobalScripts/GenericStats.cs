using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStats : MonoBehaviour
{
    [Header("Hp")]
    public float health;
    public float fullhealth;
    public float Shield;

    [Header("Gravity")]
     public float GforcePadrão;
    public float Gforce;
     public float GravScale;
     [Header("Speed")]
    public float velocityMag;
    public float velocityMagEnter;
     public float dragPadrao;
    public float dragPadraoRun;
    public float dragPadraoWalk;
    public float dragPadraoJump;
    public float dragPadraoKnocked;
    public float dragPadraoNeutral;
     public float velocidadepadraoWalking;
    public float velocidadepadraoRunning;
    public float velocidadepadrao;
    [Header("Jump")]

     public float JumpForce;
    public float JumpHAmount;
    public float JumpInit;
    public float JumpDelayDefault;
    [HideInInspector]
    public float JumpVAmount;
    [Header("Colission Properties")]
     public float groundpoundcd;
     public float IngroundCd;
    public float Friction;
    [Header("Rolling")]
    public float scale;
    public float RollingTimerPadrao;
    public float RollingTimerOnAir;
    public float RollingForcePadrao; 
    public float RollScale;
    [Header("Other")]
    public int PressedKeys;
    public float TurnTime;
    public float GrabCD;
    public Vector3 HpBarPos;
    public float TrowingForce;

    #region Hidden Variaveis
    [HideInInspector]
    public bool OnControl;

    [HideInInspector]
    public float RollingForce;
    [HideInInspector]
    public float RollingTimer;
    [Header("Facing")]
    public bool Iup;
    public bool Idown;
    public bool Ileft;
    public bool Iright;
    public bool Iupleft;
    public bool Iupright;
    public bool Idownleft;
    public bool Idownright;

    [HideInInspector]
    public float velocidade;
    
    #endregion
     [HideInInspector]
    public GenericInput InputField;
     [HideInInspector]
    public GameObject HpBar;
     [HideInInspector]
    public Object HPbarIcon;

    

    protected virtual void Awake()
    {
        InputField = gameObject.GetComponent<GenericInput>();
        HPbarIcon = Resources.Load("hpbarbig");
        HpBar = Instantiate(HPbarIcon,null,true) as GameObject;
        HpBar.transform.localPosition = HpBarPos;
        HpBar.GetComponent<HpHud>().Target = gameObject;
        OnControl = true;
    }
    public virtual void FixedUpdate()
    {
        HealthMath();
        RollingMath();

        DiagonalFixMath();
        CrawlMath();
        
        
        

    }

   
    public void CrawlMath()
    {
        if (InputField.Crawl == true)
        {
            if (InputField.Diagonalfix == true)
            {
                velocidade = velocidadepadrao / 4.6f;

            }
            else
            {
                velocidade = velocidadepadrao / 3.3f;
            }
        }
    }

    public void DiagonalFixMath()
    {
        if (InputField.Crawl == false)
        {
            if (InputField.Diagonalfix == true)
            {
                velocidade = velocidadepadrao / 1.42f;

            }
            else
            {
                velocidade = velocidadepadrao;
            }
        }
    }
    public void RollingMath()
    {
        RollingTimer -= Time.deltaTime;

        RollingForce = RollingForcePadrao;
    }

    public void HealthMath()
    {
        if (health > fullhealth)
        {
            health = fullhealth;
        }

        if (health < 0)
        {
            health = 0;
        }
    }
}


