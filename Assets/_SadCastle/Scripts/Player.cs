using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    CastleChunk currentChunk;

    public UnityEventCastleChunk OnChunkRepaired;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentChunk && currentChunk.CanRepair())
            {
                OnChunkRepaired?.Invoke(currentChunk);
                currentChunk.Repair();
            }
        }
    }

    public void MoveTowardsChunk(CastleChunk chunk)
    {
        currentChunk = chunk;

        Vector3 chunkPosition = chunk.transform.position;
        Debug.Log(chunkPosition.ToString("F20"));
        agent.SetDestination(chunkPosition);
    }

    
}

[System.Serializable]
public class UnityEventCastleChunk : UnityEvent<CastleChunk> { }