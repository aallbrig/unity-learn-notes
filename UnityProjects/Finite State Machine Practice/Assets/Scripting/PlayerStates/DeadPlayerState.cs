using UnityEngine;

public class DeadPlayerState : BasePlayerState
{
    public override void Enter(PlayerController playerCtl)
    {
        playerCtl.TriggerAnimation("death");
    }

    public override void Update(PlayerController playerCtl)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            playerCtl.TransitionToState(playerCtl.IdleState);
        }
    }

    public override void OnCollisionEnter(PlayerController playerCtl) {}
}
