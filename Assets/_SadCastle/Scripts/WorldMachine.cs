using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMachine : MonoBehaviour
{
    [System.Serializable]
    struct SpawnTimerObject
    {
        public float InGameTime;
        //enum      
    }

    [SerializeField]
    private List<SpawnTimerObject> SpawnTimeline = new List<SpawnTimerObject>();


    [ReadOnlyField] [SerializeField]
    private float time = 0;

    


    void Start()
    {
   

    }

    void Update()
    {
        time += Time.deltaTime;

        if (SpawnTimeline.Count != 0 && time >= SpawnTimeline[0].InGameTime)
        {
            //Call Marcel script
            Debug.Log("Spawned Object");
            SpawnTimeline.RemoveAt(0);
        }


    }
}
