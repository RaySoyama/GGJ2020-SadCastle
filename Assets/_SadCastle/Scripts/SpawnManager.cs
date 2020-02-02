using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum _Entity 
    {   
        Kid, 
        Beachball,
        NoLitter,
        Beer,
        Coconut,
        Lightning,
        Volleyball,
        Weed,
        Umbrella,
        Wave,
        Tide,
        Meteor,
        Shark
    };

    [System.Serializable]
    public class SpawnType
    {
        public GameObject entityPrefab;
        public Transform[] spawnPositions;
    }

    public Castle castle;

    [Header("Spawn Types")]
    public SpawnType[] spawnTypes;

    [Header("Spawn Cooldown")]
    public bool autoSpawn;
    public float cooldownDuration;
    float spawnTimer;
    [SerializeField]
    float lastHealthyChunkTimer;

    public Entity lightning;

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
        if (cooldownDuration < 2.5f)
            cooldownDuration = 2.5f;
        if (Kernel.instance.knownHealthyCastleChunks <= 1)
        {
            lastHealthyChunkTimer += Time.deltaTime;

            if (lastHealthyChunkTimer > 6)
            {
                lightning.Move();
            }
        }
        else
        {
            lastHealthyChunkTimer = 0;
        }
    }

    public void SpawnEntity()
    {
        int randInd = Random.Range(0, spawnTypes.Length);
        int randPos = Random.Range(0, spawnTypes[randInd].spawnPositions.Length);
        spawnTimer = 0;
        cooldownDuration -= 0.5f;

        Entity currentEntity = Instantiate(spawnTypes[randInd].entityPrefab, spawnTypes[randInd].spawnPositions[randPos].position, 
            spawnTypes[randInd].spawnPositions[randPos].rotation).GetComponent<Entity>();
        currentEntity.target = castle.GetChunk(Random.Range(0, 3), Random.Range(0, 3)).transform;
    }

    public void SpawnEntity(_Entity entity)
    {
        SpawnType currentSpawnType = GetEntity(entity);

        if (currentSpawnType == null)
        {
            Debug.LogError($"Entity {entity.ToString()} does not exsist ");
            return;
        }

        int randPos = Random.Range(0, currentSpawnType.spawnPositions.Length);

        Entity currentEntity = Instantiate(currentSpawnType.entityPrefab, currentSpawnType.spawnPositions[randPos].position,
            currentSpawnType.spawnPositions[randPos].rotation).GetComponent<Entity>();
        currentEntity.target = castle.GetChunk(Random.Range(0, 3), Random.Range(0, 3)).transform;
    }

    public SpawnType GetEntity(_Entity name)
    {
        for (int i = 0; i < spawnTypes.Length; i++)
        {
            if (spawnTypes[i].entityPrefab.GetComponent<Entity>().EntityType == name)
            {
                return spawnTypes[i];
            }
        }
        return null;
    }
}
