using UnityEngine;

// Serializable attribute allows instances of this class to be shown in the Inspector
[System.Serializable]
public class Table
{
    // Reference to the transform of the table GameObject
    public Transform tableTransform;

    public string tableNo;

    // Boolean flag indicating whether the table is free or not
    public bool isFree;

    // Constructor to initialize the Table instance with a given transform
    public Table(Transform tableTransform)
    {
        // Set the tableTransform field to the provided transform
        this.tableTransform = tableTransform;

        // By default, mark the table as free
        isFree = true;
    }
}
