using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum _Entity { Kid, Volleyball };

    [System.Serializable]
    public class SpawnType
    {
        public _Entity name;
        public GameObject entityPrefab;
        public Transform[] spawnPositions;
    }

    [Header("Spawn Types")]
    public SpawnType[] spawnTypes;

    public Transform[] targets;
    [Header("Spawn Cooldown")]
    public bool autoSpawn;
    public float spawnTimer;
    public float cooldownDuration;

    public static SpawnManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is more than one instance of SpawnManager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpawn && spawnTimer > cooldownDuration)
        {
            SpawnEntity();
        }
        else if (spawnTimer < cooldownDuration)
        {
            spawnTimer += Time.deltaTime;
        }
    }

    public void SpawnEntity()
    {
        int randInd = Random.Range(0, spawnTypes.Length);
        int randPos = Random.Range(0, spawnTypes[randInd].spawnPositions.Length);
        spawnTimer = 0;

        Entity currentEntity = Instantiate(spawnTypes[randInd].entityPrefab, spawnTypes[randInd].spawnPositions[randPos].position, 
            spawnTypes[randInd].spawnPositions[randPos].rotation).GetComponent<Entity>();
        currentEntity.target = targets[Random.Range(0, targets.Length)];
    }

    public void SpawnEntity(_Entity entity)
    {
        SpawnType currentSpawnType = GetEntity(entity);
        int randPos = Random.Range(0, currentSpawnType.spawnPositions.Length);

        Entity currentEntity = Instantiate(currentSpawnType.entityPrefab, currentSpawnType.spawnPositions[randPos].position,
            currentSpawnType.spawnPositions[randPos].rotation).GetComponent<Entity>();
        currentEntity.target = targets[Random.Range(0, targets.Length)];
    }

    public SpawnType GetEntity(_Entity name)
    {
        for (int i = 0; i < spawnTypes.Length; i++)
        {
            if (spawnTypes[i].name == name)
            {
                return spawnTypes[i];
            }
        }
        return null;
    }
}
