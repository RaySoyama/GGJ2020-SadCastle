using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleChunk : MonoBehaviour
{
    [SerializeField] bool isDestroyed = false;
    [SerializeField] Player player;

    [SerializeField] Mesh builtMesh;
    [SerializeField] Mesh destroyedMesh;

    bool wasDestroyed = false;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] destroySounds;

    [SerializeField]
    private AudioClip[] repairSounds;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    void Start()
    {
        if(audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing, attempting to locate one...", this);
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnMouseOver()
    {
        if (isDestroyed && Input.GetMouseButtonDown(0)) 
        {
            player.MoveTowardsChunk(this);
            // Repair();
        }

        // For testing
        else if (!isDestroyed && Input.GetMouseButtonDown(1)) 
        {
            Destroy();
        }
    }

    public void Destroy() 
    {
        if (isDestroyed) { return; }

        isDestroyed = true;
        // meshRenderer.enabled = false;
        meshFilter.mesh = destroyedMesh;

        audioSource.PlayOneShot(destroySounds[Random.Range(0, destroySounds.Length)]);
    }

    public void Repair()
    {
        if (!isDestroyed) { return; }

        isDestroyed = false;
        meshFilter.mesh = builtMesh;
        // meshRenderer.enabled = true;

        audioSource.PlayOneShot(repairSounds[Random.Range(0, repairSounds.Length)]);
    }

    public bool CanRepair()
    {
        return isDestroyed;
    }

    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
