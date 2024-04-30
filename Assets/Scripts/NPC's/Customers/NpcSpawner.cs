using System;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private Npc npcPrefab; // Prefab of the NPC GameObject
    [SerializeField] private Transform spawnPoint; // Point where NPCs will spawn
    [SerializeField] private TableManager tableManager; // Reference to the TableManager

    [SerializeField] private int maxSpawnedNPCs = 1; // Maximum number of spawned NPCs
    [SerializeField] private float spawnInterval = 0f; // Interval between NPC spawns

    private float spawnTimer = 0f; // Timer to track spawning intervals
    private int numSpawnedNPCs = 0; // Track the number of spawned NPCs

    public event EventHandler OnNpcSpawn; // Event triggered when NPC reaches destination

    public static NpcSpawner Instance { get; private set;}


   private void Awake(){
        Instance = this;
   }
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
            // Trigger event when NPC Spawns
            OnNpcSpawn?.Invoke(this, EventArgs.Empty);
        }
    }

    void SpawnNPC()
    {
        // Instantiate NPC at spawn point
        Npc newNPC = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

        // Increment the number of spawned NPCs
        numSpawnedNPCs++;

        // Get the adreess of the selected free table
        Table assignedTable = tableManager.GetRandomFreeTable();

        // Set NPC destination to a free table
        newNPC.npcController.SetDestination(assignedTable);

        
    }
}
