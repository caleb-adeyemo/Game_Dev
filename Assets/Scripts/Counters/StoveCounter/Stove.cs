using System;
using UnityEngine;

public class Stove : BaseCounter, IHasProgress{
    public static event EventHandler<OnstateChangedEventArgs> OnStateChanged;
    public class OnstateChangedEventArgs : EventArgs { 
        public State state;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }
    private State state;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;
    
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;



    // private void Start(){
    //     // the state should be idle till somehting is put on it
    //     state = State.Idle;
    // }

    private void Update(){
        switch (state){
            case State.Idle:
                break;

            case State.Frying:
                // Increment the time cooked
                fryingTimer += Time.deltaTime;

                // Trigger event to set the progrssBar fill to 'fryingTimer(Normalized)'; ranges btw 0 -> 1 
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(fryingTimer / fryingRecipeSO.fryingTimerMax, Color.green, false));

                // Check if frying is complete
                if (fryingTimer > fryingRecipeSO.fryingTimerMax){
                    // Frying complete
                    GetKitchenObject().DestroySelf(); // Destroy the Raw meat

                    // Spawn cooked meat
                    KitchenObject.spawnKitchenObject(fryingRecipeSO.output, this);

                    // Change state to Fried
                    state = State.Fried;

                    // Set the burning timer
                    burningTimer = 0f;

                    // Get the butmingrecipeSO; it contains how long the item should last beofre it's burnt 
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    
                }
                break;

            case State.Fried:
                // Increment the time burning
                burningTimer += Time.deltaTime;

                // Trigger event to set the progrssBar fill to 'burningTimer(Normalized)'; ranges btw 0 -> 1 
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(burningTimer / burningRecipeSO.burningTimerMax, Color.red, true));

                // Check if burning is complete
                if (burningTimer > burningRecipeSO.burningTimerMax)
                {
                    // Meat burned
                    GetKitchenObject().DestroySelf(); // Destroy the Cooked meat

                    // Spawn burnt meat
                    KitchenObject.spawnKitchenObject(burningRecipeSO.output, this);

                    // Change state to burned
                    state = State.Burned;

                    // Trigger event to set teh progrssBar fill to 'zero'; It will disapear then
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f, Color.green, false));
                }
                break;

            case State.Burned:
                break;
        }
        OnStateChanged?.Invoke(this, new OnstateChangedEventArgs{
            state = state
        });
    }

    public override void Interact(Player player){
        // If Stove is empty?
        if (!HasKitchenObject()){
            // If player is holding an Ingredient
            if (player.HasKitchenObject()){
                // Player is holding an ingredient that can be fried
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO())){
                    // Get the object from the player and then set the parent of the object as the counter; i.e. player dropped theitem on the stove
                    player.GetKitchenObject().SetKitcehnObjParent(this);
                    // Get the frying recipe for the item that was dropped; it contains how long the obj should fry for
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    // Set the state of the fryer to be "State.Frying"
                    state = State.Frying;
                    // Reset the timer for the frying food
                    fryingTimer = 0f;
                    // Fire an event to trigger the progress bar to fill up
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(fryingTimer/fryingRecipeSO.fryingTimerMax, Color.green, false)) ;
                }
            }
        }
        // If the counter is not free
        else {
            // Player is carring something
            if (player.HasKitchenObject()){
                // Check to see if the thing the player is carrying is a plate 
                if (player.GetKitchenObject().TryGetPlate(out Plate plateKitchenObject1)) {
                    Plate plateKitchenObject = player.GetKitchenObject() as Plate;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())){
                        // Destroy the KitchenObject that was on the counter
                        GetKitchenObject().DestroySelf();

                        // Reset the state of the stove
                        state = State.Idle;

                        // Trigger event to set teh progrssBar fill to 'zero'; It will disapear then
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f, Color.green, false));
                    }
                }
            }
            // players hands are free
            else {
                // Let player pick up the item
                GetKitchenObject().SetKitcehnObjParent(player);

                // Reset the state of the stove
                state = State.Idle;

                // Trigger event to set teh progrssBar fill to 'zero'; It will disapear then
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs(0f, Color.green, false));

            } 
        }    
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForImput(KitchenObjectsSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        }
        else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
            if(fryingRecipeSO.input == inputKitchenObjectSO){
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
            if(burningRecipeSO.input == inputKitchenObjectSO){
                return burningRecipeSO;
            }
        }
        return null;
    }

}
