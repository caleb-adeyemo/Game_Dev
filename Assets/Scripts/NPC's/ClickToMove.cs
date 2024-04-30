using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent; // Reference to the NavMeshAgent component
    [SerializeField] TableManager tableManager; // Reference to the TableManager script

    void Start()
    {
        SetDestination(); // Call the SetDestination method when the object is initialized
    }

    // Set the destination for the NavMeshAgent
    void SetDestination()
    {
        // Get a random free table's transform from the TableManager
        Transform destination = tableManager.GetRandomFreeTable();

        // If a free table is found
        if (destination != null)
        {
            // Get the position of the destination
            Vector3 targetVector = destination.position;

            // Set the destination for the NavMeshAgent
            agent.SetDestination(targetVector);
        }
    }

    void Update()
    {
        // Check for mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // Perform a raycast to determine where the mouse click hits in the scene
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Set the destination for the NavMeshAgent to the point where the raycast hits
                agent.SetDestination(hit.point);
            }
        }
    }
}
