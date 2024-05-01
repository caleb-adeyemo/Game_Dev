using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class WaiterController : MonoBehaviour
{
    [SerializeField] private DeleveryCounter deliveryCounter; // Reference to the delivery counter
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component
    [SerializeField] private Waiter waiter; // Waiter ref.
    [SerializeField] private GameObject home; // Waiter station

    private Table tableToDeliverTo;

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
                    waiter.GetKitchenObject().SetKitcehnObjParent(tableToDeliverTo);

                    // Remove the order form the waiters list 
                    waiter.removeOrderWithTable(tableToDeliverTo);

                    // if (deliveryCounter.HasKitchenObject())
                    // Update the state of the waiter
                    hasAnOrder = false;
                }
            }
            // Waiter doesn't have anything in their hands, pick up the order
            else { 
                if (agent.remainingDistance <= agent.stoppingDistance){
                    // Pick up the order from the delivery counter
                    deliveryCounter.GetKitchenObject().SetKitcehnObjParent(waiter);
                    // Set the destination to the next table in the waiter's list that has the order he is holding 
                    if (waiter.getDeleveryList().Count > 0){
                        // Get the plate the player is carrying
                        if(waiter.GetKitchenObject().TryGetPlate(out Plate plate)){
                            // List of ingrediants on the waiters plate
                            List<KitchenObjectsSO> list = plate.GetKitchenObjectsSOList();

                            // Itterate over each order in the waiters list 
                            Debug.Log("============== Order of check ===========");
                            foreach (Order order in waiter.getDeleveryList()){
                                Debug.Log("Name: " + order.getOrderRecipeSO());
                                // Compare the list of ingredients on the plate with the list fo ingrediants in the pending orders list
                                HashSet<KitchenObjectsSO> set1 = new HashSet<KitchenObjectsSO>(list);
                                HashSet<KitchenObjectsSO> set2 = new HashSet<KitchenObjectsSO>(order.getOrderRecipeSO().getIngredients());
                                bool areEqual = set1.SetEquals(set2);

                                // check which order is the same as the one waiter is holding 
                                if(areEqual){
                                    // The first table that has an order == to the order waiter is holding
                                    tableToDeliverTo = order.getOrdertable();

                                    Debug.Log("Choosen table:" + tableToDeliverTo);

                                    // Debug.Log(tableToDeliverTo);
                                    agent.SetDestination(tableToDeliverTo.transform.position);
                                    break; // Prevent the tableToDelever form being overwritten if there are 2 of the same orders
                                }
                            }
                        }
                    }
                }
            }
        }
        // Doesnt have Orders
        else{
            // Go back to the home station
            agent.SetDestination(home.transform.position);
        }
    }
}
