using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleveryCounter : BaseCounter{
    public override void Interact(Player player){
        // Check if player has a Kitchen object
        if (player.HasKitchenObject()){
            // Check of a player is holding a plate
            if (player.GetKitchenObject().TryGetPlate(out Plate plate)){
                DeleveryManager.Instance.deliverRecipe(plate);
                player.GetKitchenObject().DestroySelf();
            }
            
        }
    }
}
