using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public readonly IdlePlayerState IdleState = new IdlePlayerState();
    public readonly JumpingPlayerState JumpingState = new JumpingPlayerState();
    public readonly WalkingPlayerState WalkingState = new WalkingPlayerState();
    public readonly RunningPlayerState RunningState = new RunningPlayerState();
    public readonly AttackingPlayerState AttackingState = new AttackingPlayerState();
    public readonly TakingDamagePlayerState TakingDamageState = new TakingDamagePlayerState();
    public readonly DeadPlayerState DeadState = new DeadPlayerState();

    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public float jumpForce = 5.0f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private BasePlayerState _currentState;
    private string _currentTrigger;

    public void TriggerAnimation(string triggerName)
    {
        if (_currentTrigger != null)
        {
            if (_animator.GetBool(_currentTrigger)) _animator.SetBool(_currentTrigger, false);
            else _animator.ResetTrigger(_currentTrigger);
        }

        if (_animator.GetBool(triggerName)) _animator.SetBool(triggerName, true);
        else _animator.SetTrigger(triggerName);
        _currentTrigger = triggerName;
    }
    public void TransitionToState(BasePlayerState state)
    {
        _currentState = state;
        _currentState.Enter(this);
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        TransitionToState(IdleState);
    }

    private void Update()
    {
        _currentState.Update(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        _currentState.OnCollisionEnter(this);
    }
}
