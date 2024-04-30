using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterController : MonoBehaviour
{
    [SerializeField] private Transform deliveryCounter; // Reference to the delivery counter
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component

    private bool hasAnOrder = false;

    private void Start()
    {
        DeleveryManager.Instance.OnRecipeCopleted += OnRecipeCompletedHandler; // Subscribe to the event
    }

    private void OnDestroy()
    {
        DeleveryManager.Instance.OnRecipeCopleted -= OnRecipeCompletedHandler; // Unsubscribe from the event
    }

    private void OnRecipeCompletedHandler(object sender, System.EventArgs e)
    {
        // Set the destination of the waiter to the delivery counter
        SetDestination(deliveryCounter);
    }

    private void SetDestination(Transform destination)
    {
        if (destination != null)
        {
            agent.SetDestination(destination.transform.position); // Set destination for the NPC
            hasAnOrder = true; // Update the state of the waiter 
        }
    }

    private void Update()
    {
        // Check if the NPC has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && hasAnOrder)
        {
            Debug.Log("Waiter is Here");
            hasAnOrder= false;
            // Logic to pick up the order and serve it to the table
            // You can implement this logic based on your game's requirements
        }
    }
}
