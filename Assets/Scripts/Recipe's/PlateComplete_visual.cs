using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateComplete_visual : MonoBehaviour{
    // DataStructure to connect a Kitchen object to an associated Visual on the plate; i.e slided_cheese_SO -> cheese slice GameObject on Burger_visual Gameobject
    [Serializable]
    public struct KitchenObjectsSO_GameObject{
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }
    // Reference to the plate game object
    [SerializeField] private Plate plate;
    // List of Stucts of associated Kitchen object SO's -> appropriate GameObject visuals on the plate
    [SerializeField] private List<KitchenObjectsSO_GameObject> KitchenObjectsSO_GameObject_List;

    // On Start; Listen to Ingrediants added to plate event
    public void Start(){
        plate.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }
    // Event handler for when an ingredient is added to the plate
    private void PlateKitchenObject_OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs e){
        // Loop through all the kitchenObject SO's -> Visual on plate's 
        foreach (KitchenObjectsSO_GameObject kitchenObjectsSOGameObject in KitchenObjectsSO_GameObject_List){
            // Filter out the vusual that isn;t the recived one from the event 
            if (kitchenObjectsSOGameObject.kitchenObjectsSO == e.kitchenObjectsSO){
                // Activate the visual for the SO that triggered the event; i.e Meat SO activates Meat visual 
                kitchenObjectsSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
