using UnityEngine;

namespace Scripting.PlayerStates
{
    public class RunningPlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerCtl)
        {
            playerCtl.TriggerAnimation("running");
        }

        public override void Tick(PlayerController playerCtl)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                playerCtl.TransitionToState(playerCtl.WalkingState);
            }
        }

        public override void OnCollisionEnter(PlayerController playerCtl, Collision other) { }
    }
}
