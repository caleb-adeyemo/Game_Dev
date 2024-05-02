using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance {get; private set;}
    // Game Input
    [SerializeField] private GameInput gameInput; // Input object to get keyboard inputs

    // Physical traits
    private float playerRadius = 1f; // Player radius
    private float playerHeight = 3f; // Player height 
    private float PLAYER_REACH = 2f; // How far Player needs to be to interact

    // Player Movement
    private float speed = 8f; // How fast a Player moves
    private float rotateSpeed = 10f; // How fast Player looks in forward direction
    private bool isWalking;

    // Player Knowledge
    private Vector3 lastKnownDirection; // Last know direction Player was facing when standing still
    private BaseCounter selectedCounter; // Current counter in-front of Player


    [SerializeField] private GameObject spawnPoint;
    // kitchen object for each counter 
    private KitchenObject kitchenObject;

    // Events
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectingCounter;
    public class OnSelectedCounterEventArgs:EventArgs{
        public BaseCounter selectedCounter;
    }
    // Event Handleer
    private void Handle_Interaction(object sender, System.EventArgs e){
        if (!GameManager.Instance.IsGamePlaying()) return; // Stop all interactions if the game is not in playing state

        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }  
    }
     private void Handle_Interaction2(object sender, System.EventArgs e){
        if (!GameManager.Instance.IsGamePlaying()) return; // Stop all interactions if the game is not in playing state

        if(selectedCounter != null){
            selectedCounter.Interact2(this);
        }  
    }
    // Awake
    private void Awake(){
        if(Instance != null){ // There is more than one player;
            Debug.LogError("There is more than one player");
        }
        Instance = this;
    }
    
    // Start
    private void Start(){
        // Listen for OnInteract event Shout!!!
        gameInput.OnInteract += Handle_Interaction;
        // Listen for OnInteract2 event
        gameInput.OnCutAction += Handle_Interaction2;
    }

    // Update
    private void Update(){
        walk(); //Check if player wants to walk
        CanInteract(); //Check if player in close enough to interact with Counters
    }

    // Walk
    // private void walk(){
    //     // Calulate if the player can move using raycast
    //     float moveDistance = speed * Time.deltaTime;

    //     Vector2 inputVector = gameInput.GetMovementVector(); // get movement vector
    //     Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Translate to 3d movment

    //     // change is walking bool, if walking
    //     isWalking = moveDir != Vector3.zero;

    //     // CapsuleCast Rather than RayCast to see if player is obstructed
    //     bool canWalk = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    //     // Path clear? Then move
    //     if (canWalk){
    //         transform.position += moveDir * moveDistance; // move the player relative to the speed and frame rate
    //     }

    //     // Make sure player is facing the direction of movement
    //     transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed); // smooth transistion with slerp
    // }
    // private void walk(){
    //     Vector2 inputVector = gameInput.GetMovementVector(); // Get movement vector
    //     Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized; // Normalize to ensure consistent speed diagonally

    //     // Calculate movement distance based on speed and frame rate
    //     float moveDistance = speed * Time.deltaTime;

    //     // Update isWalking based on whether the player is moving
    //     isWalking = moveDir != Vector3.zero;

    //     // Cast a capsule to check for obstructions
    //     bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    //     // If there's an obstruction, attempt to move along the X-axis and Z-axis separately
    //     if (!canWalk){
    //         // Attempt only X movement
    //         Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
    //         canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

    //         if (canWalk){
    //             moveDir = moveDirX;
    //         }
    //         else{
    //             // Attempt only Z movement
    //             Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
    //             canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

    //             if (canWalk){
    //                 moveDir = moveDirZ;
    //             }
    //         }
    //     }

    //     // Move the player
    //     transform.position += moveDir * moveDistance;

    //     // Ensure the player is facing the direction of movement
    //     if (moveDir != Vector3.zero){
    //         transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    //     }
    // }

    private void walk(){
    // Calculate movement distance based on speed and frame rate
    float moveDistance = speed * Time.deltaTime;

    // Get the movement vector from input
    Vector2 inputVector = gameInput.GetMovementVector();
    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized; // Normalize to ensure consistent speed diagonally

    // Calculate the target position based on the movement direction
    Vector3 targetPosition = transform.position + moveDir * moveDistance;

    // Cast a capsule to check for obstructions along the movement direction
    RaycastHit hit;
    bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, out hit, moveDistance);

    // If there's an obstruction, adjust the movement direction
    if (!canWalk){
        // Calculate the adjusted movement direction based on the hit normal
        Vector3 slideDir = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
        targetPosition = transform.position + slideDir * moveDistance;
    }

    // Move the player to the target position
    transform.position = targetPosition;

    // Update isWalking based on whether the player is moving
    isWalking = moveDir != Vector3.zero;

    // Ensure the player is facing the direction of movement
    if (moveDir != Vector3.zero){
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}

    public bool IsWaling(){
        return isWalking;
    }
    // Can Interact?
    private void CanInteract(){
        Vector2 inputVector = gameInput.GetMovementVector(); // get movement vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // Translate to 3d movment

        if(moveDir != Vector3.zero){
            lastKnownDirection = moveDir;
        }
        // RayCast to know if the player is close enough to interact with anything, Counter, Bin, etc
        if(Physics.Raycast(transform.position, lastKnownDirection, out RaycastHit raycastHit, PLAYER_REACH)){
            // If the Player is infront of a Counter
            if(raycastHit.transform.TryGetComponent(out BaseCounter counter)){
                // If the counter we are looking at is not our selected counter? i.e. each player has 1 selected counter; Null/counter.
                if(counter != selectedCounter){
                    // Make it our selected counter; Then trigger an event for the counter to change color.
                    SetSelectedCounter(counter);
                }
            }
            // If we are not in front of a Counter
            else{
                // Set our selected counter to be Null; e.g. we aren't close to a counter to select.
                // If we don't do this, once we approach a counter and walk away, it would always be selected until we go to another counter.
                SetSelectedCounter(null);
            }
        }
        // If the RayCast doesn't hit anything within PLAYER_REACH
        else{
            // Set our selected counter to be Null;
            SetSelectedCounter(null);
        };
    }

    private void SetSelectedCounter(BaseCounter counter){
        selectedCounter = counter;

        OnSelectingCounter?.Invoke(this, new OnSelectedCounterEventArgs{
            selectedCounter = selectedCounter
        });
    }


     public Transform GetKitchenObjectFollowTransform(){
        return spawnPoint.transform;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject){
        kitchenObject = newKitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
}
