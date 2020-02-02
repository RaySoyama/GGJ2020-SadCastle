using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    CastleChunk currentChunk;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentChunk)
            {
                currentChunk.Repair();
            }
        }
    }

    public void MoveTowardsChunk(CastleChunk chunk)
    {
        currentChunk = chunk;

        Vector3 chunkPosition = chunk.transform.position;
        // transform.position = chunkPosition;
        agent.SetDestination(chunkPosition);
    }

    
}
