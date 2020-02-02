using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public static Kernel instance;

    void Start()
    {
        if (instance != null) { Destroy(this); Debug.LogWarning("Duplicate kernal detected. Destroying...", gameObject); return; }
        instance = this;
    }

    [SerializeField]
    Castle castle;

    public int knownCastleChunks { get; private set; }
    public int knownHealthyCastleChunks { get; private set; }
    public int knownDamagedCastleChunks { get { return knownCastleChunks - knownHealthyCastleChunks; } }

    void FixedUpdate()
    {
        knownCastleChunks = castle.totalCastleChunks;
        knownHealthyCastleChunks = castle.totalHealthyCastleChunks;
    }
}
