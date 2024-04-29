using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseCounter
{  
    public override void Interact(Player player){
        // If counter is empty?
        if (!HasKitchenObject()){
            // If player is holding an object
            if (player.HasKitchenObject()){
                // Get the object from the player and then set the parent of the object as the counter
                player.GetKitchenObject().SetKitcehnObjParent(this);
            }
            // Players hands are empty?
            else{

            }
        }
        // If the counter is not free
        else {
            // Player is carring something
            if (player.HasKitchenObject()){
                // Check to see if the thing the player is carrying is a plate 
                if (player.GetKitchenObject().TryGetPlate(out Plate plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())){
                        // Destroy the KitchenObject that was on the counter
                        GetKitchenObject().DestroySelf();
                    }
                    
                }else{ // Player is not carrying a plat; but is carrying something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        // Coounter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectsSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            // players hands are free
            else {
                // Let player pick up the item
                GetKitchenObject().SetKitcehnObjParent(player);
            }
        }
        
    }
}
