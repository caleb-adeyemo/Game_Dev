using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] List<Table> tables; // List of tables

    public Transform GetRandomFreeTable()
    {
        List<Table> freeTables = new List<Table>();
        foreach (Table table in tables)
        {
            if (table.isFree)
            {
                freeTables.Add(table); // Add free tables to the list
            }
        }
        if (freeTables.Count > 0)
        {
            int randomIndex = Random.Range(0, freeTables.Count);
            freeTables[randomIndex].isFree = false; // Mark table as occupied
            return freeTables[randomIndex].tableTransform;
        }
        return null; // No free tables
    }

    public void ReleaseTable(Transform table)
    {
        Table tableObj = tables.Find(t => t.tableTransform == table);
        if (tableObj != null)
        {
            tableObj.isFree = true; // Mark table as available
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Table table in tables)
        {
            Gizmos.DrawWireCube(table.tableTransform.position, Vector3.one);
        }
    }
}
