using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetector : ItemDetector
{
     public GameObject Effect;
     private GameObject Icon;
     public bool showIcon;
  
   
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        if(Effect != null){
        Icon = Instantiate(Effect,null) as GameObject;
        }
        Target = "Grabable";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(showIcon == true) {
        EffectPosition();
        }else{
            Icon.SetActive(false);
        }
        LerpFactor = Mathf.Clamp(LerpFactor,0,1);
    }




public void RicochetEffect(){
    LerpFactor += 0.04f;
         if(Locked != null){
            Icon.transform.position = Vector3.Lerp(Icon.transform.position, Locked.transform.position, LerpFactor);
            Icon.SetActive(true);
        }
        if(Locked == gameObject && LerpFactor == 1){
            Icon.SetActive(false);
        }
}

    public void EffectPosition(){
         if(Locked != null){      
            Bounds closet = Locked.GetComponent<Collider>().bounds;
            Vector3 Point = closet.max;
            float distance = Vector3.Distance(Locked.transform.position,Point);
            Vector3 Ofset = Locked.transform.position + new Vector3(0,0,-distance + 3);
            Icon.transform.position = Ofset;
            Icon.SetActive(true);
        }else{
            
            Icon.SetActive(false);
        }
        
    }


}
