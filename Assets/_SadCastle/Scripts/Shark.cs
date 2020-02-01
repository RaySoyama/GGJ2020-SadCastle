using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Entity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        start = transform.position;
        mid = transform.forward * 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
        }
    }

    public override void Move()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Bezier.QuadraticBezier(start, mid, target.position, (elapsed / duration) >= 1 ? 1 : elapsed / duration);
        }
    }
}
