using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMachine : MonoBehaviour
{
    public AnimationClip clip;
    public float time;
    void Update()
    {
        time = Time.timeSinceLevelLoad;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBeachball();
            AnimationEvent evt = new AnimationEvent() { functionName = "SpawnBeachball", time = Time.timeSinceLevelLoad};
            clip.AddEvent(evt);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnVolleyball();
            AnimationEvent evt = new AnimationEvent() { functionName = "SpawnVolleyball", time = Time.timeSinceLevelLoad };
            clip.AddEvent(evt);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnKid();
            AnimationEvent evt = new AnimationEvent() { functionName = "SpawnKid", time = Time.timeSinceLevelLoad };
            clip.AddEvent(evt);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnShark();
            AnimationEvent evt = new AnimationEvent() { functionName = "SpawnShark", time = Time.timeSinceLevelLoad };
            clip.AddEvent(evt);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnLightning();
            AnimationEvent evt = new AnimationEvent() { functionName = "SpawnLightning", time = Time.timeSinceLevelLoad };
            clip.AddEvent(evt);
        }


    }


    public void SpawnBeachball()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Beachball);
    }
    public void SpawnVolleyball()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Volleyball);
    }
    public void SpawnKid()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Kid);
    }

    public void SpawnShark()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Shark);
    }

    public void SpawnLightning()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Lightning);
    }
}