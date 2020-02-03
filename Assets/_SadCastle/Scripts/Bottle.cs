using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Entity
{
    public float height;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        start = transform.position;
        mid = Vector3.Lerp(target.position, transform.position, 0.5f) + Vector3.up * height;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }

    public override void Move()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Curve.QuadraticBezier(start, mid, target.position, (elapsed / duration) >= 1 ? 1 : elapsed / duration);
        }
    }
}
