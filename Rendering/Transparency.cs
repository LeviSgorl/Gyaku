using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    // Start is called before the first frame update


    private SpriteRenderer Sprite;
    public ImageFlip Transp;
    public GameObject Effect;
    public GameObject Effect2;
    public GameObject Prefab;
    
    public bool Death;
    
    void Awake()
    {
        Prep();
    }

    // Update is called once per frame
    void Update()
    {
        DeathLogic();
    }




    public void Prep()
    {

        Sprite = GetComponent<SpriteRenderer>();
        Sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        Prefab = Instantiate(Effect, gameObject.transform);
        Prefab.transform.localPosition = new Vector3(0, 0, 0);
        Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Transp = Prefab.GetComponent<ImageFlip>();
        Prefab.SetActive(true);
    } 

    public void DeathLogic()
    {
        if (Death == true)
        {
            DeathSequence();
            Death = false;
        }
        if (Transp.i < -1)
        {
            Sprite.maskInteraction = SpriteMaskInteraction.None;
            Destroy(Prefab);
        }
        if (Transp.i > Transp.Steps.Length - 1)
        {
            Destroy(gameObject);
        }
        //makes check if object is dead/destroyed, play the effect and then send order to destroy the object
    }
    void DeathSequence()
    {
        Sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        Prefab = Instantiate(Effect2, gameObject.transform);
        Prefab.transform.localPosition = new Vector3(0, 0, 0);
        Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Transp = Prefab.GetComponent<ImageFlip>();
        Prefab.SetActive(true);
    }
}
