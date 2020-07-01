using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnima : GenericAnimator
{
    GenericStats Playerkeys;
    // Start is called before the first frame update
    protected override void Start()
    {
        Playerkeys = gameObject.GetComponent<GenericStats>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _anim.SetFloat("movimento", Stats.PressedKeys);
    }
}
