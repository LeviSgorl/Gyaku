using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLighManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sprites = GameObject.FindObjectsOfType<SpriteRenderer>();
        foreach(SpriteRenderer Sp in Sprites){
            SpriteLight Has;
            Sp.TryGetComponent<SpriteLight>(out Has);
            if(Has == null && Sp.gameObject.tag != "Hud"){
                 Sp.gameObject.AddComponent<SpriteLight>();
            } 
        }
    }
    public SpriteRenderer[] Sprites;
    // Update is called once per frame
    void Update()
    {
        
    }
}
