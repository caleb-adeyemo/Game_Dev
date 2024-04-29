using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleveryMangerUI : MonoBehaviour{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemp;

    private void Awake(){
        // Make the Order invisible on awake
        recipeTemp.gameObject.SetActive(false);
    }

    private void Start(){
        DeleveryManager.Instance.OnRecipeSpawned += DeleveryManager_OnRecipeSpawned;
        DeleveryManager.Instance.OnRecipeCopleted += DeleveryManager_OnRecipeCopleted;

        UpdateVisual();
    }

    private void DeleveryManager_OnRecipeSpawned(object sender, System.EventArgs e){
        UpdateVisual();
    }

    private void DeleveryManager_OnRecipeCopleted(object sender, System.EventArgs e){
        UpdateVisual();
    }
    private void UpdateVisual(){
        foreach(Transform child in container){
            if (child == recipeTemp){continue;}
            Destroy(child.gameObject);
        }

        foreach  (RecipeSo recipeSo in DeleveryManager.Instance.getWaitingRecipeSoList()){
            Transform recipetransform = Instantiate(recipeTemp, container);
            recipetransform.gameObject.SetActive(true);
            recipetransform.GetComponent<DeleveryManagerSingleUI>().SetRecipe(recipeSo);
        }

    }
}
