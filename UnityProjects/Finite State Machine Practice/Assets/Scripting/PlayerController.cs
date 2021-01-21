using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public readonly IdlePlayerState IdleState = new IdlePlayerState();
    public readonly JumpingPlayerState JumpingState = new JumpingPlayerState();

    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }
    public float jumpForce = 5.0f;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private BasePlayerState _currentState;
    private string _currentTrigger;

    public void TriggerAnimation(string triggerName)
    {
        if (_currentTrigger != null)
            _animator.ResetTrigger(_currentTrigger);
        _animator.SetTrigger(triggerName);
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
