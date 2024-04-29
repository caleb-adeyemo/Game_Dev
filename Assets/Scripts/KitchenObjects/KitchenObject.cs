using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class represents a kitchen object in the game world.
    It contains functionality related to setting the parent counter, destroying itself, and spawning new kitchen objects.

    Variables:
        kitchenObjectsSO (KitchenObjectsSO): The scriptable object containing information about this kitchen object.
        kitchenObjparent (IKitchenObjectParent): The parent counter of this kitchen object.

    Functions:
        GetKitchenObjectsSO: Returns the KitchenObjectsSO associated with this kitchen object.
        SetKitcehnObjParent: Sets the parent counter of this kitchen object.
        GetKitchenObjParent: Returns the parent counter of this kitchen object.
        DestroySelf: Destroys this kitchen object.
        spawnKitchenObject: Spawns a new kitchen object.
*/
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private IKitchenObjectParent kitchenObjparent;

    // Returns the KitchenObjectsSO associated with this kitchen object.
    public KitchenObjectsSO GetKitchenObjectsSO(){
        return kitchenObjectsSO;
    }

    // Sets the parent counter of this kitchen object.
    public void SetKitcehnObjParent(IKitchenObjectParent kitchenObjParent){
        // Clear the kitchen object from the old counter (current one)
        if(this.kitchenObjparent != null){
            this.kitchenObjparent.ClearKitchenObject();
        }

        // Update the counter the object thinks it's on top of
        this.kitchenObjparent = kitchenObjParent;

        // Check to see if the new counter already has something on it
        if(kitchenObjParent.HasKitchenObject()){
            Debug.Log("kitchenObjParent already has a kitchen object!");
        }
        
        // Update the object the counter thinks it's holding
        kitchenObjParent.SetKitchenObject(this);

        // Update the visuals of the counter
        transform.parent = this.kitchenObjparent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    // Returns the parent counter of this kitchen object.
    public IKitchenObjectParent GetKitchenObjParent(){
        return kitchenObjparent;
    }

    // Destroys this kitchen object.
    public void DestroySelf(){
        kitchenObjparent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out Plate plateKitchenObject){
        if (this is Plate){
            plateKitchenObject = this as Plate;
            return true;
        } else{
            plateKitchenObject = null;
            return false;
        }
    }

    // Spawns a new kitchen object.
    public static GameObject spawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent){
        // Instantiate a kitchen object (e.g. tomato, dough, etc) as a GameObject
        GameObject newGameObject = Instantiate(kitchenObjectsSO.prefab, kitchenObjectParent.GetKitchenObjectFollowTransform().position, Quaternion.identity);
        // Set the new object parent to the player
        newGameObject.GetComponent<KitchenObject>().SetKitcehnObjParent(kitchenObjectParent);
        return newGameObject;
    }
}
