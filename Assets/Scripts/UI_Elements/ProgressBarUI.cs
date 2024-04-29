using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour{

    [SerializeField] private GameObject hasProgressGameObject; //
    private IHasProgress hasProgress; // Reference to the CuttingCounter obj in scene
    [SerializeField] private Image barImage; // Reference to the UI Image component representing the progress bar in scene
    [SerializeField] private Sprite barGreenSprite; // Reference to the UI Image component representing the progress bar Psite in the scene
    [SerializeField] private Sprite barRedSprite; // Reference to the UI Image component representing the progress bar Psite in the scene


    private void Start(){
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null){
            Debug.LogError("GameObject" + hasProgressGameObject.name + " doesn't have a component that implements IHasProgress");
        }
        // Subscribe to the CuttingCounter's OnProgressChanged event
        hasProgress.OnProgressChanged += HasProgresss_OnProgressChanged;
        // Initialize the progress bar fill amount to zero
        barImage.fillAmount = 0f;

        // Hide the bar at first
        hide();
    }

    // Event handler method called when the CuttingCounter's progress changes
    private void HasProgresss_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
        
        // Update the fill amount of the progress bar based on the normalized progress value
        barImage.fillAmount = e.ProgressNormalized;
        // Debug.Log("Ammount: " + e.ProgressNormalized);

        // Set progress bar color
        barImage.color = e.BarColor; 
        
        if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f){
            hide();
        }else{
            show();
        }
    }

    private void show() {
        gameObject.SetActive(true);
    }

    private void hide() {
        gameObject.SetActive(false);

    }


}

