using UnityEngine;

namespace Scripting.PlayerStates
{
    public class JumpingPlayerState : BasePlayerState
    {
        public override void Enter(PlayerController playerCtl)
        {
            playerCtl.Rigidbody.AddForce(Vector3.up * playerCtl.jumpForce, ForceMode.Impulse);
            playerCtl.TriggerAnimation("jump");
        }

        public override void Tick(PlayerController playerCtl) {}

        public override void OnCollisionEnter(PlayerController playerCtl, Collision other) { }
    }
}
