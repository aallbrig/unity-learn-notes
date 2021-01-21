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

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            playerCtl.TransitionToState(playerCtl.WalkingState);
        }

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            playerCtl.TransitionToState(playerCtl.DeadState);
        }

        if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
        {
            playerCtl.TransitionToState(playerCtl.AttackingState);
        }
        
        if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
        {
            playerCtl.TransitionToState(playerCtl.TakingDamageState);
        }
    }

    public override void OnCollisionEnter(PlayerController playerCtl) {}
}
