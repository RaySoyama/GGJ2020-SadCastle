using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static AudioClipHelper;

public class CastleChunk : MonoBehaviour
{
    int rowNumber;
    int columnNumber;
    [SerializeField] bool isDestroyed = false;
    [SerializeField] Player player;

    [SerializeField] Mesh builtMesh;
    [SerializeField] Mesh destroyedMesh;
    [SerializeField] Mesh destroyedWithAdjacent1IntactMesh;
    [SerializeField] Mesh destroyedWithAdjacent2IntactMesh;
    [SerializeField] Mesh destroyedWithAdjacentsIntactMesh;
    [SerializeField] CastleChunk adjacentChunk1;
    [SerializeField] CastleChunk adjacentChunk2;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    Castle castle;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] destroySounds;
    private static AudioClip lastDestroySound;

    [SerializeField]
    private AudioClip[] repairSounds;
    public static AudioClip lastRepairSound;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshFilter = GetComponentInChildren<MeshFilter>();
        castle = GetComponentInParent<Castle>();
    }

    void Start()
    {
        if(audioSource == null)
        {
            Debug.LogWarning("AudioSource is missing, attempting to locate one...", this);
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void SetRowAndColumnNumbers(int row, int column)
    {
        rowNumber = row;
        columnNumber = column;
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
        isDestroyed = true;
        // meshRenderer.enabled = false;
        // meshFilter.mesh = destroyedMesh;
        castle.UpdateMeshes();

        lastDestroySound = GRCETOUOO(destroySounds, lastDestroySound);
        audioSource.PlayOneShot(lastDestroySound);
    }

    public void Repair()
    {
        if (!isDestroyed) { return; }

        isDestroyed = false;
        // meshFilter.mesh = builtMesh;
        castle.UpdateMeshes();
        // meshRenderer.enabled = true;

        lastRepairSound = GRCETOUOO(repairSounds, lastRepairSound);
        audioSource.PlayOneShot(lastRepairSound);
    }

    public bool CanRepair()
    {
        return isDestroyed;
    }

    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateMesh()
    {
        if (!CanRepair()) {
            meshFilter.mesh = builtMesh;
        }
        else if (!adjacentChunk1 || !adjacentChunk2) {
            meshFilter.mesh = destroyedMesh;
        }
         else if (adjacentChunk1.CanRepair() && adjacentChunk2.CanRepair()) {
            meshFilter.mesh = destroyedMesh;
        }
        else if (adjacentChunk1.CanRepair() && !adjacentChunk2.CanRepair()) {
            meshFilter.mesh = destroyedWithAdjacent2IntactMesh;
        }
        else if (!adjacentChunk1.CanRepair() && adjacentChunk2.CanRepair()) {
            meshFilter.mesh = destroyedWithAdjacent1IntactMesh;
        }
        else if (!adjacentChunk1.CanRepair() && !adjacentChunk2.CanRepair()) {
            meshFilter.mesh = destroyedWithAdjacentsIntactMesh;
        }
    }
}