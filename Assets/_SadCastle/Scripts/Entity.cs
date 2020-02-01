using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Transform target;
    public bool move;
    public float elapsed, duration;

    protected Vector3 start, mid;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        start = transform.position;
    }

    protected virtual void Update()
    {
        if (move)
        {
            Move();
        }
    }

    public virtual void Move()
    {

    }

    public virtual void SelfTerminate()
    {

    }
}
