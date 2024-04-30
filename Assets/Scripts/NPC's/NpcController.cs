using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent; // Reference to the NavMeshAgent component

    public void SetDestination(Transform destination)
    {
        if (destination != null)
        {
            agent.SetDestination(destination.transform.position); // Set destination for the NPC
        }
    }
}
