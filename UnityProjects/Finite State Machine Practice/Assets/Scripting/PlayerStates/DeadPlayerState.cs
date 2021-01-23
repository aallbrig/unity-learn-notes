using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class DeadPlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerController)
        {
            playerController.AnimationController.TriggerAnimation("death");
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                playerController.TransitionToState(playerController.IdleState);
            }
        }

        public override void Leave(PlayerController playerController) {}

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}
    }
}
