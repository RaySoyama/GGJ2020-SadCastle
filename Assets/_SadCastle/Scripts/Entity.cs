using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using static AudioClipHelper;

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

    public AudioClip[] onSpawnAudio;
    private static AudioClip lastSpawnAudio;
    public AudioClip[] onMidAudio;
    private static AudioClip lastMidAudio;
    public AudioClip[] onEndAudio;
    private static AudioClip lastEndAudio;

    public AudioSource audioSource;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        start = transform.position;

        OnSpawn?.Invoke();
        if(audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing. Attempting to add one...");
            audioSource = GetComponent<AudioSource>();
            if(audioSource == null) { audioSource = gameObject.AddComponent<AudioSource>(); }
        }

        lastSpawnAudio = GRCETOUOO(onSpawnAudio, lastSpawnAudio);
        audioSource.PlayOneShot(lastSpawnAudio);
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
            lastMidAudio = GRCETOUOO(onMidAudio, lastMidAudio);
            audioSource.PlayOneShot(lastMidAudio);
        }
        else if (!onEndProcessed && elapsed >= duration)
        {
            onEndProcessed = true;
            OnEnd?.Invoke();
            lastEndAudio = GRCETOUOO(onEndAudio, lastEndAudio);
            audioSource.PlayOneShot(lastEndAudio);
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

public static class AudioClipHelper
{
    public static AudioClip GRCETOUOO(AudioClip[] clips, AudioClip avoid = null)
    {
        return GetRandomClipExceptThisOneUnlessOnlyOne(clips, avoid);
    }
    public static AudioClip GetRandomClipExceptThisOneUnlessOnlyOne(AudioClip[] clips, AudioClip avoid)
    {
        if (clips == null || clips.Length == 0) { return null; }
        if (clips.Length == 1) { return clips[0]; }

        int finalIndex = Random.Range(0, clips.Length);

        int avoidIndex = -1;
        if (avoid != null)
        {
            for (int i = 0; i < clips.Length; ++i)
            {
                if (clips[i] == avoid) { avoidIndex = i; break; }
            }
        }

        if(avoidIndex != -1)
        {
            int offset = Random.Range(0, clips.Length - 1);
            finalIndex = (avoidIndex + offset) % clips.Length;
        }

        return clips[finalIndex];

    }
}
