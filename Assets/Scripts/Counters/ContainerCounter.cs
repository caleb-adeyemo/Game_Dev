using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    // Spawn point for kitchen objects

 
    public override void Interact(Player player){
        // If nothing on the counter
        if(!HasKitchenObject()){
            // If player already has something in their hands
            if(player.HasKitchenObject()){
                // Do nothing!!
            }
            // If player's hands are free...
            else{
                KitchenObject.spawnKitchenObject(kitchenObjectsSO, player);
            }
            
            
        } 
        // If there is something already on the counter, let the player pick it up
        else{
            GetKitchenObject().SetKitcehnObjParent(player);
        } 
        
    }
}
