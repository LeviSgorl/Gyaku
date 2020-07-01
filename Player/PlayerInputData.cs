using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputData : GenericInput
{
    // Start is called before the first frame update
    [Header("Keys")]
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;

    public KeyCode Jump;
    public KeyCode Grab;
    public KeyCode Crounch;

    public KeyCode Run;
    

    
    
    
    /// <summary>
    /// Animation bools
    /// </summary>
    
  

    protected override void Start()
    {
        base.Start();
       
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        WalkingInput();
        DiagonalFix();
        CrawlInput();
        RollingInput();
        JumpInput();
        RunInput();
        TrowInput();
        GrabInput();
       
        
    }

  
    public void GrabInput(){
        
        if(Input.GetKey(Grab) && Inground == true && Stats.GrabCD < 0){
            Grabbing = true;
            
        }
        if(!Input.GetKey(Grab)){
            Grabbing = false;
        }
    }
    public void TrowInput(){
        Stats.GrabCD -= Time.deltaTime;
         if(Input.GetKeyDown(Grab) && HoldingItem == true){
          TrowEngage = true;
            Stats.TrowingForce = 0;
        }
         if(Input.GetKey(Grab) && TrowEngage == true){
           
            if(Stats.TrowingForce < 1){
            Stats.TrowingForce += 0.05f;
            TrowMax = false;
            }else{
            TrowMax = true;
            }
        }
        if(Input.GetKeyUp(Grab) && HoldingItem == true && TrowEngage == true){
            Stats.GrabCD = 0.2f;
            Trowing = true;
            TrowEngage = false;
            
        }
    }

    public void RunInput(){
        if(Input.GetKeyDown(Run) && Inground == true && Crawl == false){
            
            if(Running == false){
                
                Running = true;
            }
            else
            {
                
                Running = false;
            }
        }
        if(Stats.PressedKeys == 0 | Crawl == true){
            Running = false;
        }
      
    }

    public void JumpInput()
    {
        if (Input.GetKeyDown(Jump) && Inground == true && Crawl == false && Jumping == false)
        {
            
            Stats.JumpInit = Stats.JumpDelayDefault;
            JumpStart = true;
            Stats.OnControl = false;
        }
        if (Input.GetKey(Jump))
        {
            HoldingJump = true;
        }
        else
        {
            HoldingJump = false;
        }
        if(Stats.JumpInit < 0 && JumpStart == true){
            Jumping = true;
            JumpStart = false;
            Stats.IngroundCd = 0.3f;
            checkGround = false;
            JumpHboost = true;
            RunJumpBoost = true;     
            Inground = false;
            Stats.OnControl = true;
        }
        Stats.JumpInit -= Time.deltaTime;
        if (Stats.IngroundCd < 0) { checkGround = true; Jumping = false; }
    }
    public void RollingInput()
    {
        if (Input.GetKeyDown(Jump) && Crawl == true && Stats.RollingTimer <= -0.2f)
        {
            Stats.OnControl = false;
            Rolling = true;
            if (Inground)
            {
                Stats.RollingTimer = Stats.RollingTimerPadrao;

            }
            else
            {
                //make it chargeable
                RollOnAir = true;
                rollVboost = true;
                Stats.RollingTimer = Stats.RollingTimerOnAir;
            }
            //vertical e horizontal
            if (Stats.Ileft == true) { RollLeft = true; }

            if (Stats.Iup == true) { RollUp = true; }

            if (Stats.Idown == true) { RollDown = true; }

            if (Stats.Iright == true) { RollRight = true; }

            //diagonal
            if (Stats.Iupleft == true) { RollUpLeft = true; }

            if (Stats.Iupright == true) { RollUpRight = true; }

            if (Stats.Idownleft == true) { RollDownLeft = true; }

            if (Stats.Idownright == true) { RollDownRight = true; }
        }
    }

    public void CrawlInput()
    {
    if (Stats.OnControl == true)
    {
        if (Input.GetKey(Crounch))
        {
            Crawl = true;
        }
        if (Input.GetKey(Crounch) == false)
        {
            Crawl = false;
        }
    }

        if (Input.GetKeyUp(Crounch))
        {
            CrawlTrigger = false;
            WalkingTrigger = true;
        }
        if (Input.GetKeyDown(Crounch))
        {
            CrawlTrigger = true;
            WalkingTrigger = false;
        }

    }
    public void WalkingInput()
    {
        if (Input.GetKeyDown(Left))
        {
            Stats.PressedKeys += 1;
            walkingleft = true;

            Stats.Ileft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKeyDown(Up))
        {
            Stats.PressedKeys += 1;
            walkingup = true;
            Stats.Iup = true;
            Stats.Ileft = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKeyDown(Down))
        {
            Stats.PressedKeys += 1;
            walkingdown = true;
             Stats.Idown = true;
            Stats.Ileft = Stats.Iup = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKeyDown(Right))
        {
            Stats.PressedKeys += 1;
            walkingright = true;
            Stats.Iright = true;
            Stats.Ileft = Stats.Idown = Stats.Iup = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }     
         
        ///////////////////////////////////////////////////////////////////////////
        

        if (Input.GetKeyUp(Right))
        {
            Stats.PressedKeys -= 1;
            walkingright = false;
           
        }

        if (Input.GetKeyUp(Up))
        {
            Stats.PressedKeys -= 1;
            walkingup = false;
        }

        if (Input.GetKeyUp(Left))
        {
            Stats.PressedKeys -= 1;
            walkingleft = false;
        }

        if (Input.GetKeyUp(Down))
        {
            Stats.PressedKeys -= 1;
            walkingdown = false;
        }
        /////////////////////////////////////////////////////



    }

    public void DiagonalFix()
    {
        if (Stats.PressedKeys >= 2)
        {
            Diagonalfix = true;
        }
        else
        {
            Diagonalfix = false;
        }
    }


}
