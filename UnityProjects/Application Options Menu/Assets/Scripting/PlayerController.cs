using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    public void SetDestination(Vector3 destination)
    {
        CommandManager.Instance.Clear();
        CommandManager.Instance.Add(new MoveCommand(_agent, destination, this));
    }

    public void EnqueueDestination(Vector3 destination)
    {
        CommandManager.Instance.Add(new MoveCommand(_agent, destination, this));
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
