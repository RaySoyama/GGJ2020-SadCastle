using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateMove : Entity
{
    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(start, target.position, Time.deltaTime);
        }
    }
}
