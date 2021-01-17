using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    public void SetDestination(Vector3 destination)
    {
        var cmd = new Move(_agent, destination);
        cmd.Execute();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Set animator to the current agent velocity
    }
}
