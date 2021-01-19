using UnityEngine;
using UnityEngine.AI;

public class ToonRTSAnimationController : MonoBehaviour
{
    public GameObject animationTarget;
    private Animator _animator;
    private NavMeshAgent _agent;

    public void AttackAnimation()
    {
        _animator.SetTrigger("attack");
    }

    public void TakeDamageAnimation()
    {
        _animator.SetTrigger("take damage");
    }

    public void Die()
    {
        _animator.SetTrigger("die");
    }

    private void Start()
    {
        _animator = animationTarget.GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }
}
