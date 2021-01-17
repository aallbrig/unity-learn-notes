using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour, ICommand
{
    private readonly NavMeshAgent _agent;
    private readonly Vector3 _destination;
    private readonly Vector3 _previousDestination;

    public Move(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _destination = destination;
        _previousDestination = _agent.destination;
    }

    public void Execute()
    {
        _agent.destination = _destination;
    }

    public void Undo()
    {
        _agent.destination = _previousDestination;
    }
}
