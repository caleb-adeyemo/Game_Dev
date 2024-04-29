using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform spawnPoint; // The point where plates will spawn
    [SerializeField] private GameObject platesVisualPrefab; // Prefab for the visual representation of plates

    private List<GameObject> platesVisualGameObjectList; // List to store spawned plate game objects

    private void Awake()
    {
        platesVisualGameObjectList = new List<GameObject>(); // Initialize the list
    }

    private void Start()
    {
        // Subscribe to the event when plates are spawned
        platesCounter.OnPlatesSpawned += PlatesCounter_OnPlatesSpawned;

        // Subscribe to the event when plates are taken/removed; by player
        platesCounter.OnPlatesRemoved += PlatesCounter_OnPlatesRemoved;

    }

    // Event handler for when plates are spawned
    private void PlatesCounter_OnPlatesSpawned(object sender, System.EventArgs e)
    {
        // Instantiate a new plate visual prefab at the spawn point
        GameObject newPlateGameObject = Instantiate(platesVisualPrefab, spawnPoint.position, Quaternion.identity);
        
        // Offset the plate vertically to stack them on each other
        newPlateGameObject.transform.position += Vector3.up * 0.3f * platesVisualGameObjectList.Count;

        // Add the new plate game object to the list
        platesVisualGameObjectList.Add(newPlateGameObject);
    }

    // Event handler for when plates are taken/removed; by player
    private void PlatesCounter_OnPlatesRemoved(object sender, System.EventArgs e)
    {
        // Get the last Plate game object onthe plate stack
        GameObject lastPlate = platesVisualGameObjectList[platesVisualGameObjectList.Count - 1];

        // Remove the plate from the list of plates
        platesVisualGameObjectList.Remove(lastPlate);

        // Destroy the plate game object
        Destroy(lastPlate);
    }
}
