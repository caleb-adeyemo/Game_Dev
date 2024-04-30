using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterController : MonoBehaviour
{
    [SerializeField] private DeleveryCounter deliveryCounter; // Reference to the delivery counter
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component
    [SerializeField] private Waiter waiter; // Waiter ref.

    // [SerializeField] private Table testTable;
    private bool hasAnOrder = false;

    private void Start(){
        DeleveryManager.Instance.OnRecipeCopleted += OnRecipeCompletedHandler; // Subscribe to the event
    }

    private void OnDestroy(){
        DeleveryManager.Instance.OnRecipeCopleted -= OnRecipeCompletedHandler; // Unsubscribe from the event
    }

    private void OnRecipeCompletedHandler(object sender, System.EventArgs e){
        // Set the destination of the waiter to the delivery counter
        SetDestination(deliveryCounter);
    }

    private void SetDestination(DeleveryCounter destination){
        if (destination != null)
        {
            agent.SetDestination(destination.transform.position); // Set destination for the NPC
            hasAnOrder = true; // Update the state of the waiter 
        }
    }

    private void Update(){

        if(hasAnOrder){
            // Waiter has the kitchen object, deliver it to the table 
            if (waiter.HasKitchenObject()){
                // Check if the waiter is at the table
                if (agent.remainingDistance <= agent.stoppingDistance){
                    // Drop the order
                    waiter.GetKitchenObject().SetKitcehnObjParent(waiter.getDeleveryList()[0]);
                    // waiter.GetKitchenObject().SetKitcehnObjParent(testTable);

                    // Update the state of the waiter
                    hasAnOrder = false;
                }

            }
            // Waiter doesn't have anything in their hands, pick up the order
            else { 
                if (agent.remainingDistance <= agent.stoppingDistance){
                    // Pick up the order from the delivery counter
                    Debug.Log("Has an Order:" + hasAnOrder);
                    deliveryCounter.GetKitchenObject().SetKitcehnObjParent(waiter);

                    // Set the destination to the next table in the waiter's list 
                    if (waiter.getDeleveryList().Count > 0){
                        agent.SetDestination(waiter.getDeleveryList()[0].transform.position);
                        // agent.SetDestination(testTable.transform.position);
                    }
                }
                
            }
        }
        
    }

}
