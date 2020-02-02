using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    CastleChunk currentChunk;

    [FormerlySerializedAs("OnChunkRepaired")]
    public UnityEventCastleChunk OnChunkRepairStart;

    [Tooltip("Number of seconds to wait before actually repairing the chunk (this should ideally be as long as the animation)")]
    public float repairDelay = 0.5f;

    List<CastleChunk> chunksInProgress = new List<CastleChunk>();

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentChunk && currentChunk.CanRepair() && !chunksInProgress.Contains(currentChunk))
            {
                chunksInProgress.Add(currentChunk);

                // broadcast that repair job has begun
                OnChunkRepairStart?.Invoke(currentChunk);

                // look at chunk being repaired
                Vector3 lookChunk = currentChunk.transform.position;
                lookChunk.y = transform.position.y;
                agent.transform.DOKill();
                agent.transform.DOLookAt(lookChunk, 1.0f);

                // queue repair
                DOVirtual.DelayedCall(repairDelay, () => FinalizeChunkRepair(currentChunk));
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

    private void FinalizeChunkRepair(CastleChunk chunk)
    {
        chunksInProgress.Remove(chunk);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (chunk && chunk.CanRepair())
            {
                currentChunk.Repair();
            }
        }
    }
}

[System.Serializable]
public class UnityEventCastleChunk : UnityEvent<CastleChunk> { }