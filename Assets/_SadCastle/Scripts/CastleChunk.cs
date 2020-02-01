using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleChunk : MonoBehaviour
{
    [SerializeField] bool isDestroyed = false;

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
        if (isDestroyed && Input.GetMouseButton(0)) 
        {
            Repair();
        }

        // For testing
        else if (!isDestroyed && Input.GetMouseButton(1)) 
        {
            Destroy();
        }
    }

    public void Destroy() 
    {
        isDestroyed = true;
        mesh.enabled = false;
    }

    void Repair()
    {
        isDestroyed = false;
        mesh.enabled = true;
    }
}
