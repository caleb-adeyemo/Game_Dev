using UnityEngine;
using UnityEngine.AI;
using System;

public class NpcController : MonoBehaviour{
    // Instance
    public static NpcController Instance { get; private set; }

    // Variables
    [SerializeField] NavMeshAgent agent; // Reference to the NavMeshAgent component
    private GameObject exit; // Reference to the Exit door

    private bool hasReachedDestination = false; // Flag to track if the NPC has reached its destination
    private float timeToEat;
    private float timer;
    private State state;

    private Table tableNpcIsOn;

    // States
    private enum State{
        WalkingToTable,
        Waiting,
        Eating,
        Destroy,
        Leaving 

    }

    // Event
    public delegate void E(Table npcTable);
    public event E OnDestinationReached; // Event triggered when NPC reaches destination

    // Awake
    public void Awake(){
        Instance = this;
        state = State.WalkingToTable;
    }

    // Subscriptions
    public void Start(){
        WaiterController.Instance.OnFoodDelevered += OnFoodDelevered;
    }

    // Event Handlers
    private void OnFoodDelevered(Table table) {
        // Debug.Log("Waiter dropped food on table: " + table);
        // Change the state to Eating
        state = State.Eating;

        // Get a random timeFor the Npc to eat
        timeToEat = UnityEngine.Random.Range(3, 5);

        // Set the table to be destroyed 
        tableNpcIsOn = table;
    }

    // Setter function
    public void setExit(GameObject _exit){
        exit = _exit;
    }

    // NpcController Function
    public void SetDestination(Table destinationTable){
        if (destinationTable != null){
            agent.SetDestination(destinationTable.transform.position); // Set destination for the NPC
            // Get the Npc component attached to the current GameObject
            Npc npc = GetComponent<Npc>();
            npc.SetTable(destinationTable);     
            hasReachedDestination = false; // Reset the flag when setting a new destination
        }
    }

    // Update
    void Update(){
        switch (state){
            // Walking To table
            case State.WalkingToTable:
                // Check if the NPC has reached its destination and the event hasn't been triggered yet
                if (!hasReachedDestination && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
                    // Set the state to waiting to prevent multiple event triggers
                    state = State.Waiting;

                    // Get the Npc component attached to the current GameObject
                    Npc npc = GetComponent<Npc>();

                    // Trigger event to place an order; {Subs => DelevryManger.cs}
                    OnDestinationReached?.Invoke(npc.GetTable());
                }
            break;
            // Idle
            case State.Waiting:
            break;
            // Eating
            case State.Eating:
                
                timer += Time.deltaTime;
                if (timer >= timeToEat){
                    state = State.Destroy;
                }

            break;
            // Destroy plate; maybe anninmation 
            case State.Destroy:
                // Debug.Log("State in Destroy: "+ state);
                // Destroy the plates on the table
                if (tableNpcIsOn.GetKitchenObject() != null){
                    tableNpcIsOn.GetKitchenObject().DestroySelf();
                    state = State.Leaving;
                }
                
            break; 
            // Walk away
            case State.Leaving:
                Debug.Log("We are walking away now");
                agent.SetDestination(exit.transform.position);
                state = State.Waiting;
                Debug.Log("Final State: " + state);
            break;  
        }
    }
}
