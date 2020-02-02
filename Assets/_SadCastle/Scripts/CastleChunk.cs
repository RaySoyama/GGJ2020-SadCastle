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

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
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
        meshFilter.mesh = destroyedMesh;
    }

    public void Repair()
    {
        isDestroyed = false;
        meshFilter.mesh = builtMesh;
        // meshRenderer.enabled = true;
    }

    public bool CanRepair()
    {
        return isDestroyed;
    }
}
