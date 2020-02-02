using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public SpawnManager._Entity EntityType;
    public Transform target;
    public int maxChuncks;
    public bool move;
    public float elapsed, duration;
    public float destroyTime;

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
        SelfTerminate();
    }

    public virtual void Move()
    {

    }

    public virtual void SelfTerminate()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            Destroy(gameObject, destroyTime);
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CastleChunk"))
        {
            if (maxChuncks > 0)
            {
                maxChuncks--;
                other.GetComponent<CastleChunk>().Destroy();
            }
        }
    }
}
