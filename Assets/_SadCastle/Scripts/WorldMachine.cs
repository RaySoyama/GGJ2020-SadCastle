using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMachine : MonoBehaviour
{
    [System.Serializable]
    struct SpawnTimerObject
    {
        public float InGameTime;
        public SpawnManager._Entity entityName;
    }

    SpawnManager spawnManager;

    [SerializeField]
    private List<SpawnTimerObject> SpawnTimeline = new List<SpawnTimerObject>();


    [ReadOnlyField] [SerializeField]
    private float time = 0;

    


    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (SpawnTimeline.Count != 0 && time >= SpawnTimeline[0].InGameTime)
        {
            spawnManager.SpawnEntity(); // there is an overload with a SpawnManager._Entity parameter
            Debug.Log("Spawned Object");
            SpawnTimeline.RemoveAt(0);
        }
    }
}
