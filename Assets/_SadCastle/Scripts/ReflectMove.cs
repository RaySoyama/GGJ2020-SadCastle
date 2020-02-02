using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Beach Ball behavior. Spawn positions should be high above the level
public class ReflectMove : Entity
{
    public float endDistance;
    public float height;
    Vector3 end;
    // Start is called before the first frame update
    protected override void Start()
    {
        start = transform.position;
        end = (target.position + (target.position - start) + Vector3.up * height) * endDistance;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void Move()
    {
        if (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(start, target.position, elapsed / duration * 2);
        }
        else if (elapsed > duration / 2 && elapsed < duration)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(target.position, end, (elapsed / duration) - duration / 2);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(start, 0.3f);
        Gizmos.DrawSphere(target.position, 0.3f);
        Gizmos.DrawSphere(end, 0.3f);
    }
}
