using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleveryCounter : BaseCounter{
    public override void Interact(Player player){
        // Check if player has a Kitchen object
        if (player.HasKitchenObject()){
            // Check of a player is holding a plate
            if (player.GetKitchenObject().TryGetPlate(out Plate plate)){
                bool completeOrder = DeleveryManager.Instance.deliverRecipe(plate);
                if (completeOrder){
                    // Get the object from the player and then set the parent of the object as the Delevery counter
                    player.GetKitchenObject().SetKitcehnObjParent(this);
                }else{
                    player.GetKitchenObject().DestroySelf();
                }
                
            }
            
        }
    }
}
