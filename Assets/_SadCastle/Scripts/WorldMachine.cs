using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMachine : MonoBehaviour
{
    public void SpawnBeachball()
    {
        SpawnManager.instance.SpawnEntity(SpawnManager._Entity.Beachball);
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