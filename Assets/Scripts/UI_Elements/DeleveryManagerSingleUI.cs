using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeleveryManagerSingleUI : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI orderName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake(){
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSo recipeSo){
        orderName.text = recipeSo.recipeName;

        foreach (Transform child in iconContainer){
            if (child == iconTemplate){continue;}

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in recipeSo.KitchenObjectsSOList){
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }

}
