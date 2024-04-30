using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    TableManager tableManager;

    void Start()
    {
        tableManager = FindObjectOfType<TableManager>(); // Find TableManager in the scene
        SetDestination();
    }

    void SetDestination()
    {
        Transform destination = tableManager.GetRandomFreeTable(); // Get a free table
        if (destination != null)
        {
            Vector3 targetVector = destination.position;
            agent.SetDestination(targetVector);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
