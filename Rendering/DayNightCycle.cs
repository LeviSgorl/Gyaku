using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public float ro;
     [HideInInspector]
    public float ro2;
    public float CycleSpeed;

    public float CurrentTime;
    private Rigidbody _rb;
    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        CycleSpeed = 0.0015f;
        CurrentTime = -8;
    }
    
    // Update is called once per frame
    void Update()
    {
        CurrentTime = Mathf.Clamp(CurrentTime,-12,12);
        
        setTime(CurrentTime);
        
        CurrentTime += CycleSpeed;
        if(CurrentTime >= 12) CurrentTime = -CurrentTime*2;
    }

    public void setTime(float Hour){
        transform.rotation = Quaternion.AngleAxis( (Hour * 15),Vector3.back) * Quaternion.AngleAxis( (-50) * Mathf.Abs(Hour)/12,Vector3.right);     
        

    }
}
