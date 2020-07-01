using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInput : MonoBehaviour
{
    [Header("Walk Directions")]
    public bool walkingright;
    public bool walkingleft;
    public bool walkingdown;
    public bool walkingup;
    public bool WalkingTrigger;
    [Header("Rolling Directions")]
    public bool RollLeft;
    public bool RollRight;
    public bool RollUp;
    public bool RollDown;
    public bool RollUpLeft;
    public bool RollUpRight;
    public bool RollDownLeft;
    public bool RollDownRight;

    [Header("Run State")]

    [Header("Player States")]
    public bool Running;
    public bool RunJumpBoost;
    public bool RunningJump; 
    public bool MaxRun;
    [Header("Crawl States")]
    public bool Crawl;
    public bool Rolling;
    public bool CrawlTrigger;
    
    [Header("Jump States")]
    public bool Jumping;
    public bool HoldingJump;
    public bool JumpHboost;
    public bool JumpStart;
    public bool RollOnAir;
     public bool rollVboost;
    
    [Header("Grab States")]
    public bool Grabbing; 
    public bool Trowing;
    public bool HoldingItem;

    [Header("Neutral States")]
    public bool Target;  
    public bool Inground;
    public bool ActivateGrav;

    [Header("Others")]
    public bool GTest;  
    public bool checkGround;
    public bool Diagonalfix;
    public bool RollVM;
    [HideInInspector]
    public GenericStats Stats;  
    private GenericMovement GenericMov;
    public Vector3 GroundColOri;
    public Vector3 LookDir;
    public Vector3 DirParalel;
    public bool TrowEngage;
    public bool TrowMax;
   
   
   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        GenericMov = gameObject.GetComponent<GenericMovement>();
        Stats = gameObject.GetComponent<GenericStats>();
        checkGround = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GroundInput();
        LooDirLogic();
        

    }

    public void LooDirLogic(){
        if(Stats.Iup){
            LookDir = DirUp;
            DirParalel = DirRight;
        }
        if(Stats.Idown){
            LookDir = DirDown;
             DirParalel = DirLeft;
        }
        if(Stats.Iupleft){
            LookDir = DirUpLeft;
            DirParalel = DirUpRight;
        }
        if(Stats.Idownleft){
            LookDir = DirDownLeft;
            DirParalel = DirUpLeft;
        }
         if(Stats.Iupright){
            LookDir = DirUpRight;
            DirParalel = DirDownRight;
        }
        if(Stats.Idownright){
            LookDir = DirDownRight;
            DirParalel = DirDownLeft;
        }
         if(Stats.Iright){
            LookDir = DirRight;
            DirParalel = DirDown;
        }
        if(Stats.Ileft){
            LookDir = DirLeft;
            DirParalel = DirUp;
        }
    }
    
       public void GroundInput(){
        Stats.IngroundCd -= Time.deltaTime;
        if (checkGround)
        {
            Inground = GTest;
           
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        GroundColOri = collision.contacts[0].normal;
        
        if (collision.contacts[0].normal.z < -0.8f)
        {
            GTest = true;
            Stats.TurnTime = 0.2f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        GroundColOri = collision.contacts[0].normal;
        if (collision.contacts[0].normal.z < -0.8f)
        {
            GTest = true;
            Stats.TurnTime -= Time.deltaTime;
        }
        
    }

     public void WalkingInputGeneric()
    {
        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingleft = true;

            Stats.Ileft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingup = true;
            Stats.Iup = true;
            Stats.Ileft = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingdown = true;
             Stats.Idown = true;
            Stats.Ileft = Stats.Iup = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingright = true;
            Stats.Iright = true;
            Stats.Ileft = Stats.Idown = Stats.Iup = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }     
         
        ///////////////////////////////////////////////////////////////////////////
        

        //if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingright = false;
           
        }

       // if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingup = false;
        }

        //if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingleft = false;
        }

       // if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingdown = false;
        }
        /////////////////////////////////////////////////////



    }
    private void OnCollisionExit(Collision collision)
    {    
           GTest = false;     
    }
    public static readonly Vector3 DirUp = new Vector3(0,1,0);
    public static readonly Vector3 DirDown = new Vector3(0,-1,0);
    public static readonly Vector3 DirUpLeft = new Vector3(-1,1,0);
    public static readonly Vector3 DirDownLeft = new Vector3(-1,-1,0);
    public static readonly Vector3 DirDownRight = new Vector3(1,-1,0);
    public static readonly Vector3 DirUpRight = new Vector3(1,1,0);
    public static readonly Vector3 DirRight = new Vector3(1,0,0);
    public static readonly Vector3 DirLeft = new Vector3(-1,0,0);
}

           
           
            
       
      
