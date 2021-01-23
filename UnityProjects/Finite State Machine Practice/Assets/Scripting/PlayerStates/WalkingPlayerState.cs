using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class WalkingPlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerController)
        {
            playerController.AnimationController.TriggerAnimation("walking");
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                playerController.TransitionToState(playerController.RunningState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            {
                playerController.TransitionToState(playerController.IdleState);
            }
        }

        public override void Leave(PlayerController playerController) {}

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}
    }
}
