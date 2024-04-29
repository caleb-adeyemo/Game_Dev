using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleveryManager : MonoBehaviour{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCopleted;
    public static DeleveryManager Instance { get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSo> waitingRecipeSoList;

    private int waitingRecipesMax = 4; // Max number of orders that spawns per time

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Awake(){
        Instance = this;
        waitingRecipeSoList = new List<RecipeSo>();
    }
    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer < 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;

            // If the number of orders active rn is less then the required max of 4 orders; then spawn a random order
            if (waitingRecipeSoList.Count < waitingRecipesMax){
                // Choose a random Order to add to the list of things to cook
                RecipeSo waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                // Add the prde tto the platers cooking list 
                waitingRecipeSoList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void deliverRecipe(Plate plate){
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

                    OnRecipeCopleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // Player did not delever the correct recipe
        Debug.Log("Player Didn't delever the corrrect recipe!!!");
    }

    public List<RecipeSo> getWaitingRecipeSoList(){
        return waitingRecipeSoList;
    }
}
