using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public int numberOfRepairAnimations = 2;

    public void HandleChunkRepair(CastleChunk repairedChunk)
    {
        animator.SetTrigger("DoRepair");
        animator.SetInteger("NextRepairType", Random.Range(0, numberOfRepairAnimations));
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Reset()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
}
