using UnityEngine;

public class IdlePlayerState : BasePlayerState
{
    public override void Enter(PlayerController playerCtl)
    {
        playerCtl.TriggerAnimation("idle");
    }

    public override void Update(PlayerController playerCtl)
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerCtl.Rigidbody.AddForce(Vector3.up * playerCtl.jumpForce, ForceMode.Impulse);
            playerCtl.TransitionToState(playerCtl.JumpingState);
        }
    }

    public override void OnCollisionEnter(PlayerController playerCtl)
    {
        throw new System.NotImplementedException();
    }
}
