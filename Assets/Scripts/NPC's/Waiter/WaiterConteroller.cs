using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterController : MonoBehaviour{
    // Instance
    public static WaiterController Instance { get; private set; }

    // Variables
    [SerializeField] private DeleveryCounter deliveryCounter; // Reference to the delivery counter
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component
    [SerializeField] private Waiter waiter; // Waiter reference
    [SerializeField] private GameObject home; // Waiter station
    private Queue<Vector3> destinations = new Queue<Vector3>(); // Queue of destinations
    private bool hasAnOrder = false;
    private Table tableToGoTo = null;


    // Events
    public delegate void E(Table table);
    public event E OnFoodDelevered;

    // Awake
    public void Awake(){
        Instance = this;
    }

    // Start
    private void Start(){
        DeleveryManager.Instance.OnRecipeCopleted += OnRecipeCompletedHandler; // Subscribe to the event
    }

    // Event handler
    private void OnRecipeCompletedHandler(object sender, System.EventArgs e){
        // If the waiter is already delivering an order, stack the delivery counter as the next destination
        if (hasAnOrder){
            destinations.Enqueue(deliveryCounter.transform.position);
        } else{
            // Set the destination of the waiter to the delivery counter
            SetDestination(deliveryCounter.transform.position);
        }
    }

    // WaiterController Function
    private void SetDestination(Vector3 destination){
        agent.SetDestination(destination); // Set destination for the waiter
        hasAnOrder = true; // Update the state of the waiter 
    }

    // WaiterController Function
    private void SetNextDestination(){
        // Check there is at least one destination in the Queue
        if (destinations.Count > 0){
            // Pop the destination
            Vector3 nextDestination = destinations.Dequeue();
            // Set next destination for the waiter
            agent.SetDestination(nextDestination); 
        }
    }
    // Update
    private void Update(){
        if (hasAnOrder){
            // Waiter has the kitchen object, deliver it to the table 
            if (waiter.HasKitchenObject()){
                // Check if the waiter is at the table
                if (agent.remainingDistance <= agent.stoppingDistance){
                    // Drop the order
                    waiter.GetKitchenObject().SetKitcehnObjParent(tableToGoTo);

                    // Trigger the event to Start the Npc's timmer to eat
                    OnFoodDelevered?.Invoke(tableToGoTo);

                    // Remove the order from the waiter's list 
                    waiter.removeOrderWithTableObj(tableToGoTo);

                    // There are no more orders to deliver
                    if (destinations.Count == 0){
                        // Update the state of the waiter
                        hasAnOrder = false;
                    }
                    else{
                        // Set the next destination if there are more orders to deliver
                        SetNextDestination();
                    }
                }
            }
            // Waiter doesn't have anything in their hands, pick up the order
            else{
                if (agent.remainingDistance <= agent.stoppingDistance){
                    // Pick up the order from the delivery counter
                    deliveryCounter.GetKitchenObject().SetKitcehnObjParent(waiter);
                    // Set the destination to the next table in the waiter's list that has the order he is holding 
                    if (waiter.getDeleveryList().Count > 0){
                        // Get the plate the player is carrying
                        if (waiter.GetKitchenObject().TryGetPlate(out Plate plate)) {
                            // List of ingredients on the waiter's plate
                            List<KitchenObjectsSO> list = plate.GetKitchenObjectsSOList();

                            // Itterate over each order in the waiter's list 
                            foreach (Order order in waiter.getDeleveryList()){
                                // Compare the list of ingredients on the plate with the list of ingredients in the pending orders list
                                HashSet<KitchenObjectsSO> set1 = new HashSet<KitchenObjectsSO>(list);
                                HashSet<KitchenObjectsSO> set2 = new HashSet<KitchenObjectsSO>(order.getOrderRecipeSO().getIngredients());
                                bool areEqual = set1.SetEquals(set2);

                                // check which order is the same as the one waiter is holding 
                                if (areEqual){
                                    // Set the destination to the table
                                    destinations.Enqueue(order.getOrdertable().transform.position);
                                    tableToGoTo = order.getOrdertable(); // Get the table the food is going to be deleverted to
                                    SetNextDestination(); // Set next destination
                                    break; // Prevent the tableToDeliver from being overwritten if there are 2 of the same orders
                                }
                            }
                        }
                    }
                }
            }
        }
        // If the waiter doesn't have orders, go back to the home station
        else{
            agent.SetDestination(home.transform.position);
            tableToGoTo = null;
        }
    }
}
