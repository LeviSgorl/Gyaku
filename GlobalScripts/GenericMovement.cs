using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _rb;
    [HideInInspector]
    public Transform GlobalOrientation;
    [HideInInspector]
   
    public GenericInput Keys;
    [HideInInspector]
    public GenericStats Stats;
    [HideInInspector]
    public GenericAnimator Anim;
    
    private GameObject Effect;
    public GameObject BounceEF;
    [HideInInspector]
    public int i;

   
    [HideInInspector]
    private bool gotonext;

    [HideInInspector]
    public Vector3 HoldposOffset;
    public GrabDetector ItemD;
    public float Bounces;
    public bool VBounce;
    
    public Collision MainCol;
    public Vector3 LookDirIns;
    public float BounceForceV;
    public float BounceForceH;
    public float HoldDistance;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Keys = gameObject.GetComponent<GenericInput>();
        Stats = gameObject.GetComponent<GenericStats>();
        Anim = gameObject.GetComponent<GenericAnimator>();
        _rb = gameObject.GetComponent<Rigidbody>();
        ItemD = gameObject.GetComponentInChildren<GrabDetector>();
        GlobalOrientation = GameObject.Find("GlobalOri").transform;
        
        Keys.ActivateGrav = true;
        Stats.Friction = 1;
        Stats.GravScale = 1;
       
    }
    protected virtual void FixedUpdate()
    {
        VelocityCheck();
        Gravity();
        IngroundLogic();
        MassMath();
        VBounceDrag();
    }
 
    public void VBounceDrag(){
        
        if(VBounce == false && Keys.Inground && !Keys.Running){
            Stats.dragPadrao =  Stats.dragPadraoWalk;
        }
        if(VBounce){
            Stats.dragPadrao = Stats.dragPadraoKnocked;
        }
    
    }
    public void Bounce(){
        
        if (VBounce)
        {
        Stats.OnControl = false;
        
        //_rb.AddForce((_rb.velocity * BounceForceH);
        _rb.AddForce((MainCol.contacts[0].normal) * (BounceForceV));
        _rb.AddRelativeTorque(transform.forward * 90000);
        BounceForceV /= 1.6f;
        BounceForceH /= 1.2f;
        
        
        
        Bounces--;
        }
        if(_rb.velocity.magnitude < 500)
        {
            Stats.OnControl = true;
            VBounce = false;
            BounceForceV = (_rb.mass * 32000)+Stats.Gforce;
            BounceForceH = 100;
            
        }
       
    }
    

    public void MassMath(){
            Stats.GforcePadrão = _rb.mass * 4000 * Stats.GravScale;
            Stats.JumpVAmount = (_rb.mass * 6000) - Stats.Gforce;
            Stats.JumpForce = _rb.mass * 80;
    }
    private void OnCollisionStay(Collision collision)
    {
        
            if (i < collision.contactCount && Stats.velocityMagEnter >= 50 )
            {
                
               
                    GroundPound(collision);
                    MainCol = collision;
            }
           
       

    }

    private void OnCollisionEnter(Collision collision)
    {
        i = 0;
        Stats.velocityMagEnter = Stats.velocityMag;
        if(Stats.Friction != 0){
            _rb.velocity = _rb.velocity / Stats.Friction;
        }
       
 Bounce();
        // GroundPound(collision);
    }

    void GroundPound(Collision Col)
    {
        if (gotonext == false) { 
            Effect = Instantiate(BounceEF, null, true);
            Effect.GetComponent<AudioSource>().volume = Stats.velocityMag /1000;
            Effect.transform.position = Col.contacts[i].point;
            Effect.transform.up = Col.contacts[i].normal;
            Effect.GetComponent<ParticleSystem>().maxParticles = (int)Stats.velocityMag / 10;
            Effect.gameObject.SetActive(true);
            Debug.Log(transform.root.name + " Hitted " + Col.contacts[i].otherCollider.name + " and casted " + Effect.GetComponent<ParticleSystem>().maxParticles +" particles");
            
            Destroy(Effect, 1f);
            
            gotonext = true;
        }
        if (Time.frameCount % 1 == 0)
        {
            i++;
            gotonext = false;
            
        }
    }
    void VelocityCheck()
    {
        Stats.groundpoundcd -= Time.deltaTime;
        if (_rb.velocity.magnitude > 1)
        {
            Stats.groundpoundcd = 0.2f;
        }
        if (Time.frameCount % 5 == 0 && _rb.velocity.magnitude > 1 && Stats.groundpoundcd > 0)
        {
            Stats.velocityMag = _rb.velocity.magnitude;
        }
        if (Stats.groundpoundcd < 0)
        {
            Stats.velocityMag = 0;
        }
    }

    void Gravity()
    {
        _rb.AddForce(GameObject.Find("GlobalOri").transform.up * -Stats.Gforce);
    }

    void IngroundLogic(){
        if (Keys.Inground)
        {
            Stats.IngroundCd = 0.05f;
            _rb.drag = Stats.dragPadrao;
        }
        if (VBounce)
        {
            Stats.IngroundCd = 0.05f;
            _rb.drag = Stats.dragPadraoKnocked;
        }
        if (Keys.ActivateGrav)
        {
            if(Stats.Gforce < Stats.GforcePadrão){
            Stats.Gforce += _rb.mass * 800;
            }   
        }

    }
}
