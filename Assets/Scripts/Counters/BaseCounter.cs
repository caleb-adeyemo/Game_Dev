using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Every Class that extends this base class has access to the following:

    Variables: spawnPoint(GameObject) AND kitchenObject(KitchenObject)
    Functions: GetKitchenObjectFollowTransform, SetKitchenObject, GetKitchenObject, ClearKitchenObject, HasKitchenObject
*/
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // ================================================================= Variables=============================================================================
    [SerializeField] private GameObject spawnPoint; // Spawn point for kitchen objects
    
    private KitchenObject kitchenObject; // kitchen object for each counter 

    // ================================================================= Functions =============================================================================

    public virtual void Interact(Player player){} // What to do when "spcaeBar" is clicked in front of a counter

    public virtual void Interact2(Player player){} // What to do when "c" is clicked in front of a counter

    // Return the transform component of the spawnPoint GameObject
    public Transform GetKitchenObjectFollowTransform(){
        return spawnPoint.transform;
    }

    // Set the KitchenObject the counter thinks is placed on it
    public void SetKitchenObject(KitchenObject newKitchenObject){ 
        kitchenObject = newKitchenObject;
    }

    // Return the kitchenObject that's currently on the counter 
    public KitchenObject GetKitchenObject(){ 
        return kitchenObject;
    }

    // Set the KitchenObject on the counter to be "Null"
    public void ClearKitchenObject(){ 
        kitchenObject = null;
    }

    // Return "true/false" if there is a kitchenObject on the counter 
    public bool HasKitchenObject(){ 
        return kitchenObject != null;
    }
}
