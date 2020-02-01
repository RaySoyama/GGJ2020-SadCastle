using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnType
    {
        public GameObject entityPrefab;
        public Transform[] spawnPositions;
    }

    [Header("Spawn Types")]
    public SpawnType[] spawnTypes;

    public Transform[] targets;
    [Header("Spawn Cooldown")]
    public float spawnTimer;
    public float cooldownDuration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > cooldownDuration)
        {
            SpawnEntity();
        }
    }

    public void SpawnEntity()
    {
        int randInd = Random.Range(0, spawnTypes.Length);
        int randPos = Random.Range(0, spawnTypes[randInd].spawnPositions.Length);
        spawnTimer = 0;

        Entity currentEntity = Instantiate(spawnTypes[randInd].entityPrefab, spawnTypes[randInd].spawnPositions[randPos]).GetComponent<Entity>();
        currentEntity.target = targets[Random.Range(0, targets.Length)];
    }

    public void SpawnEntity(SpawnType entity)
    {
        int randInd = Random.Range(0, spawnTypes.Length);
        int randPos = Random.Range(0, spawnTypes[randInd].spawnPositions.Length);
        spawnTimer = 0;

        Entity currentEntity = Instantiate(spawnTypes[randInd].entityPrefab, spawnTypes[randInd].spawnPositions[randPos]).GetComponent<Entity>();
        currentEntity.target = targets[Random.Range(0, targets.Length)];
    }
}
