using UnityEngine;

namespace Scripting.PlayerStates
{
    public class WalkingPlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerCtl)
        {
            playerCtl.TriggerAnimation("walking");
        }

        public override void Tick(PlayerController playerCtl)
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                playerCtl.TransitionToState(playerCtl.RunningState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            {
                playerCtl.TransitionToState(playerCtl.IdleState);
            }
        }

        public override void OnCollisionEnter(PlayerController playerCtl, Collision other) {}
    }
}
