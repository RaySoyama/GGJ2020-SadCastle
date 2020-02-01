using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMove : Entity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        start = transform.position;
        mid = (target.position - transform.position).normalized / 2 + Vector3.up * 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
