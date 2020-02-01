using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMove : Entity
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    protected override void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        start = transform.position;
        mid = Vector3.Lerp(target.position, transform.position, 0.5f) + Vector3.up * 3;

        if (transform.position.x < target.position.x)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }
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
