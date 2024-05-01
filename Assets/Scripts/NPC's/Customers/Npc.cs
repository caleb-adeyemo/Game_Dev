using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IKitchenObjectParent
{
    public static Npc Instance {get; private set;}

    [SerializeField] private GameObject spawnPoint; // Waiter's hands
    private KitchenObject kitchenObject; // Kitchen object waiter is carrying
    private Table npcTable; // Each NPC has a table 
    public NpcController npcController; // Each Npc has a controller 

    // Get
    public Table GetTable(){
        return npcTable;
    }

    // Set
    public void SetTable(Table table){
        npcTable = table;
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
