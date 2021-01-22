using UnityEngine;

namespace Scripting.PlayerStates
{
    public class TakingDamagePlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerCtl)
        {
            playerCtl.TriggerAnimation("taking damage");
        }

        public override void Tick(PlayerController playerCtl)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            {
                playerCtl.TransitionToState(playerCtl.IdleState);
            }
        }

        public override void OnCollisionEnter(PlayerController playerCtl, Collision other) {}
    }
}
