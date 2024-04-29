using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateIconUI : MonoBehaviour{
    // Get the Plate game object ref.
    [SerializeField] private Plate plate;

    // Ref. to icon template game object; white background with Food Sprite on it
    [SerializeField] private Transform iconTemp;

    // On awake; make the Template icon invisible  
    private void Awake(){
        iconTemp.gameObject.SetActive(false);
    }

    // Listen for the event when an ingredient is added to the plate 
    public void Start(){
        plate.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    // Event handler for when the ingreient is added; Update the icons indicating what food is on the plate aleady
    private void PlateKitchenObject_OnIngredientAdded(object sender, Plate.OnIngredientAddedEventArgs e){
        UpdateVisual();
    }

    private void UpdateVisual(){
        foreach (Transform child in transform){
            if (child != iconTemp){
                Destroy(child.gameObject);
            }
        }
        foreach (KitchenObjectsSO kitchenObjectsSO in plate.GetKitchenObjectsSOList()){
            Transform iconTransform = Instantiate(iconTemp, transform);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO_Sprite(kitchenObjectsSO);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
