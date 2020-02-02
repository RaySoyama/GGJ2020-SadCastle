using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateMove : Entity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            transform.position = Vector3.Lerp(start, target.position, (elapsed / duration) >= 1 ? 1 : elapsed / duration);
            transform.LookAt(target.position, Vector3.up);
        }
    }
}
