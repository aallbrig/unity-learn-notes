using Scripting.PlayerStates;
using UnityEngine;

namespace Scripting.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region Player states

        public readonly IdlePlayerState IdleState = new IdlePlayerState();
        public readonly CombatIdlePlayerState CombatIdleState = new CombatIdlePlayerState();
        public readonly ChargingPlayerState ChargingState = new ChargingPlayerState();
        public readonly JumpingPlayerState JumpingState = new JumpingPlayerState();
        public readonly WalkingPlayerState WalkingState = new WalkingPlayerState();
        public readonly RunningPlayerState RunningState = new RunningPlayerState();
        public readonly AttackingPlayerState AttackingState = new AttackingPlayerState();
        public readonly TakingDamagePlayerState TakingDamageState = new TakingDamagePlayerState();
        public readonly DeadPlayerState DeadState = new DeadPlayerState();

        private BasePlayerState _currentState;

        #endregion

        public Rigidbody Rigidbody { get; private set; }
        public AnimationController AnimationController { get; private set; }
        
        public void TransitionToState(BasePlayerState state)
        {
            _currentState?.Leave(this);
            _currentState = state;
            _currentState.Enter(this);
        }

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            AnimationController = GetComponent<AnimationController>();

            TransitionToState(IdleState);
        }

        private void Update() => _currentState.Tick(this);

        private void OnCollisionEnter(Collision other) => _currentState.OnCollisionEnter(this, other);
    }
}
