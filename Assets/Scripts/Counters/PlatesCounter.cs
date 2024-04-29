using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter{
    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;

    [SerializeField] private KitchenObjectsSO plateKitchenObjectSO;
    private float spawnPlatesTimer;
    private float spawnPlateTimerMax = 4f;

    private int plateSpawnedAmmount;
    private int platesSpawnedAmmountMax = 4;

    private void Update(){
        spawnPlatesTimer += Time.deltaTime;
        if (spawnPlatesTimer > spawnPlateTimerMax){
            spawnPlatesTimer = 0f;
            if (plateSpawnedAmmount < platesSpawnedAmmountMax){
                plateSpawnedAmmount++;

                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player){
        // Check if Player's hands are free
        if (!player.HasKitchenObject()){
            // Check if there is a plate to give the player
            if (plateSpawnedAmmount > 0) {
                plateSpawnedAmmount--;

                KitchenObject.spawnKitchenObject(plateKitchenObjectSO, player);

                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
