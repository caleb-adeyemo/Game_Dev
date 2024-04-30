using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] TableManager tableManager;

    void Start()
    {
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
