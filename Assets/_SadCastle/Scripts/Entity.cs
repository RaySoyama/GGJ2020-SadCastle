using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public SpawnManager._Entity EntityType;
    public Transform target;
    public int maxChuncks;
    public bool move;
    public float elapsed, duration;
    public float destroyTime;

    protected Vector3 start, mid;

    public float elapsedNormalized
    {
        get
        {
            return elapsed / duration;
        }
    }

    public UnityEvent OnSpawn;
    public UnityEvent OnMid;
    private bool onMidProcessed = false;
    public UnityEvent OnEnd;
    private bool onEndProcessed = false;

    public AudioClip onSpawnAudio;
    public AudioClip onMidAudio;
    public AudioClip onEndAudio;

    public AudioSource audioSource;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        start = transform.position;

        OnSpawn?.Invoke();
        audioSource.PlayOneShot(onSpawnAudio);
    }

    protected virtual void Update()
    {
        if (move)
        {
            Move();
        }
        SelfTerminate();
        if (!onMidProcessed && elapsedNormalized >= 0.5f)
        {
            onMidProcessed = true;
            OnMid?.Invoke();
            audioSource.PlayOneShot(onMidAudio);
        }
        else if (!onEndProcessed && elapsed >= duration)
        {
            onEndProcessed = true;
            OnEnd?.Invoke();
            audioSource.PlayOneShot(onEndAudio);
        }
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
