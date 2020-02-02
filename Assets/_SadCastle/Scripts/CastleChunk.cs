using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleChunk : MonoBehaviour
{
    [SerializeField] bool isDestroyed = false;
    [SerializeField] Player player;

    bool wasDestroyed = false;

    MeshRenderer mesh;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    void OnMouseOver()
    {
        Debug.Log(Input.mousePosition);
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
        mesh.enabled = false;
    }

    public void Repair()
    {
        isDestroyed = false;
        mesh.enabled = true;
    }

    public bool CanRepair()
    {
        return isDestroyed;
    }
}
