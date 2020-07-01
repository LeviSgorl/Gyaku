using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHumanoidMovement : GenericMovement
{
    
   [HideInInspector]
    private ParticleSystem RunParticle;
    private ParticleSystem.EmissionModule RunParticleEM;
    public GameObject ItemToHold;
    public float FlashCd;
    
    
    protected override void Start()
    {
        base.Start();
        GameObject RunDust = Instantiate(Resources.Load("RunDust"),transform) as GameObject;
        Vector3 Point = transform.root.gameObject.GetComponent<Collider>().bounds.center;
        RunDust.transform.position = Point;
        RunParticle = RunDust.GetComponent<ParticleSystem>();
        RunParticleEM = RunParticle.emission;
        Stats.Friction = 0;
        _rb = gameObject.GetComponent<Rigidbody>();
        GlobalOrientation = GameObject.Find("GlobalOri").transform;
        HoldposOffset = new Vector3(0,0,-5);
        HoldDistance = 50;
      
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Stats.OnControl == true && Keys.TrowEngage == false){
        WalkingLogic();
        JumpSequence();      
        RunMovement();
        GrabSequence();       
        }
        GravHandler();
        MaxVelEffects(); 
        HumanIngroundLogic();
        TrowSequence();
        RollingSequence();
        
        
        if(Keys.HoldingItem == true) {HoldingSequence();}
    }
    public void Update(){
        MaxThrowVisual();
    }

   

    public void MaxThrowVisual(){
        SpriteRenderer[] Sp = transform.root.GetComponentsInChildren<SpriteRenderer>();
        Material DefaultMat = transform.root.GetComponentInChildren<SpriteRenderer>().material;
        FlashCd -= Time.deltaTime;
         if(Keys.TrowMax){
           
           
           if(FlashCd < 0){
            foreach(SpriteRenderer sprite in Sp){
               if(sprite.tag != "Hud"){
                sprite.material = Resources.Load("TrowMaxFlash") as Material;
                 }
                }
                FlashCd = 0.2f;
           }    
           else
           {
                foreach(SpriteRenderer sprite in Sp){
                if(sprite.tag != "Hud"){
                sprite.material = Resources.Load("Sprites_Default2") as Material;
                }
                }
            }
           }else{
                foreach(SpriteRenderer sprite in Sp){
                if(sprite.tag != "Hud"){
                sprite.material = Resources.Load("Sprites_Default2") as Material;
                }
                }
           }     
    }

    public void TrowSequence(){
        if(Keys.Trowing == true){
            Keys.HoldingItem = false;
            ItemToHold.transform.parent = null;
            ItemToHold.TryGetComponent<Rigidbody>(out Rigidbody a);
            if(a != null){
                a.isKinematic = false;
            }
            ItemToHold.layer = 0;   
            Keys.Trowing = false;
            ItemToHold.tag = "Grabable";
            ItemD.ChangeActive = true;
            ItemD.showIcon = true;
            Rigidbody EX_rb;
            ItemToHold.TryGetComponent<Rigidbody>(out EX_rb);
            if(EX_rb != null){
            EX_rb.AddForce(Keys.LookDir * (22000 * (Stats.TrowingForce + 0.2f)) * EX_rb.mass);
            EX_rb.AddRelativeTorque(Keys.DirParalel * 1500000 * EX_rb.mass);
            EX_rb.AddForce(GlobalOrientation.up * (66000 * EX_rb.mass) / (1 + Stats.TrowingForce));
            ItemToHold.GetComponent<GenericMovement>().VBounce = true;
            ItemToHold.GetComponent<GenericMovement>().LookDirIns = Keys.LookDir;
            }
            Keys.TrowMax = false;
            ItemToHold = null;
        }
    }
    public void GrabSequence(){
        if(Keys.Grabbing == true && ItemD.Locked != null){
            ItemD.ChangeActive = false;
            
            
            if(ItemD.Locked.tag == ItemD.Target){
            MoveToItem();
            DetectOBJDistance();
            }
        }else{
            ItemD.ChangeActive = true;
             
        }
    }

    public void MoveToItem(){
        //transform.position = ItemToHold.transform.position;
    }
   
    public void HoldingSequence(){
            Keys.Grabbing = false;
            ItemD.showIcon = false;
            ItemToHold.layer = 9;     
            ObjectPositioning();
            ItemToHold.TryGetComponent<Rigidbody>(out Rigidbody a);
            if(a != null){
                a.isKinematic = true;
            }
            
    }
     public void DetectOBJDistance(){
        Vector3 Center = transform.GetComponent<Collider>().bounds.ClosestPoint(transform.position);
            Vector3 closet2 = ItemD.Locked.GetComponent<Collider>().bounds.ClosestPoint(Center);
            float distance2 = Vector3.Distance(Center,closet2);
            if(distance2 < 80){
                if(ItemToHold == null){
                ItemToHold = ItemD.Locked;
                }
                Keys.HoldingItem = true;       
                ItemToHold.tag = "HeldObject";
                ItemToHold.transform.parent = transform;
                ItemToHold.transform.localPosition = new Vector3(0,0,-10);
                
            }
    }
    public void ObjectPositioning(){
            Vector3 Center = transform.GetComponent<Collider>().bounds.center;
            Vector3 closet = ItemToHold.GetComponent<Collider>().bounds.ClosestPoint(Center);
            float distance = Vector3.Distance(Center,closet);
            HoldDistance = 70 - (20 * Stats.TrowingForce);
            if(distance < HoldDistance + 3){
            ItemToHold.transform.localPosition += HoldposOffset;
            }
             if(distance > HoldDistance - 3){
            ItemToHold.transform.localPosition -= HoldposOffset;
            }
            if(distance > 120){
            ItemToHold.transform.localPosition = new Vector3(0,0,-10);
            }
    }
    public void MaxVelEffects(){
        if(_rb.velocity.magnitude > 240){

            Keys.MaxRun = true;

        }else{
            Keys.MaxRun = false;
        }

    }  


    public void GravHandler(){
        if(!Keys.Inground && !Keys.Jumping && !Keys.rollVboost){
         Keys.ActivateGrav = true;
        }
       
    }


    public void WalkingLogic()
    {
        
            if(Keys.Inground){
                _rb.AddForce(-GlobalOrientation.up * Stats.GforcePadrão/20);
            }
            if (Keys.walkingup)
            {
                _rb.AddForce(GlobalOrientation.forward * Stats.velocidade);

            }
            if (Keys.walkingdown)
            {
                _rb.AddForce(GlobalOrientation.forward * -Stats.velocidade);

            }
            if (Keys.walkingright)
            {
                _rb.AddForce(GlobalOrientation.right * Stats.velocidade);

            }
            if (Keys.walkingleft)
            {
                _rb.AddForce(GlobalOrientation.right * -Stats.velocidade);

            }

        
    }
    public void RollingSequence()
    {
        if (Keys.Inground && Keys.RollOnAir == true)
        {
            Stats.RollingTimer = 0;
            
        }

        if(Keys.Inground){
            Keys.rollVboost = false;
        }
        if (Stats.RollingTimer > 0)
        {
            if (Keys.RollOnAir == false)
            {
                Roll(1);
            }
            else
            {
                RollVBoost();
            }

        }
        else
        {    
            Keys.RollOnAir = false;
            if (Keys.Rolling == true)
            {

                Stats.OnControl = true;

            }
            //torna falso ao acabar o rolamento
            FlushAnim((int)groups.Roll);



        }
    }
    public void HumanIngroundLogic(){
        if (Keys.Inground)
        {
            Stats.Gforce = 0;
            _rb.drag = Stats.dragPadrao;
            Keys.RollVM = true;
            Keys.ActivateGrav = false;
            Anim._anim.speed = 1;
        }
        if (!Keys.Inground && Keys.Jumping)
        {
            _rb.drag = Stats.dragPadraoJump;
        }
    }
    public void JumpSequence()
    {
        if (Keys.Jumping)
        {
            Anim._anim.speed = 1;
            if(Keys.Running){
                if(Keys.RunJumpBoost == true){
                _rb.AddForce(_rb.velocity * _rb.velocity.magnitude * 800);

                Keys.RunningJump = true;

                Keys.RunJumpBoost = false;
                }
             Keys.Running = false;
            }

           

            _rb.AddForce(GlobalOrientation.up * Stats.scale * Stats.JumpForce);
            
            
            Stats.Gforce = Stats.GforcePadrão/2;
            if(Keys.HoldingJump){
                 Stats.scale = 26;
            }else{
                 Stats.scale = 20;
            }
           
           

        }
       
        if (Keys.JumpHboost == true)
        {
                
            _rb.AddForce(GlobalOrientation.up * (Stats.scale * 5) * Stats.JumpForce);
            _rb.AddForce(_rb.velocity * Stats.JumpHAmount);
            Keys.JumpHboost = false;
        }
    }

    public void RollVBoost()
    {
        Keys.Jumping = false;
        if(Keys.RollVM){
            if(Keys.RunningJump == true){          
                _rb.AddForce(_rb.velocity * 160);     
                Keys.RunningJump = false;
            }
            
            _rb.AddForce(GlobalOrientation.up * Stats.JumpVAmount);
            
            Roll(_rb.velocity.magnitude / 100);
            Stats.RollScale = 2;
            Keys.RollVM = false;
        }
       
        
        if (Keys.rollVboost == true)
        {
            
            _rb.AddForce(GlobalOrientation.up * Stats.JumpVAmount);
               
            Roll(Stats.RollScale /= 1.4f);
            Anim._anim.speed += 0.05f;

            if(Stats.RollScale <= 0.6f){
                Keys.ActivateGrav = true;
            }
        }
        
         
    }

    public void RunMovement(){
        if(Keys.Running && Keys.Inground){          
            Stats.velocidadepadrao = Stats.velocidadepadraoRunning;
            Stats.dragPadrao = Stats.dragPadraoRun;   
            RunParticleEM.rateOverTime = 50;
            Anim._anim.speed = _rb.velocity.magnitude / 100;
        }
        if(!Keys.Running){  
            Stats.velocidadepadrao = Stats.velocidadepadraoWalking;
            Stats.dragPadrao = Stats.dragPadraoWalk; 
            RunParticleEM.rateOverTime = 0;
            
        }
        

        
       
        
    }

     public void RollHover()
    {
        
        Keys.ActivateGrav = false;
        if (Keys.rollVboost == true)
        {
            //_rb.AddForce(GlobalOrientation.up * JumpVAmount);
            
            Roll(3);
            Stats.IngroundCd = 0.3f;
        }
        
         
    }
    public void Roll(float Scale)
    {
       

        //horizontal e vertical

        if (Keys.RollDown == true)
        {
            _rb.AddForce(GlobalOrientation.forward * -Stats.RollingForce * Scale);
        }
        if (Keys.RollUp == true)
        {
            _rb.AddForce(GlobalOrientation.forward * Stats.RollingForce * Scale);
        }
        if (Keys.RollRight == true)
        {
            _rb.AddForce(GlobalOrientation.right * Stats.RollingForce * Scale);
        }
        if (Keys.RollLeft == true)
        {
            _rb.AddForce(GlobalOrientation.right * -Stats.RollingForce * Scale);
        }

        //diagonal

        if (Keys.RollDownLeft == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (-Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (-Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollUpLeft == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (-Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollDownRight == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (-Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollUpRight == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (Stats.RollingForce * Scale) / 1.1f);

        }

        // Porra((S) => { Console.WriteLine(S); });
    }





    public void FlushAnim(int State)
    {
        Keys.Rolling =
        Keys.RollDown =
        Keys.RollUp =
        Keys.RollLeft =
        Keys.RollRight =
        Keys.RollDownLeft =
        Keys.RollUpLeft =
        Keys.RollDownRight =
        Keys.RollUpRight = !(State == (int)groups.Roll);
    }


    public enum groups : int
    {
        Roll = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3

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
