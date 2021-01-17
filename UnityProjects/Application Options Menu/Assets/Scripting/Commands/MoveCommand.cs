using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : ICommand
{
    private readonly NavMeshAgent _agent;
    private readonly Vector3 _destination;
    private readonly MonoBehaviour _mono;
    private IEnumerator _coroutine;

    public MoveCommand(NavMeshAgent agent, Vector3 destination, MonoBehaviour mono)
    {
        _agent = agent;
        _destination = destination;
        _mono = mono;
    }

    public event Command OnCommandStart;
    public event Command OnCommandComplete;

    public void Execute()
    {
        OnCommandStart?.Invoke();
        _agent.destination = _destination;
        _coroutine = AgentReachedDestination(_agent);
        _mono.StartCoroutine(_coroutine);
    }

    private IEnumerator AgentReachedDestination(NavMeshAgent agent)
    {
        while (true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 3)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        OnCommandComplete?.Invoke();
                        _mono.StopCoroutine(_coroutine);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
