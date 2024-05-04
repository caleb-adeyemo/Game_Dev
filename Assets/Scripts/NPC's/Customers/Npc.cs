using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour, IKitchenObjectParent{
    // State
    public enum State{
        WalkingToTable,
        Waiting,
        Eating,
        Destroying,
        Leaving,
        Left
    }

    // Variables
    [SerializeField] private GameObject spawnPoint; // Waiter's hands
    [SerializeField] public NavMeshAgent agent; // Reference to the NavMeshAgent component

    private Table npcTable; // Each NPC has a table 
    private State state; // Each Npc has a state its in
    private float timeToEat = 5f; // Time they habve to eat
    private KitchenObject kitchenObject; // Kitchen object waiter is carrying

    // Constructor
    public Npc(){
        state = State.Waiting;
    }

    // Start
    public void Awake(){
        timeToEat = UnityEngine.Random.Range(10, 15);
    }

    // Get
    public Table getTable(){
        return npcTable;
    }
    public State getState(){
        return state;
    }
    public float getTimeToEat(){
        return timeToEat;
    }
    // Set
    public void setTable(Table table){
        npcTable = table;
    }
    public void setState(State newState){
        state = newState;
    }

    // Functions
    public void SetDestination(Table destinationTable){
        if (destinationTable != null){
            agent.SetDestination(destinationTable.getSeat().transform.position); // Set destination for the NPC
            setTable(destinationTable);     
        }
    }
    public void SetDestination(GameObject obj){
        if (obj != null){
            agent.SetDestination(obj.transform.position); // Set destination for the NPC
        }
    }

    public bool IsWalking(){
        if (state == State.WalkingToTable || state == State.Leaving){
            return true;
        }
        return false;
    }
    // ============================= IKitchenObjectParent Functions ==================================
    public Transform GetKitchenObjectFollowTransform(){
        return spawnPoint.transform;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject){
        kitchenObject = newKitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
    // ============================= IKitchenObjectParent Functions ==================================
}
