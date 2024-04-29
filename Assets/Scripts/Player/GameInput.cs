using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    
//Unity generated class Code, That updates as you add more movment and actions - PlayerInputActions
PlayerInputActions playerInput;

//  Create Interact event
public event EventHandler OnInteract;
public event EventHandler OnInteract2;


private void Awake(){
    // Create an obj that deals with all player actions; walking, interacting, carrying, etc
    playerInput =  new PlayerInputActions();
    // Enable the obj; Unity requries you to enable this.
    playerInput.Player.Enable();
    // When the player hits "Spacebar"; call the Interact_performed function to alert Subcribers
    playerInput.Player.Interacts.performed += Interact_performed;
    // When the play hits 'C'; 
    playerInput.Player.Cuts.performed += Interact_performed2;
}

// --------- MOVEMENT -------

// Method to create a Vector2 for player movment -> (+-ve Right/Left, +-ve Up/Down)
public Vector2 GetMovementVector(){
    // Read the keys that was presses; WASD OR Arrow keys.
    Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
    // Normalise the input vector
    inputVector = inputVector.normalized;
    return inputVector;
}

//  ---------INTERACTION ----------

// Shout that the OnInteract event has been triggered
private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    // Shout!!!
    OnInteract?.Invoke(this, EventArgs.Empty);
}

private void Interact_performed2(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    // Shout!!!
    OnInteract2?.Invoke(this, EventArgs.Empty);
}
}
