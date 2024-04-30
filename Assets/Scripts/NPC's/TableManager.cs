using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] List<Table> tables; // List of tables

    // Get a random free table's transform from the list of tables
    public Transform GetRandomFreeTable()
    {
        // List to store references to free tables
        List<Table> freeTables = new List<Table>();

        // Iterate through each table in the list
        foreach (Table table in tables)
        {
            // Check if the table is free
            if (table.isFree)
            {
                freeTables.Add(table); // Add free tables to the list
            }
        }
        foreach (Table table in tables){
            Debug.Log("" + table.tableNo);
        }
        // If there are free tables available
        if (freeTables.Count > 0)
        {
            // Get a random index within the range of freeTables list
            int randomIndex = Random.Range(0, freeTables.Count);

            // Mark the selected table as occupied
            freeTables[randomIndex].isFree = false;

            Debug.Log("Selected table: " + freeTables[randomIndex].tableNo);

            // Return the transform of the selected table
            return freeTables[randomIndex].tableTransform;
        }

        // No free tables available
        return null;
    }

    // Release a table by marking it as free
    public void ReleaseTable(Transform table)
    {
        // Find the table object with the specified transform
        Table tableObj = tables.Find(t => t.tableTransform == table);
        
        // If the table is found
        if (tableObj != null)
        {
            tableObj.isFree = true; // Mark table as available
        }
    }

    // Visualize the tables using Gizmos
    void OnDrawGizmos()
    {
        // Set the color of Gizmos to green
        Gizmos.color = Color.green;

        // Iterate through each table in the list
        foreach (Table table in tables)
        {
            // Draw a wire cube Gizmo at the position of the table's transform
            Gizmos.DrawWireCube(table.tableTransform.position, Vector3.one);
        }
    }
}
