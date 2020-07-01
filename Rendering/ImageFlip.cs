using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFlip : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteMask Mask;
    SpriteRenderer[] Sprite;
    public Sprite[] Steps;
    [Range(0,56)]
    public int i;
    public float timersteppadrao;
    float timerstep;
    public bool Reverse;

    void Awake()
    {
        Mask = gameObject.GetComponent<SpriteMask>();
        Sprite = transform.root.GetComponentsInChildren<SpriteRenderer>();;
        Order();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpriteSequence();
        TransparencySequence();
    }
    public void TransparencySequence(){
        
        float Range = i / Steps.Length;
        Color colorShadow =  new Color(1,1,1,Range);
        foreach(SpriteRenderer spr in Sprite){
        Color color = spr.color;
        spr.color = Vector4.Lerp(color,colorShadow ,0 + Time.deltaTime * 5);
        }
       
    }

    public void SpriteSequence()
    {
        timerstep -= Time.deltaTime;
        if (i >= 0 && i <= Steps.Length - 1)
        {
            Mask.sprite = Steps[i];
        }
        if (Reverse == false)
        {
            if (timerstep < 0)
            {

                i += 1;

                timerstep = timersteppadrao;
            }
        }
        else
        {
            if (timerstep < 0)
            {

                i -= 1;

                timerstep = timersteppadrao;
            }
        }
    }
    public void Order()
    {
        if (Reverse == false)
        {
            i = 0;
        }
        else
        {
            i = Steps.Length - 1;
        }

        timersteppadrao = 0.001f;
    }
}
