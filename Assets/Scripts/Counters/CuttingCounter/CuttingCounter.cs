using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress{
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSoArray;

    public int cuttingProgress;
    public override void Interact(Player player){
         // If counter is empty?
        if (!HasKitchenObject()){
            // If player is holding an object
            if (player.HasKitchenObject()){
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())){
                    // Get the object from the player and then set the parent of the object as the counter
                    player.GetKitchenObject().SetKitcehnObjParent(this);
                    // Set the progrss of the cuts to be zero; as the objesct was just placed down
                    cuttingProgress = 0;
                    // Get the CuttingRecipeSO; it holds the max number of cuts for the recipe placed down
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    // Send event signal for all listeners
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs((float)cuttingProgress/cuttingRecipeSO.cuttingProgressMax, Color.green, false));
                }
            }
            // Players hands are empty?
            else{

            }
        }
        // If the counter is not free
        else {
            // Player is carring something
            if (player.HasKitchenObject()){
                if (player.HasKitchenObject()){
                    // Check to see if the thing the player is carrying is a plate 
                    if (player.GetKitchenObject().TryGetPlate(out Plate plateKitchenObject1)) {
                        Plate plateKitchenObject = player.GetKitchenObject() as Plate;
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())){
                            // Destroy the KitchenObject that was on the counter
                            GetKitchenObject().DestroySelf();
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

    public override void Interact2(Player player){
        // Check of there is an item on the counter AND It can also be cut
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO())){
            // Icrement the cutting progress
            cuttingProgress++;
            // Send the event for the cutting annimation
            OnCut?.Invoke(this, EventArgs.Empty);
            // Trigger the sound
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
            // Send event signal for all listeners; that the recipe was cut, and update the progressBar
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs((float)cuttingProgress/cuttingRecipeSO.cuttingProgressMax, Color.green, false));
            // If "c" pressed enough times per ingredient then spawn cut ingredient
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
                KitchenObjectsSO outputKitchenObjectSO = GetOutputForImput(GetKitchenObject().GetKitchenObjectsSO());
                // Cut the item; destroy the obect and spawn the cut object
                GetKitchenObject().DestroySelf();
                KitchenObject.spawnKitchenObject(outputKitchenObjectSO, player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO){ // Function to check of the inputed KitchenObjectSO is cuttable
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForImput(KitchenObjectsSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO){ //
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSoArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
