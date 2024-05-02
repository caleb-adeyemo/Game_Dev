using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] List<Table> tables; // List of tables

    // Get a random free table's transform from the list of tables
    public Table GetRandomFreeTable()
    {
        // List to store references to free tables
        List<Table> freeTables = new List<Table>();

        // Iterate through each table in the list
        foreach (Table table in tables)
        {
            // Check if the table is free
            if (table.isFree){
                freeTables.Add(table); // Add free tables to the list
            }
        }

        // If there are free tables available
        if (freeTables.Count > 0)
        {

            // Get a random index within the range of freeTables list
            int randomIndex = Random.Range(0, freeTables.Count);

            // Mark the selected table as occupied
            freeTables[randomIndex].isFree = false;


            // Return the transform of the selected table
            return freeTables[randomIndex];
        }

        // No free tables available
        return null;
    }

    // Release a table by marking it as free
    public void ReleaseTable(Table table){
        // Set teh table to be free
        table.isFree = true;
    }
}
