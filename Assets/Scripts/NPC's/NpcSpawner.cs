using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab; // Prefab of the NPC GameObject
    [SerializeField] private Transform spawnPoint; // Point where NPCs will spawn
    [SerializeField] private TableManager tableManager; // Reference to the TableManager

    [SerializeField] private int maxSpawnedNPCs = 3; // Maximum number of spawned NPCs
    [SerializeField] private float spawnInterval = 4f; // Interval between NPC spawns

    private float spawnTimer = 0f; // Timer to track spawning intervals
    private int numSpawnedNPCs = 0; // Track the number of spawned NPCs

    void Update()
    {
        // Increment the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new NPC
        if (spawnTimer >= spawnInterval && numSpawnedNPCs < maxSpawnedNPCs)
        {
            // Reset the spawn timer
            spawnTimer = 0f;

            // Spawn a new NPC
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        // Instantiate NPC at spawn point
        GameObject newNPC = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

        // Get NPCController component
        NpcController npcController = newNPC.GetComponent<NpcController>();

        // Increment the number of spawned NPCs
        numSpawnedNPCs++;

        // Get the adreess of the selected free table
        Transform res = tableManager.GetRandomFreeTable();

        // Set NPC destination to a free table
        npcController.SetDestination(res);

        
    }
}
