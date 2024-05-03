using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleveryManager : MonoBehaviour{
    public delegate void EventHandler_OnRecipeSpawned(Order order);
    public event EventHandler_OnRecipeSpawned OnRecipeSpawned;
    public event EventHandler OnRecipeCopleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeleveryManager Instance { get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSo> waitingRecipeSoList;

    private int SuccessfullOrdersNo;

    private void Awake(){
        Instance = this;
        waitingRecipeSoList = new List<RecipeSo>();
    }

    private void Start(){
        // Subscribe to event triggered by NPCController
        NpcController.Instance.OnDestinationReached += NpcController_OnDestinationReached;
    }
 
    private void NpcController_OnDestinationReached(Table npcTable){
        // Spawn a new order (recipe) when NPC reaches destination
        RecipeSo waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)]; // Select a random recipe
        Order order = new Order(npcTable, waitingRecipeSO);
        Debug.Log("Table " + npcTable + " : " + order.getOrderRecipeSO());
        waitingRecipeSoList.Add(waitingRecipeSO); // Add the order to the orderList 
        OnRecipeSpawned?.Invoke(order); // Trigger the Event for UI to change and show the orders
    }
    private void Update(){
    }

    public bool deliverRecipe(Plate plate){
        for (int i = 0; i < waitingRecipeSoList.Count; i++) {
            RecipeSo waitingRecipe = waitingRecipeSoList[i];

            if (waitingRecipe.KitchenObjectsSOList.Count == plate.GetKitchenObjectsSOList().Count) {
                bool plateComplete = true;
                // Has the same number of ingredients
                foreach (KitchenObjectsSO recipeKitchenObjectSO in waitingRecipe.KitchenObjectsSOList) {
                    // Cycle though all ingredients in the recipe

                    bool ingredientsFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectSO in plate.GetKitchenObjectsSOList()) {
                        // Cycle though all ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredients Match
                            ingredientsFound = true;
                            break;
                        }
                    }
                    if (!ingredientsFound){
                        // This recipe ingredient is missing on the plate
                        plateComplete = false;
                    }
                }

                if (plateComplete){
                    // Player delevered the corrrect recipe
                    waitingRecipeSoList.RemoveAt(i);
                    SuccessfullOrdersNo++;
                    OnRecipeCopleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        }
        // Player did not delever the correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty); //Trigger Failed sound
        return false;
    }

    public List<RecipeSo> getWaitingRecipeSoList(){
        return waitingRecipeSoList;
    }

    public int getSuccessfullOrdersNo(){
        return SuccessfullOrdersNo;
    }
}
