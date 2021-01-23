using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class IdlePlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerController)
        {
            playerController.AnimationController.TriggerAnimation(PlayerAnimations.Idle);
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerController.TransitionToState(playerController.JumpingState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                playerController.TransitionToState(playerController.WalkingState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            {
                playerController.TransitionToState(playerController.DeadState);
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            {
                playerController.TransitionToState(playerController.AttackingState);
            }
        
            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
            {
                playerController.TransitionToState(playerController.TakingDamageState);
            }
        }

        public override void Leave(PlayerController playerController) {}

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}
    }
}
