using System;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour, IKitchenObjectParent{
    public static Waiter Instance {get; private set;}

    [SerializeField] private GameObject spawnPoint; // Waiter's hands
    private KitchenObject kitchenObject; // Kitchen object waiter is carrying

    private List<Table> deleveryList;

    private void Awake(){
        deleveryList = new List<Table>();
    }

    private void Start(){
        // Subscribe to event triggered by NPCController
        NpcSpawner.Instance.OnNpcSpawn += SubTo_NpcController;
    }
    private void SubTo_NpcController(object sender, EventArgs e){
        // Subscribe to event triggered by NPCController
        NpcController.Instance.OnDestinationReached += NpcController_OnDestinationReached;
    }

    private void NpcController_OnDestinationReached(Table npcTable){
        deleveryList.Add(npcTable);
    }

    public List<Table> getDeleveryList(){ // Get waiters delevery list 
        return deleveryList;
    }
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
}
