using System;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour, IKitchenObjectParent{
    public static Waiter Instance {get; private set;}

    [SerializeField] private GameObject spawnPoint; // Waiter's hands
    private KitchenObject kitchenObject; // Kitchen object waiter is carrying

    private List<Order> deleveryList;

    private void Awake(){
        deleveryList = new List<Order>();
    }
    // ==================================== Events ==================================================
    private void Start(){
        // Subscribe to event triggered by DeleveryManager
        DeleveryManager.Instance.OnRecipeSpawned += DeleveryManager_OnRecipeSpawned;

    }
    private void DeleveryManager_OnRecipeSpawned(Order order){
        deleveryList.Add(order);
    }
    // ==================================== Events ==================================================
    // ==================================== GET/SET =================================================
    public List<Order> getDeleveryList(){ // Get waiters delevery list 
        return deleveryList;
    }
    // ==================================== GET/SET ==================================================



    // ============================= Waiter Functions ================================================
    public Order getOrderWithTableObj(Table t){
        foreach (Order order in deleveryList){
            if (order.getOrdertable() == t){
                return order;
            }
        }
        return null; // Return null if no matching order is found
    }


    public void removeOrderWithTableObj(Table _table){
        Order orderToRemove = null;
        foreach (Order order in deleveryList){
            if (order.getOrdertable() == _table){
                orderToRemove = order;
                break; // Exit loop once the matching order is found
            }
        }
        if (orderToRemove != null){
            deleveryList.Remove(orderToRemove);
        }
    }
    // ============================= Waiter Functions ================================================

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
