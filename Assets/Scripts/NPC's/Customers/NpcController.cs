using UnityEngine;
using UnityEngine.AI;
using System;

public class NpcController : MonoBehaviour
{
    public static NpcController Instance { get; private set; }

    [SerializeField] NavMeshAgent agent; // Reference to the NavMeshAgent component

    public event EventHandler OnDestinationReached; // Event triggered when NPC reaches destination

    private bool hasReachedDestination = false; // Flag to track if the NPC has reached its destination

    public void Awake()
    {
        Instance = this;
    }

    public void SetDestination(Transform destination)
    {
        if (destination != null)
        {
            agent.SetDestination(destination.transform.position); // Set destination for the NPC
            hasReachedDestination = false; // Reset the flag when setting a new destination
        }
    }

    void Update()
    {
        // Check if the NPC has reached its destination and the event hasn't been triggered yet
        if (!hasReachedDestination && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Set the flag to true to prevent multiple event triggers
            hasReachedDestination = true;

            // Trigger event when NPC reaches destination
            OnDestinationReached?.Invoke(this, EventArgs.Empty);
        }
    }
}
