using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plate : KitchenObject{
    // Event to trigger when an Ingredient is added to the plate
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs{
        public KitchenObjectsSO kitchenObjectsSO;
    }
    // List of valide items that can be added to the plate
    [SerializeField] private List<KitchenObjectsSO> validIngredientsSOList;

    // List of things on the plate; rn
    private List<KitchenObjectsSO> itemsOnThePlate; 

    // Initialse the list of items on the plate; on Awake it's empty
    public void Awake(){
        itemsOnThePlate = new List<KitchenObjectsSO>();
    }

    // ================================ FUNCTIONS ================================
    // Fuction that takes a kitchen object, and adds it the plate. 
    public bool TryAddIngredient(KitchenObjectsSO ingredientSO){
        // Check to see of the ingredient trying to be added to the plate is valid
        if(!validIngredientsSOList.Contains(ingredientSO)){
            return false;
        }


        // Plate already has the ingredient; don't allow duplicates
        if (itemsOnThePlate.Contains(ingredientSO)){
            return false;
        } 
        else{
            // Add the ingtedient to the plate; and return true
            itemsOnThePlate.Add(ingredientSO);

            // Alert subscribers to Update the visual AND Update the icons; "plateComplete_visual.cs & plateUI.cs"
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectsSO = ingredientSO
            });
            return true;
        };
        
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList(){
        return itemsOnThePlate;
    }
}
