using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Entity
{
    Animator anim;
    // Start is called before the first frame update
    protected override void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
    public override void Move()
    {
        anim.SetTrigger("DoLightning");
    }
    
}
