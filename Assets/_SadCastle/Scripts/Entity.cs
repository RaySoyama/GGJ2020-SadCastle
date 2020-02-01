using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector3 target;
    public bool move;

    Vector3 start, mid;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        mid = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Bezier.QuadraticBezier(start, mid, target, Time.deltaTime);
        }
    }
}
