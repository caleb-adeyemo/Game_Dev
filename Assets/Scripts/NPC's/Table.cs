using UnityEngine;

[System.Serializable]
public class Table
{
    public Transform tableTransform;
    public bool isFree;

    public Table(Transform tableTransform)
    {
        this.tableTransform = tableTransform;
        isFree = true; // By default, the table is free
    }
}
