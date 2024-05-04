using UnityEngine;

// Serializable attribute allows instances of this class to be shown in the Inspector
[System.Serializable]
public class Table : MonoBehaviour, IKitchenObjectParent
{
    // Boolean flag indicating whether the table is free or not
    public bool isFree;
    [SerializeField] private GameObject spawnPoint; // Top of the table
    [SerializeField] private GameObject seat; // Seat at the table
    private KitchenObject kitchenObject; // Kitchen object on the table


    // Constructor to initialize the Table instance with a given transform
    public Table(){
        // By default, mark the table as free
        isFree = true;
    }

    public GameObject getSeat(){
        return seat;
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
