using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class JumpingPlayerState : BasePlayerState
    {
        private const float JumpForce = 5.0f;

        public override void Enter(PlayerController playerController)
        {
            playerController.Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            playerController.AnimationController.TriggerAnimation("jump");
        }

        public override void Tick(PlayerController playerController) {}

        public override void Leave(PlayerController playerController) {}

        public override void OnCollisionEnter(PlayerController playerController, Collision other)
        {
            playerController.TransitionToState(playerController.IdleState);
        }
    }
}
