using UnityEngine;
using UnityEngine.AI;
using System;

public class NpcController : MonoBehaviour
{
    public static NpcController Instance { get; private set; }

    [SerializeField] NavMeshAgent agent; // Reference to the NavMeshAgent component
    

    // Define a delegate with a Transform parameter
    public delegate void EventHandler(Table npcTable);
    
    public event EventHandler OnDestinationReached; // Event triggered when NPC reaches destination

    private bool hasReachedDestination = false; // Flag to track if the NPC has reached its destination

    public void Awake(){
        Instance = this;
    }

    public void SetDestination(Table destinationTable){
        if (destinationTable != null)
        {
            agent.SetDestination(destinationTable.transform.position); // Set destination for the NPC
            // Get the Npc component attached to the current GameObject
            Npc npc = GetComponent<Npc>();
            npc.SetTable(destinationTable);     
            hasReachedDestination = false; // Reset the flag when setting a new destination
        }
    }

    void Update(){
        // Check if the NPC has reached its destination and the event hasn't been triggered yet
        if (!hasReachedDestination && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Set the flag to true to prevent multiple event triggers
            hasReachedDestination = true;

            // Get the Npc component attached to the current GameObject
            Npc npc = GetComponent<Npc>();

            // Trigger event when NPC reaches destination
            OnDestinationReached?.Invoke(npc.GetTable());
        }
    }
}
