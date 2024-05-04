using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NpcController : MonoBehaviour{
    // Instance 
    public static NpcController Instance { get; private set; }
    // Variables
    [SerializeField] private Npc npcPrefab; // Prefab of the NPC GameObject
    [SerializeField] private Transform spawnPoint; // Point where NPCs will spawn
    [SerializeField] GameObject exit; // Reference to the Exit door
    [SerializeField] private TableManager tableManager; // Reference to the TableManager
    [SerializeField] private int maxSpawnedNPCs = 1; // Maximum number of spawned NPCs
    [SerializeField] private float spawnInterval = 0f; // Interval between NPC spawns
    private float spawnTimer = 0f; // Timer to track spawning intervals
    private int numSpawnedNPCs = 0; // Track the number of spawned NPCs

    private List<Npc> spawnedNpcs;
    private float timer;

    private Npc loopNpc;

    // Awake
    public void Awake(){
        Instance = this;
        spawnedNpcs = new List<Npc>();
    }


    // Event
    public delegate void E(Table npcTable);
    public event E OnDestinationReached; // Event triggered when NPC reaches destination


    // Events
    public event EventHandler OnNpcSpawn; // Event triggered when NPC reaches destination


    // Subscriptions
    public void Start(){
        WaiterController.Instance.OnFoodDelevered += OnFoodDelevered;
    }

    // Event Handlers
    private void OnFoodDelevered(Table table) {
        // Cycle through the list of spawned Npc's
        foreach(Npc npc in spawnedNpcs) {
            // If the table food was deleveredd is the current npc in the loop
            if (npc.getTable() == table) {
                // Change the state to Eating
                npc.setState(Npc.State.Eating);
            }
        }
    }

    // Update
    void Update(){
        // Increment the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new NPC
        if (spawnTimer >= spawnInterval && numSpawnedNPCs < maxSpawnedNPCs){
            // Reset the spawn timer
            spawnTimer = 0f;

            // Spawn a new NPC
            SpawnNPC();

            // Trigger event when NPC Spawns
            OnNpcSpawn?.Invoke(this, EventArgs.Empty);
        }

        for (int i = spawnedNpcs.Count - 1; i >= 0; i--){
            loopNpc = spawnedNpcs[i];
            switch (loopNpc.getState()){
                // Walking To table
                case Npc.State.WalkingToTable:
                    // Check if the NPC has reached its destination
                    if (!loopNpc.agent.pathPending && loopNpc.agent.remainingDistance <= loopNpc.agent.stoppingDistance) {


                        Vector3 dir = loopNpc.agent.destination - transform.position;
                        dir.y = 0;//This allows the object to only rotate on its y axis
                        quaternion rot = Quaternion.LookRotation(dir);
                        loopNpc.transform.rotation = Quaternion.Lerp(transform.rotation, rot, 50f * Time.deltaTime);

                        // Trigger event to place an order; {Subs => DelevryManger.cs}
                        OnDestinationReached?.Invoke(loopNpc.getTable());

                        // Set the state to waiting to prevent multiple event triggers
                        loopNpc.setState(Npc.State.Waiting);
                    }
                break;


                // Idle
                case Npc.State.Waiting:
                    // Do Nothing
                break;

                // Eating
                case Npc.State.Eating:
                    timer += Time.deltaTime;
                    if (timer >= loopNpc.getTimeToEat()){
                        timer = 0f;
                        loopNpc.setState( Npc.State.Destroying);
                    }
                break;

                // Destroy plate
                case Npc.State.Destroying:
                    // Destroy the plates on the table
                    loopNpc.getTable().GetKitchenObject().DestroySelf();
                    // Set the state to 'leaving'
                    loopNpc.setState(Npc.State.Leaving);    
                break;

                // Walk away
                case Npc.State.Leaving:
                    loopNpc.SetDestination(exit);
                    loopNpc.setState(Npc.State.Left);
                break;  

                // Destroy Npc
                case Npc.State.Left:
                    // Check if the NPC has reached its destination
                    if (!loopNpc.agent.pathPending && loopNpc.agent.remainingDistance <= loopNpc.agent.stoppingDistance){
                        // Remove the Npc from the list 
                        spawnedNpcs.Remove(loopNpc);
                        // Destroy the Npc
                        Destroy(loopNpc.gameObject);
                        // Update the No of spawned npc's 
                        numSpawnedNPCs--;
                        tableManager.ReleaseTable(loopNpc.getTable());
                    }
                break;
            }
        }
        
    }

    void SpawnNPC(){
        // Instantiate NPC at spawn point
        Npc newNPC = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

        // Increment the number of spawned NPCs
        numSpawnedNPCs++;

        // Set NPC destination to a free table
        newNPC.SetDestination(AssignTable());

        newNPC.setState(Npc.State.WalkingToTable);

        // Add Npc to the Controller list 
        spawnedNpcs.Add(newNPC);
    }

    private Table AssignTable(){
        // Get the adreess of the selected free table
        return tableManager.GetRandomFreeTable();
    }
}
