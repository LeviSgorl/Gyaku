using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  [ExecuteInEditMode]
 public class SpriteLight : MonoBehaviour {

     
     public SpriteRenderer Sprite;

     public bool DarkenSprite;
     public Color color;
      public Color colorShadow;
     Vector3 posAbs;
     public Light lights;

     public float inView;

     public Vector3 dir;
     public Vector3 LightPos;
     int i;
     int i2;
     public Collider col;
     public Vector3 Offset;
     public int leng;
     RaycastHit[] hit;
     public float DarkeningScale;

    void Awake(){
         posAbs = transform.position;
         Sprite = gameObject.GetComponent<SpriteRenderer>();
         
         lights = GameObject.Find("Sun").GetComponent<Light>();
         Offset = new Vector3(0,0,-0.1f);
         
    }
    void FixedUpdate()
    {   
        GetLights();         
        TestInView(dir, 5000f);
        color = Sprite.color;
        colorShadow =  new Color(DarkeningScale,DarkeningScale,DarkeningScale,1);
        DarkeningScale = 0.5f;
        if(DarkenSprite){
            Sprite.color = Vector4.Lerp(color,colorShadow ,0 + Time.deltaTime * 5);
            
        }else{
           Sprite.color = Vector4.Lerp(color,Color.white,0 + Time.deltaTime * 5);;
        }
    }

        public void GetLights() {
           
        dir = lights.transform.forward * -1f;
        //LightPos = lights[1].transform.position
            
        }
        void TestInView(Vector3 dir, float dist) {
            
            
            posAbs = transform.position + Offset;
            hit = Physics.RaycastAll(posAbs, dir, dist,1, QueryTriggerInteraction.Ignore);
            leng = hit.Length;
            foreach(RaycastHit H in hit){
            if(H.collider.gameObject == transform.root.gameObject){
                Debug.DrawRay(posAbs, dir.normalized * dist, Color.green);
                
            }else{
                Debug.DrawRay(posAbs, dir.normalized * H.distance, Color.red);
                DarkenSprite = true;
               // DarkeningScale = H.distance/1100;
            }
            }
            if(hit.Length == 0) {
                Debug.DrawRay(posAbs, dir.normalized * dist, Color.green); DarkenSprite = false;
            }
        
            
            
        }

 }